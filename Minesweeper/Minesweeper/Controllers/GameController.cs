using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Minesweeper.CompositeModels;
using Minesweeper.Filters;
using Minesweeper.Models;
using Minesweeper.Services.Business;
using Minesweeper.Services.Utility;

namespace Minesweeper.Controllers {
    /// <summary>
    /// Interacts with Game services
    /// to manage and show state
    /// </summary>
    [AuthorizationFilter]
    public class GameController : Controller {
        private int ID;

        private GameManagementService GameSvc;

        private GameStateViewModel GameViewModel;

        private GameStateManagementService GameStateSvc;

        private readonly ILogger Logger;

        /// <summary>
        /// Injects a logger
        /// </summary>
        /// <param name="logger"></param>
        public GameController(ILogger logger) {
            Logger = logger;

            var tmpId = System.Web.HttpContext.Current.Session["ID"];

            // dont instantiate business services
            // unless there is an id in session
            // authorization filter runs after
            // constructer
            if (tmpId != null) {
                ID = (int) tmpId;
                GameSvc = new GameManagementService(ID);
                GameViewModel = new GameStateViewModel(ID) { Game = GameModel.GetGameModelInstance(ID) };
                GameStateSvc = new GameStateManagementService();
            }

        }

        /// <summary>
        /// Shows Gameboard
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpGet]
        public ActionResult Index() {
            Logger.Debug($"In {GetType().FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}");

            // try to insert into db
            try
            {
                // if ajax request return partial view
                if (Request.IsAjaxRequest()) {
                    return PartialView("GameBoard", GameViewModel);
                }

                // check if instance of game is running
                if (GameSvc.hasStartedGame(ID)) {
                    Logger.Debug("IN NON DB START");
                    return View("Game", GameViewModel);
                }
                // if game hasnt been started in multi-singleton
                // check database to restore state
                else if (GameStateSvc.HasGameInDB(ID)) {
                    Logger.Debug("IN DB RESTORE");
                    GameStateSvc.RestoreGameState(ID);
                    GameViewModel = new GameStateViewModel(ID);
                    return View("Game", GameViewModel);
                }
                // if a game is neither in the db or in the dictionary
                // start a new game
                else {
                    Logger.Debug("IN REGULAR START");
                    GameSvc.ResetBoard();
                    return View("Game", GameViewModel);
                }
            }
            // redirect to login if db error
            catch (Exception e) {
                Logger.Error(e.ToString());
                return RedirectToAction("Index", "Login");
            }
        }

        /// <summary>
        /// Clears sesion and logs out
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Game/Exit")]
        public ActionResult Exit() {
            // clear session
            System.Web.HttpContext.Current.Session.Clear();
            return View("~/Views/Login/Login.cshtml", new AuthorizationViewModel() {Message = "Logged Out"});
        }

        /// <summary>
        /// Resets game board
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpGet]
        [Route("Game/Reset")]
        public ActionResult Reset() {
            Logger.Debug($"In {GetType().FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}");

            // sets cell, clicks and count ints to 0
            // sets flagged, Won and lost bools to false
            GameSvc.ResetBoard();

            // if ajax request return partial view
            if (Request.IsAjaxRequest()) {
                return PartialView("GameBoard", GameViewModel);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// gets board state from db
        /// then returns a partial view 
        /// with the game state
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Game/Restore")]
        public ActionResult RestoreGame() {
            Logger.Debug($"In {GetType().FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}");

            //attempts to restore game state
            try
            {
                Logger.Debug("Attempting to restore game board for user: {0}", ID.ToString());
                GameStateSvc.RestoreGameState(ID);
                GameViewModel = new GameStateViewModel(ID);
                return PartialView("GameBoard", GameViewModel);
            }
            catch (Exception e) {
                Logger.Error(e.ToString());
                AuthorizationViewModel m = new AuthorizationViewModel {Message = "internal error"};
                return View("~/Views/Login/Login.cshtml", m);
            }
        }

        /// <summary>
        /// Toggles Flags on Tiles
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="Col"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Game/{Row}/{Col}")]
        public ActionResult Flag(int Row, int Col) {
            Logger.Debug($"In {GetType().FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}");
            GameSvc.ToggleFlag(Row, Col);
            return PartialView("GameBoard", GameViewModel);
        }

        /// <summary>
        /// Saves Game State
        /// </summary>
        /// <returns>PartialView GameBoard</returns>
        [HttpGet]
        [Route("Game/Save")]
        public ActionResult Save() {
            Logger.Debug($"In {GetType().FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}");

            // attempts to serialize and store Game State
            // in db with user id as key
            try
            {
                GameStateSvc.InsertGameState(ID);
            }
            catch (Exception e) {
                Logger.Error(e.ToString());
            }

            return PartialView("GameBoard", GameViewModel);
        }

        /// <summary>
        /// Shows Game View
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="Col"></param>
        /// <param name="Secs"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Game/{Row}/{Col}/{Secs}")]
        public ActionResult Index(int Row, int Col, int Secs) {

            Logger.Debug("In {0}", GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);

            String GameStatus = "Ongoing";

            Logger.Debug("Game Status: {0}",GameStatus);

            GameViewModel.Game.Clicks++;

            // returns partial view with updated model and gamestatus 
            if (Request.IsAjaxRequest()) {
                // processes action on gameboard
                GameSvc.ProcessCell(Row, Col);
                GameSvc.UpdateTime(Secs);

                // check for win/loss and update game status
                if (GameModel.GetGameModelInstance(ID).HasWon) {
                    GameStatus = "Won";
                }
                if (GameModel.GetGameModelInstance(ID).HasLost) {
                    GameStatus = "Lost";
                }
                
                GameViewModel.Status = GameStatus;


                return PartialView("GameBoard", GameViewModel);
            }

            return View("Game");
        }
    }
}
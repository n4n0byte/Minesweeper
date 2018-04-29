using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Minesweeper.CompositeModels;
using Minesweeper.Models;
using Minesweeper.Services.Business;
using Minesweeper.Services.Utility;

namespace Minesweeper.Controllers
{

    /// <summary>
    /// Interacts with Game services
    /// to manage and show state
    /// </summary>
    public class GameController : Controller {

        private static int ID = (int)System.Web.HttpContext.Current.Session["ID"];

        private GameManagementService GameSvc = new GameManagementService(ID);

        private GameStateViewModel GameViewModel = new GameStateViewModel(ID);

        private GameStateManagementService GameStateSvc = new GameStateManagementService();

        private readonly ILogger Logger;

        /// <summary>
        /// Injects a logger
        /// </summary>
        /// <param name="logger"></param>
        public GameController(ILogger logger) {
            Logger = logger;
        }

        /// <summary>
        /// Shows Gameboard
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpGet]
        public ActionResult Index() {

            Logger.Debug("In {0}", GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);

            // try to insert into db
            try {
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
        /// Resets game board
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpGet]
        [Route("Game/Reset")]
        public ActionResult Reset() {
            
            GameSvc.ResetBoard();

            // if ajax request return partial view
            if (Request.IsAjaxRequest())
            {
                return PartialView("GameBoard", GameViewModel);
            }

            return RedirectToAction("Index");

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
            Logger.Debug("In {0}", GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            GameSvc.ToggleFlag(Row, Col);
            return PartialView("GameBoard", GameViewModel );
        }



        [HttpGet]
        [Route("Game/Save")]
        public ActionResult Save() {

            GameStateSvc.InsertGameState(ID);

            return PartialView("GameBoard", GameViewModel );
        }

        [HttpGet]
        [Route("Game/{Row}/{Col}/{Secs}")]
        public ActionResult Index(int Row, int Col, int Secs) {

            String GameStatus = "Ongoing";

            GameViewModel.Game.Clicks++;

            // returns partial view with updated model and gamestatus 
            if (Request.IsAjaxRequest()) {

                // processes action on gameboard
                GameSvc.ProcessCell(Row, Col);
                GameSvc.UpdateTime(Secs);

                if (GameModel.GetGameModelInstance(ID).HasWon) {
                    GameStatus = "Won";
                }
                if (GameModel.GetGameModelInstance(ID).HasLost) {
                    GameStatus = "Lost";
                }

                if (!GameStatus.Equals("Ongoing")) {
                    
                }

                GameViewModel.Status = GameStatus;


                return PartialView("GameBoard",GameViewModel);
            }

            return View("Game");
        }


    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Minesweeper.Models;
using Minesweeper.Services.Business;
using Minesweeper.ViewModels;

namespace Minesweeper.Controllers
{
    public class GameController : Controller {

        private static string Username = System.Web.HttpContext.Current.Session["Username"] as string;

        private GameManagementService GameSvc = new GameManagementService(Username);

        private GameStateViewModel GameViewModel = new GameStateViewModel(Username);

        [HttpGet]
        public ActionResult Index() {

            
            // if ajax request return partial view
            if (Request.IsAjaxRequest()) {
                return PartialView("GameBoard", GameViewModel);
            }

            return View("Game",GameViewModel);

        }

        [HttpPost]
        [Route("Game/{Row}/{Col}")]
        public ActionResult Flag(int Row, int Col)
        {

            Debug.WriteLine(Row);
            GameSvc.ToggleFlag(Row, Col);

            
            

            return PartialView("GameBoard", GameViewModel );
        }

        [HttpGet]
        [Route("Game/Load")]
        public ActionResult Load() {
 
            // check if user has game stored in db already
        
            // check if instance of game is running
            if (!GameSvc.hasStartedGame(Username)) {
                GameSvc.ResetBoard();
                
            }
         
            // if user has game, load it otherwise make a new game

            // TODO add load functionality 
            return RedirectToAction("Index");

        }

        [HttpGet]
        [Route("Game/Save")]
        public ActionResult Save() {
            
            // serialize game state
            // serialize player stats

            // save both into db
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Game/{Row}/{Col}/{Secs}")]
        public ActionResult Index(int Row, int Col, int Secs) {

            String GameStatus = "Ongoing";
            
            // returns partial view with updated model and gamestatus 
            if (Request.IsAjaxRequest()) {

                // processes action on gameboard
                GameSvc.ProcessCell(Row, Col);
                GameSvc.UpdateTime(Secs);

                if (GameModel.GetGameModelInstance(Username).HasWon) {
                    GameStatus = "Won";
                }
                if (GameModel.GetGameModelInstance(Username).HasLost) {
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
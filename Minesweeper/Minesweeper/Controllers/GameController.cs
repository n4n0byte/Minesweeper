using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Minesweeper.Models;
using Minesweeper.Services.Business;

namespace Minesweeper.Controllers
{
    public class GameController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            GameManagementService GameSvc = new GameManagementService();
            GameSvc.ResetBoard();
            return View("Game");
        }

        [HttpPost]
        [Route("Game/{Secs}")]
        public ActionResult Index(int Secs)
        {

            GameManagementService GameSvc = new GameManagementService();
            GameModel.GetGameModelInstance().Secs = Secs;
            return View("Game");
        }

        [HttpGet]
        [Route("Game/{Result}")]
        public ActionResult Index(String Result) {
            
            GameManagementService GameSvc = new GameManagementService();

            if (Result.Equals("won")) {
                GameSvc.RevealAll();
                return RedirectToAction("Index", "Game");
            }
            else if (Result.Equals("lost")) {
                GameSvc.RevealAll();
                return View("Game");
            }
            else {
                return View("Game");
            }
            
        }

        [HttpGet]
        [Route("Game/{Row}/{Col}")]
        public ActionResult Index(int Row, int Col) {

            GameManagementService GameSvc = new GameManagementService();
            
            GameSvc.ProcessCell(Row,Col);

            if (GameModel.GetGameModelInstance().HasWon) {
                return RedirectToAction("Index", "Game", new {Result = "won"});
            }

            if (GameModel.GetGameModelInstance().HasLost) {
                Debug.WriteLine("Lost");
                return RedirectToAction("Index", "Game", new { Result = "lost" });
            }

            return View("Game");
        }

    }
}
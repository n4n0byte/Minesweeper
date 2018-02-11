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
            GameModel.GetGameModelInstance().Secs = 0;
            return View("Game");
        }

        [HttpPost]
        [Route("Game/{Secs}")]
        public ActionResult Index(int secs)
        {

            GameManagementService GameSvc = new GameManagementService();
            Debug.WriteLine(secs);
            var m = GameModel.GetGameModelInstance();
            m.Secs = secs;
            return View("Game");
        }

        [HttpGet]
        [Route("Game/{Result}")]
        public ActionResult Index(String Result) {
            
            GameManagementService GameSvc = new GameManagementService();

            if (Result.Equals("won")) {
                return RedirectToAction("Index", "Game");
            }
            else if (Result.Equals("lost") && !GameSvc.HasWon()) {
                GameSvc.ResetBoard();
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

            return View("Game");
        }

    }
}
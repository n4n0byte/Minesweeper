﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Minesweeper.Models;
using Minesweeper.Services.Business;

namespace Minesweeper.Controllers
{
    public class GameController : Controller {

        private GameManagementService GameSvc;

        public GameController() { 
            GameSvc = new GameManagementService();
        }

        [HttpGet]
        public ActionResult Index() {
            GameSvc.ResetBoard();
            return View("Game");
        }

        [HttpGet]
        [Route("Game/{Result}")]
        public ActionResult Index(String Result) {
            
            GameManagementService GameSvc = new GameManagementService();

            if (Result.Equals("won")) {
                //todo add extra functionality
                GameSvc.RevealAll();
                return View("Game");
            }
            else if (Result.Equals("lost")) {
                //todo add extra functionality
                GameSvc.RevealAll();
                return View("Game");
            }
            else {
                return View("Game");
            }
            
        }

        [HttpPost]
        [Route("Game/{Row}/{Col}")]
        public ActionResult Flag(int Row, int Col)
        {
            GameManagementService GameSvc = new GameManagementService();
            Debug.WriteLine(Row);
            GameSvc.ToggleFlag(Row,Col);

            return View("Game");
        }

        [HttpGet]
        [Route("Game/{Row}/{Col}/{Secs}")]
        public ActionResult Index(int Row, int Col, int Secs) {

            String GameStatus = "Ongoing";
            GameManagementService GameSvc = new GameManagementService();

            if (Request.IsAjaxRequest()) {
                GameSvc.ProcessCell(Row, Col);
                GameSvc.UpdateTime(Secs);

                if (GameModel.GetGameModelInstance().HasWon) {
                    GameStatus = "Won";
                }
                if (GameModel.GetGameModelInstance().HasLost) {
                    GameStatus = "Lost";
                }

                return PartialView("GameBoard",(Object)GameStatus);
            }

            return View("Game");
        }


    }
}
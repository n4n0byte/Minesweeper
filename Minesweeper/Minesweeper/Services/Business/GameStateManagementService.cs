using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Minesweeper.Models;
using Minesweeper.Services.Data;

namespace Minesweeper.Services.Business
{

    public class GameStateManagementService
    {

        private GameStateDAO GameSvc = new GameStateDAO();

        public void insertGameState(int ID) {

            GameModel Game = GameModel.GetGameModelInstance(ID);

            // make game json serialized
            string gameJson = new JavaScriptSerializer().Serialize(Game);

            PlayerStatModel CurStat = new PlayerStatModel(Game.Clicks,Game.Secs);
            
            GameSvc.InsertGameState(ID ,gameJson,CurStat);

        }

    }
}
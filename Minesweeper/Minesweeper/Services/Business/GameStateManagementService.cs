using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Minesweeper.Models;
using Minesweeper.Services.Data;
using Newtonsoft.Json;

namespace Minesweeper.Services.Business
{

    public class GameStateManagementService
    {

        private GameStateDAO GameSvc = new GameStateDAO();

        /**
         * checks if User has saved a game
         */
        public bool HasGameInDB(int ID) {
            return GameSvc.PlayerIDInDatabase(ID);
        }

        public void RestoreGameState(int ID) {

            // get game from db as json
            string GameJson = GameSvc.GetGameJsonByID(ID);

            // Deserialize back into a GameModel
            GameModel ResGame = JsonConvert.DeserializeObject<GameModel>(GameJson);

            // Replace it in the dictionary
            GameModel.ReplaceGame(ID,ResGame);

        }

        /**
         * json serializes game then inserts it into db
         */
        public void InsertGameState(int ID) {

            GameModel Game = GameModel.GetGameModelInstance(ID);

            // make game json serialized
            string gameJson = JsonConvert.SerializeObject(GameModel.GetGameModelInstance(ID));

            PlayerStatModel CurStat = new PlayerStatModel(Game.Clicks,Game.Secs);
            
            GameSvc.InsertGameState(ID ,gameJson,CurStat);

        }

    }
}
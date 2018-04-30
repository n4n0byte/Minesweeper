using System.Collections.Generic;
using Minesweeper.Models;
using Minesweeper.Services.Data;
using Newtonsoft.Json;

namespace Minesweeper.Services.Business
{

    /**
     * Manages Insertion and Restoration of 
     * GameJson
     */
    public class GameStateManagementService
    {

        private GameStateDAO GameSvc = new GameStateDAO();
        
        /// <summary>
        /// searches for player stat by playerID, will
        /// return null if nothing is found
        /// </summary>
        /// <param name="PlayerID"></param>
        /// <returns>PlayerStatModel</returns>
        public PlayerStatModel GetPlayerStatModelById(int PlayerID) {
            return GameSvc.GetPlayerStatModelById(PlayerID);
        }

        /// <summary>
        /// returns all player stats in db as a list
        /// </summary>
        /// <returns></returns>
        public List<PlayerStatModel> GetAllPlayerStats() {
            return GameSvc.GetAllPlayerStats();
        }

        /**
         * checks if User has saved a game
         */
        public bool HasGameInDB(int ID) {
            return GameSvc.PlayerIDInDatabase(ID);
        }

        /**
         * Retrieves Game State from DB,
         * Deserializes it, then injects it into
         * the Game Singleton Dictionary with the PlayerID
         * as the key
         */ 
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
            
            // insert game state into database
            GameSvc.InsertGameState(ID ,gameJson,CurStat);

        }

    }
}
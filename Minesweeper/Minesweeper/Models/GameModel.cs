using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Minesweeper.Models
{
    [Serializable]
    public class GameModel {

        public Cell[,] Board { get; set; }
        public int Rows { get; set; } = 7;
        public int Cols { get; set; } = 7;
        public int Secs { get; set; } = 0;
        public bool HasWon { get; set; } = false;
        public bool HasLost { get; set; } = false;
        public int Clicks { get; set; } = 0;
        public bool Started { get; set; } = false;


        private static Dictionary<string,GameModel> UserGameDictionary =  new Dictionary<string, GameModel>();

        

        /**
         * Constructs a new Game per Username
         */
        public static GameModel GetGameModelInstance(string Username) {

            // lazily initialize a game according to username
            if (!UserGameDictionary.ContainsKey(Username)) {
                UserGameDictionary.Add(Username, new GameModel());
            }


            return UserGameDictionary[Username];

        }



        /**
         * Deserializes a game and stores/updates
         * the UserGameDict
         */
        public static void DeserializeAndRegisterGame(string Username, string SerializedGame) {

            JavaScriptSerializer JSDeserializer = new JavaScriptSerializer();

            GameModel DeserializedGame = JSDeserializer.Deserialize<GameModel>(SerializedGame);

            // either replace game dictionary at Username
            // or insert a game
            if (!UserGameDictionary.ContainsKey(Username)) {
                UserGameDictionary.Add(Username, new GameModel());
            } else {
                UserGameDictionary[Username] = DeserializedGame;
            }

        }

        /**
         * removes game from dictionary
         */
        public static void RemoveGame(string Username) {

            UserGameDictionary[Username] = null;

        }

        /**
         * checks to see if user has started a game
         */
        public static bool HasGame(string Username) {

            if (UserGameDictionary.ContainsKey(Username)) return true;
            return false;

        }

        /**
         * Crates A GameModel Instance
         */
        private GameModel() {
            Board = new Cell[Rows,Cols];

            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Cols; j++) {
                    Board[i,j] = new Cell();
                    Board[i,j].col = j;
                    Board[i, j].row = i;
                }
            }

        }


    }
}
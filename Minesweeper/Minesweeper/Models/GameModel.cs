using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Minesweeper.Models
{
    [Serializable]
    [DataContract]
    public class GameModel {

        [DataMember]
        public Cell[,] Board { get; set; }

        [DataMember]
        public int Rows { get; set; } = 7;

        [DataMember]
        public int Cols { get; set; } = 7;

        [DataMember]  
        public int Secs { get; set; } = 0;

        [DataMember]
        public bool HasWon { get; set; } = false;

        [DataMember]
        public bool HasLost { get; set; } = false;

        [DataMember]
        public int Clicks { get; set; } = 0;

        [DataMember]
        public bool Started { get; set; } = false;

        [JsonIgnore]
        [IgnoreDataMember]
        private static Dictionary<int,GameModel> UserGameDictionary =  new Dictionary<int, GameModel>();

        

        /**
         * Constructs a new Game per ID
         */
        public static GameModel GetGameModelInstance(int ID) {

            // lazily initialize a game according to username
            if (!UserGameDictionary.ContainsKey(ID)) {
                UserGameDictionary.Add(ID, new GameModel());
            }


            return UserGameDictionary[ID];

        }

        public static void ReplaceGame(int ID, GameModel InGame) {

            UserGameDictionary.Remove(ID);
            UserGameDictionary[ID] = InGame;

        }


        /**
         * Deserializes a game and stores/updates
         * the UserGameDict
         */
        public static void DeserializeAndRegisterGame(int ID, string SerializedGame) {

            JavaScriptSerializer JSDeserializer = new JavaScriptSerializer();

            GameModel DeserializedGame = JSDeserializer.Deserialize<GameModel>(SerializedGame);

            // either replace game dictionary at ID
            // or insert a game
            if (!UserGameDictionary.ContainsKey(ID)) {
                UserGameDictionary.Add(ID, new GameModel());
            } else {
                UserGameDictionary[ID] = DeserializedGame;
            }

        }

        /**
         * removes game from dictionary
         */
        public static void RemoveGame(int ID) {

            UserGameDictionary[ID] = null;

        }

        /**
         * checks to see if user has started a game
         */
        public static bool HasGame(int ID) {

            if (UserGameDictionary[ID].Started) return true;
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
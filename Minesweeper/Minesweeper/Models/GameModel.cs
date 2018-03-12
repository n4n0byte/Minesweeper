using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minesweeper.Models
{
    public class GameModel {

        public Cell[,] Board { get; set; }
        public int Rows { get; set; } = 7;
        public int Cols { get; set; } = 7;
        public int Secs { get; set; } = 0;
        public bool HasWon { get; set; } = false;
        public bool HasLost { get; set; } = false;

        private static GameModel Game = new GameModel();

        public static GameModel GetGameModelInstance() {
            return Game;
        }

        /**
         * 
         */
        public static GameModel GetGameModelInstance(int id) {

            return Game;

        }

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
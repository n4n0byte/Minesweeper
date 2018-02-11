using System;
using System.Collections.Generic;
using Minesweeper.Models;

namespace Minesweeper.Services.Business {
    public class GameManagementService {
        private readonly GameModel Game = GameModel.GetGameModelInstance();
        private readonly Random Rand = new Random();

        public void ResetBoard() {
            // clears each tile
            for (var i = 0; i < Game.Rows; i++)
            for (var j = 0; j < Game.Cols; j++)
                Game.Board[i, j].reset();


            RandomlySetActiveCells();

            // loops through and sets live neighbor count
            // for each cell
            for (var i = 0; i < Game.Rows; i++)
            for (var j = 0; j < Game.Cols; j++)
                SetLiveNeighbors(i, j);
        }

        public void RevealAll() {
            for (var row = 0; row < Game.Rows; row++)
            for (var col = 0; col < Game.Cols; col++)
                Game.Board[row, col].BeenVisited = true;
        }

        public bool HasWon() {
            for (var row = 0; row < Game.Rows; row++)
            for (var col = 0; col < Game.Cols; col++)
                // check for non visited regular tiles
                if (!Game.Board[row, col].BeenVisited && !Game.Board[row, col].isLive)
                    return false;

            return true;
        }

        public List<Cell> GetBlankNeighbors(int row, int col) {
            var neighbors = new List<Cell>();

            // checking for unvisited 0 val neighbors
            for (var curRow = row - 1; curRow <= row + 1; curRow++)
            for (var curCol = col - 1; curCol <= col + 1; curCol++)
                // bounds checking
                if (curCol >= 0 && curCol < Game.Cols && curRow >= 0 && curRow < Game.Rows)
                    if (!Game.Board[curRow, curCol].BeenVisited && Game.Board[curRow, curCol].liveNeighbors == 0)
                        neighbors.Add(Game.Board[curRow, curCol]);

            return neighbors;
        }

        public void SetLiveNeighbors(int row, int col) {
            var count = 0;

            if (Game.Board[row, col].isLive) {
                Game.Board[row, col].liveNeighbors = 9;
            }
            else {
                // checking for live neighbors
                for (var curRow = row - 1; curRow <= row + 1; curRow++)
                for (var curCol = col - 1; curCol <= col + 1; curCol++)
                    // bounds checking
                    if (curCol >= 0 && curCol < Game.Cols && curRow >= 0 && curRow < Game.Rows)
                        if (Game.Board[curRow, curCol].isLive)
                            count++;

                Game.Board[row, col].liveNeighbors = count;
            }
        }

        public void RandomlySetActiveCells() {
            for (var row = 0; row < Game.Rows; row++)
            for (var col = 0; col < Game.Cols; col++) {
                var randNum = Rand.Next(1, 101);
                // activates cell with 15% chance
                if (randNum <= 15)
                    Game.Board[row, col].isLive = true;
            }
        }

        private void SearchAndActivateBlankNeighbors(Cell cell)
        {

            if (!cell.BeenVisited)
            {

                cell.BeenVisited = true;
                // use SearchAndActivateBlankNeighbors to search and activate cells with no nearby mines 
                foreach (Cell curCell in GetBlankNeighbors(cell.row, cell.col))
                {
                    SearchAndActivateBlankNeighbors(curCell);
                }

            }
        }

        public void UpdateTime(int Secs) {
            Game.Secs = Secs;
        }

        public void ProcessCell(int Row, int Col) {
            Cell CurCell = Game.Board[Row, Col];

            if (CurCell.liveNeighbors == 0) {
                SearchAndActivateBlankNeighbors(CurCell);
            }
            else {
                CurCell.BeenVisited = true;
            }

        }
    }
}
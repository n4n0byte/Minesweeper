using System;
using System.Collections.Generic;
using Minesweeper.Models;

namespace Minesweeper.Services.Business {

    /// <summary>
    /// Handles Game State
    /// </summary>
    public class GameManagementService {

        private GameModel Game;
        private Random Rand = new Random();

        /**
         * creates a game usig user id
         */
        public GameManagementService(int ID) {
           
            Game = GameModel.GetGameModelInstance(ID);

        }
        
        /// <summary>
        /// checks if game has started
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>bool</returns>
        public bool hasStartedGame(int ID) {

            return GameModel.HasGame(ID);

        }

        /**
         * removes game from name - Gameodel dict
         */
        public void removeGame(int ID) {
            GameModel.RemoveGame(ID);
        }

        /**
         * Resets Game State
         */
        public void ResetBoard() {

            // reset win loss bools
            Game.HasLost = false;
            Game.HasWon = false;
            Game.Started = true;

            Game.Secs = 0;

            // clears each tile
            for (var i = 0; i < Game.Rows; i++)
            for (var j = 0; j < Game.Cols; j++)
                Game.Board[i, j].Reset();


            RandomlySetActiveCells();

            // loops through and sets live neighbor count
            // for each cell
            for (var i = 0; i < Game.Rows; i++)
            for (var j = 0; j < Game.Cols; j++)
                SetLiveNeighbors(i, j);
        }

        /// <summary>
        /// sets every Cell in gameboard to
        /// visited
        /// </summary>
        public void RevealAll() {
            for (var row = 0; row < Game.Rows; row++)
            for (var col = 0; col < Game.Cols; col++)
                Game.Board[row, col].BeenVisited = true;
        }

        /// <summary>
        /// checks for a win
        /// </summary>
        /// <returns></returns>
        public bool HasWon() {
            for (var row = 0; row < Game.Rows; row++)
            for (var col = 0; col < Game.Cols; col++)
                // check for non visited regular tiles
                if (!Game.Board[row, col].BeenVisited && !Game.Board[row, col].isLive)
                    return false;

            return true;
        }

        /// <summary>
        /// Gets List of unvisited neighbors
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private List<Cell> GetBlankNeighbors(int row, int col) {
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

        /// <summary>
        /// Sets the count of surrounding mines
        /// for every cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
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

        /// <summary>
        /// Randomly makes cells bombs
        /// </summary>
        /// <remarks>
        /// currently set to a 15% chance
        /// </remarks>
        public void RandomlySetActiveCells() {
            for (var row = 0; row < Game.Rows; row++)
            for (var col = 0; col < Game.Cols; col++) {
                var randNum = Rand.Next(1, 101);
                // activates cell with 15% chance
                if (randNum <= 15)
                    Game.Board[row, col].isLive = true;
            }
        }

        /// <summary>
        /// DFS cell discovery
        /// </summary>
        /// <param name="cell"></param>
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

        /// <summary>
        /// checks for a game loss (this is bad, should just pass
        ///  in current cell position, then check if it is a mine)
        /// </summary>
        /// <returns></returns>
        private bool HasLost() {
            for (var row = 0; row < Game.Rows; row++)
            for (var col = 0; col < Game.Cols; col++)
                // check for non visited regular tiles
                if (Game.Board[row, col].BeenVisited && Game.Board[row, col].isLive)
                    return true;
            return false;
        }

        /// <summary>
        /// Sets game time to 
        /// </summary>
        /// <param name="Secs"></param>
        public void UpdateTime(int Secs) {
            Game.Secs = Secs;
        }

        /// <summary>
        /// flips the flag variable on a cell
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="Col"></param>
        public void ToggleFlag(int Row, int Col) {
            // flip falg bool given index
            Cell CurCell = Game.Board[Row, Col];
            CurCell.flagged = !CurCell.flagged;
        }

        /// <summary>
        /// Handles what happens after a cell gets
        /// clicked
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="Col"></param>
        public void ProcessCell(int Row, int Col) {
            Cell CurCell = Game.Board[Row, Col];

            // if cell is zero, reveal all touching tiles
            // that are also zero recursively
            if (CurCell.liveNeighbors == 0) {
                SearchAndActivateBlankNeighbors(CurCell);
            }
            // set beenvisited flag
            else {
                CurCell.BeenVisited = true;
            }

            // check for win           
            if (HasWon()) {
                Game.HasWon = true;
            }
            
            // check for loss
            else if (HasLost()) {
                Game.HasLost = true;
            }

            // show all cells if player won or loss
            if (Game.HasWon || Game.HasLost) {
                RevealAll();
            }

        }
    }
}
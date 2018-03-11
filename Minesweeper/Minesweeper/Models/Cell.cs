using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Minesweeper {
    public class Cell {

        [JsonProperty("row")]
        public int row { get; set; }

        [JsonProperty("col")]
        public int col { get; set; }

        public int liveNeighbors { get; set; }

        [JsonProperty("beenVisited")]
        public bool BeenVisited { get; set; } = false;

        public bool isLive { get; set; }

        [JsonProperty("flagged")]
        public bool flagged { get; set; } = false;

        public Cell(int row, int col) {
            this.row = row;
            this.col = col;
            Reset();
        }
        
        public void toggleFlag() {

            flagged = !flagged;

        }

        public Cell() : this(-1, -1) {
        }

        public void Reset() {
            this.liveNeighbors = 0;
            this.BeenVisited = false;
            this.isLive = false;
            this.flagged = false;
        }
    }
}
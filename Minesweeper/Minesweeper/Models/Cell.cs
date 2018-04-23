using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Minesweeper {
    
    [DataContract]
    public class Cell {

        [DataMember]
        [JsonProperty("row")]
        public int row { get; set; }

        [DataMember]
        [JsonProperty("col")]
        public int col { get; set; }

        [DataMember]
        public int liveNeighbors { get; set; }

        [DataMember]
        [JsonProperty("beenVisited")]
        public bool BeenVisited { get; set; } = false;

        [DataMember]
        public bool isLive { get; set; }

        [DataMember]
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
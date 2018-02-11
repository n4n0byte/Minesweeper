﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper {
    public class Cell {

        public int row { get; set; }
        public int col { get; set; }
        public int liveNeighbors { get; set; }
        public bool BeenVisited { get; set; } = false;
        public bool isLive { get; set; }
        public bool flagged { get; set; }

        public Cell(int row, int col) {
            this.row = row;
            this.col = col;
            reset();
        }

        public void toggleFlag() {

            /**
            if (this.Text == "F") {
                this.Text = "";
            }
            else {
                this.Text = "F";
            }
            */
            flagged = !flagged;

        }

        public Cell() : this(-1, -1) {
        }

        public void reset() {
            this.liveNeighbors = 0;
            this.BeenVisited = false;
            this.isLive = false;
            this.flagged = false;
        }
    }
}
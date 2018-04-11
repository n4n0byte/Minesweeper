using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minesweeper.Models
{
    /**
     * Stores number of clicks and 
     * time in seconds
     */
    [Serializable]
    public class PlayerStatModel
    {

        public int NumberOfClicks { get; set; }

        public int TimeSpent { get; set; }

    }
}
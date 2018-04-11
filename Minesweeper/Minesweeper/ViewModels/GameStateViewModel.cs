using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Minesweeper.Models;

namespace Minesweeper.ViewModels
{
    public class GameStateViewModel
    {

        public GameModel Game { get; set; }

        public string Status { get; set; } = "Ongoing";

        public GameStateViewModel(int ID)
        {
            this.Game = GameModel.GetGameModelInstance(ID);
        }



    }
}
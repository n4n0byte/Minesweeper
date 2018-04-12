using Minesweeper.Models;

namespace Minesweeper.CompositeModels
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
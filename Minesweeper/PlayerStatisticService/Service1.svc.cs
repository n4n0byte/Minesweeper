using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Minesweeper.Services.Data;

namespace PlayerStatisticService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PlayerStatService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PlayerStatService.svc or PlayerStatService.svc.cs at the Solution Explorer and start debugging.
    public class PlayerStatService : IPlayerStatService {

        private GameStateDAO GameStateSvc;

        public PlayerStatService() {
            GameStateSvc = new GameStateDAO();
        }

        public DTO GetAllPlayerStats() {
            throw new NotImplementedException();
        }

        public DTO GetUser(int id) {
            throw new NotImplementedException();
        }
    }
}

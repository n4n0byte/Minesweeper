using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Minesweeper.Models;
using Minesweeper.Services.Business;
using Minesweeper.Services.Data;

namespace PlayerStatisticService {
    
    public class PlayerStatService : IPlayerStatService {
        private GameStateManagementService GameStateSvc;
        
        /**
         * 
         */
        public PlayerStatService() {
            GameStateSvc = new GameStateManagementService();
        }

        /**
         * Retrieves a List of players
         * Returns List<Players> 
         */
        public DTO GetAllPlayerStats() {

            int StatusCode = 0;
            String Message = "Success";
            List<PlayerStatModel> Results = null;

            try {
                Results = GameStateSvc.GetAllPlayerStats();

                // set status and message codes if nothing is returned
                if (Results.Count == 0) {
                    StatusCode = -1;
                    Message = "No Player Stats In DB";
                }

            } catch (Exception e) {
                // set error if exception is thrown
                // set Message to this
                StatusCode = -2;
                Message = $"Server Error\n{e.StackTrace}";
                Results = new List<PlayerStatModel>();
            }

            return new DTO(StatusCode, Message, Results);
        }

        /**
         * returns PlayerStat
         */
        public DTO GetPlayerStat(string id) {

            int StatusCode = 0;
            String Message = "Success";
            List<PlayerStatModel> Results = new List<PlayerStatModel>();
            PlayerStatModel Result = null;

            try {

                int IdNum = -1;

                // set error if input is not a number
                if (Int32.TryParse(id, out IdNum) == false) {
                    StatusCode = -3;
                    Message = "Error, not a number";
                }
                else {
                    Result = GameStateSvc.GetPlayerStatModelById(IdNum);

                    // check if player is in db
                    if (Result == null) {
                        StatusCode = -1;
                        Message = "Player Id not found";
                        Results = new List<PlayerStatModel>();
                    }
                    else {
                        Results.Add(Result);
                    }
                }
            }
            catch (Exception e) {
                // set error if exception is thrown
                // set Message to this
                StatusCode = -2;
                Message = $"Server Error\n{e.StackTrace}";
            }
           

            return new DTO(StatusCode, Message, Results);
        }
    }
}

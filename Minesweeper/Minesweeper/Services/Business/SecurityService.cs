using Minesweeper.Models;
using Minesweeper.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minesweeper.Services.Business {
    public class SecurityService {

        private static SecurityDAO Service = new SecurityDAO();

        public void RegisterUser(UserModel user) {
            Service.RegisterUser(user);
        }

        public bool CanRegister(UserModel user) {
            return !Service.CheckIfUserIsRegistered(user);
        }

        public int GetUserId(UserModel user) {
            return Service.GetUserId(user);
        }

        public bool Authenticate(UserModel user) {
            return Service.FindByUser(user);
        }
    }

}
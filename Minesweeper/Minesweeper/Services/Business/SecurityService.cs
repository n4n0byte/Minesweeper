using Minesweeper.Models;
using Minesweeper.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minesweeper.Services.Business {
    public class SecurityService {

        public void RegisterUser(UserModel user) {
            SecurityDAO service = new SecurityDAO();
            service.RegisterUser(user);
        }

        public bool CanRegister(UserModel user) {
            SecurityDAO service = new SecurityDAO();
            return !service.CheckIfUserIsRegistered(user);
        }

        public bool Authenticate(UserModel user) {
            SecurityDAO service = new SecurityDAO();
            return service.FindByUser(user);
        }
    }

}
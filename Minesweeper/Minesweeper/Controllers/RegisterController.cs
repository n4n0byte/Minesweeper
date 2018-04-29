using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Provider;
using Minesweeper.Models;
using Minesweeper.Services.Business;
using Minesweeper.Services.Utility;

namespace Minesweeper.Controllers {

    public class RegisterController : Controller {

        private ILogger Logger;
        private AuthorizationViewModel Model;

        public RegisterController(ILogger logger) {
            Logger = logger;
            Model = new AuthorizationViewModel();            
        }

        /// <summary>
        /// Shows Registration View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index() {
            Logger.Debug($"In {GetType().FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}");

            Logger.Debug("In Register");
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(UserModel user) {
            Logger.Debug($"In {GetType().FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}");

            // tries to register user
            try
            {
                SecurityService service = new SecurityService();

                // checks to see if user info is already in db
                if (service.CanRegister(user))
                {
                    service.RegisterUser(user);
                    return View("RegisterSuccess");
                }
                else {
                    Model.Message = "Someone Already has this username";
                    return View("Register", Model);
                }

            }
            catch (Exception e) {
                Model.Message = "Internal Error";
                Logger.Error(e.ToString());
                return View("Register", Model);
            }

        }
    }
}
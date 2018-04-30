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
        public ActionResult Register(AuthorizationViewModel user) {
            Logger.Debug($"In {GetType().FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}");

            // tries to register user
            try
            {
                SecurityService service = new SecurityService();

                // checks to see if user info is already in db
                if (service.CanRegister(user.Model))
                {
                    service.RegisterUser(user.Model);
                    Model.Message = "Successfully Registered";
                    return View("~/Views/Login/Login.cshtml", Model);
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
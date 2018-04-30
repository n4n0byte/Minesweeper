using Minesweeper.Models;
using Minesweeper.Services.Business;
using System;
using System.Web.Mvc;
using Minesweeper.Services.Utility;

namespace Minesweeper.Controllers {     
    public class LoginController : Controller {

        private ILogger Logger;
        private AuthorizationViewModel Model;

        /// <summary>
        /// Injects a logger, initializes viewmodel member variable
        /// </summary>
        /// <param name="logger"></param>
        public LoginController(LLogger logger) {
            Logger = logger;
            Model = new AuthorizationViewModel();
        }

        /// <summary>
        /// shows view with 
        /// login form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index() {

            Logger.Debug($"In {GetType().FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}");
            return View("Login");
        }

        /// <summary>
        /// Tries to authenticate
        /// user credentials, redirects
        /// to login page if unavailable
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(AuthorizationViewModel user) {

            Logger.Debug($"In {GetType().FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}");
            Logger.Debug($"UserModel : {user.Model}");

            SecurityService service = new SecurityService();

            // try to authenticate user
            try {
                // authenticate user and redirect them
                if (service.Authenticate(user.Model))
                {
                    System.Web.HttpContext.Current.Session["Username"] = user.Model.Username;
                    System.Web.HttpContext.Current.Session["ID"] = service.GetUserId(user.Model);

                    return RedirectToAction("Index", "Game");
                }
                // render error message
                else {
                    Model.Message = "Invalid Credentials";
                    return View("Login", Model);
                }

            }
            catch (Exception e) {
                // log error and render response
                Logger.Error(e.ToString());
                Logger.Info(user.ToString());
                Model.Message = "Internal Error";
                return View("Login", Model);
            }
        }
    }
}
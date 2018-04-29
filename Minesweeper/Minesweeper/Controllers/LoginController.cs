using Minesweeper.Models;
using Minesweeper.Services.Business;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Minesweeper.Services.Data;
using Minesweeper.Services.Utility;
using Newtonsoft.Json;

namespace Minesweeper.Controllers {     
    public class LoginController : Controller {

        private LLogger Logger;

        public LoginController(LLogger logger) {
            Logger = logger;
        }

        /// <summary>
        /// shows view with 
        /// login form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index() {

            Logger.Debug("In {0}", GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            return View("Login");
        }

        /// <summary>
        /// Tries to authenticate
        /// user credentials, redirects
        /// to login page if unavailable
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(UserModel model) {

            Logger.Debug("In {0}", GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);

            SecurityService service = new SecurityService();

            // authenticate user and redirect them
            if (service.Authenticate(model)) {
                System.Web.HttpContext.Current.Session["Username"] = model.Username;
                System.Web.HttpContext.Current.Session["ID"] = service.GetUserId(model);
                
                return RedirectToAction("Index", "Game");
            }

            return RedirectToAction("Index");
        }
    }
}
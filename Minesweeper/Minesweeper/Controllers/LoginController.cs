using Minesweeper.Models;
using Minesweeper.Services.Business;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Minesweeper.Services.Data;

namespace Minesweeper.Controllers {
    public class LoginController : Controller {

        [HttpGet]
        // GET: Login
        public ActionResult Index() {
            GameStateDAO test = new GameStateDAO();

            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(UserModel model) {
            SecurityService service = new SecurityService();

            // authenticate user and redirect them
            if (service.Authenticate(model)) {
                System.Web.HttpContext.Current.Session["Username"] = model.Username;
                return RedirectToAction("Load", "Game");
            }

            return RedirectToAction("Index");
        }
    }
}
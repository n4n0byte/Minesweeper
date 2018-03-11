using Minesweeper.Models;
using Minesweeper.Services.Business;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minesweeper.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        // GET: Login
        public ActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(UserModel model) {
            SecurityService service = new SecurityService();
            if (service.Authenticate(model)) {
                Session["id"] = service.GetUserId(model);
                return RedirectToAction("Index", "Game");
            }

            return View("LoginFailed");
        }
    }
}
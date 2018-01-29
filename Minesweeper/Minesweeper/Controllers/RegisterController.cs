using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Provider;
using Minesweeper.Models;
using Minesweeper.Services.Business;

namespace Minesweeper.Controllers {
    public class RegisterController : Controller {
        [HttpGet]
        public ActionResult Index() {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(UserModel user) {
            SecurityService service = new SecurityService();

            if (service.CanRegister(user)) {
                service.RegisterUser(user);
                return View("RegisterSuccess");
            }
            else {
                return View("RegisterFailed");
            }
        }
    }
}
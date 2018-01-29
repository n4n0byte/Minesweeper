using Minesweeper.Models;
using Minesweeper.Services.Business;
using System;
using System.Collections.Generic;
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
            return service.Authenticate(model) ? View("LoginPassed", model) : View("LoginFailed");
        }
    }
}
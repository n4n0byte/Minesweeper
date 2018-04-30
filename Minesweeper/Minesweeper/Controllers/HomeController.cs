using System.Web.Mvc;

namespace Minesweeper.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index","Login");
        }
    }
}
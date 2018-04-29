using System.Web;
using System.Web.Mvc;
using Minesweeper.Filters;

namespace Minesweeper
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizationFilter());
        }
    }
}

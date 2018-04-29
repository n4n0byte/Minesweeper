using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Minesweeper.Models;

namespace Minesweeper.Filters {

    public class AuthorizationFilter : ActionFilterAttribute {
        
        /// <summary>
        /// Filter to make sure that 
        /// user is logged in before accessing 
        /// the Gameboard
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            string[] controllerNames = {"Login", "Register", "Home"};

            // make sure controller is not excludes
            if (!controllerNames.Contains(controllerName)) {

                // check if user is in session
                if (filterContext.HttpContext.Session["Username"] == null) {
                    var Url = new UrlHelper(filterContext.RequestContext);
                    var url = Url.Action("Index", "Login");
                    filterContext.Result = new RedirectResult(url);
                }

            }
        }
    }
}
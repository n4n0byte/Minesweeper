using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Minesweeper.Models;

namespace Minesweeper.Filters {

    /// <summary>
    /// Filter to make sure that 
    /// user is logged in before accessing 
    /// the Gameboard
    /// </summary>
    public class AuthorizationFilter : ActionFilterAttribute {
        
        /// <summary>
        /// Checks to see if user is logged
        /// in before 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            string[] controllerNames = {"Login", "Register", "Home"};

            // make sure controller is not excludes
            if (!controllerNames.Contains(controllerName)) {

                // check if user is in session
                if (filterContext.HttpContext.Session["Username"] == null) {
                    
                    // if not, redirect them to the login
                    // with a validation message                    
                    filterContext.Result = new ViewResult { ViewName = "~/Views/Login/Login.cshtml", ViewData = new ViewDataDictionary(
                        model: new AuthorizationViewModel() {Message = "Unauthorized, Please Sign in"} 
                    )};
                }

            }
        }
    }
}
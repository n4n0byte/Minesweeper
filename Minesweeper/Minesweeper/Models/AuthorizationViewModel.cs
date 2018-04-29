using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minesweeper.Models
{
    /// <summary>
    /// Used to send messages and show/store Usermodel state
    /// </summary>
    public class AuthorizationViewModel
    {
        public UserModel Model { get; set; }

        public string Message { get; set; } = "";

    }
}
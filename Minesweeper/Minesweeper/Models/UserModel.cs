using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace Minesweeper.Models
{
    public class UserModel
    {
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Username { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Password { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Sex { get; set; }

        [StringLength(2, MinimumLength = 2)]
        [Required]
        public string State { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string LastName { get; set; }

        [Required]
        [Range(10,180)]
        public int Age { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
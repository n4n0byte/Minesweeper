﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;

namespace Minesweeper.Models
{
    public class UserModel
    {

        public int ID { get; set; }

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

        [DisplayName("First Name")]
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string LastName { get; set; }

        [Required]
        [Range(4,100)]
        public int Age { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public override string ToString() {
            string result = "";
            PropertyInfo[] properties = GetType().GetProperties();

            // print each property field
            foreach (var property in properties) {
                result += $"{property.Name} : {property.GetValue(this,null)} \n";
            }

            return result;
        }
    }
}
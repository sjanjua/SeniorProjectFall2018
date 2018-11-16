using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class Administrators
    {
        [Display(Name = "User Name"), Required(ErrorMessage = "Please enter User Name")]
        public String UserName { get; set; }
        [Display(Name = "User Role")]
        public String User_Type { get; set; }
    }
}
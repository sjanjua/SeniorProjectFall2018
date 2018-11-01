using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class SignIn
    {
        [Display(Name = "User Name"), Required(ErrorMessage = "Please enter User Name")]
        public String UserID { get; set; }
        [Display(Name = "Password"), Required(ErrorMessage = "Please enter Password"), DataType(DataType.Password)]
        public String Password_Field { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class Logon
    {
        [ScaffoldColumn(false)]
        public int UserID { get; set; }
        [Display(Name = "User Name"), Required(ErrorMessage = "Please enter User Name")]
        public String UserName { get; set; }
        [Display(Name = "Password"), Required(ErrorMessage = "Please enter Password"), DataType(DataType.Password)]
        public String Password { get; set; }
    }
}
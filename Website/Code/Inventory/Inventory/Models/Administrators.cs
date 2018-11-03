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
        [Display(Name = "First Name")]
        public String First_Name { get; set; }
        [Display(Name = "Last Name")]
        public String Last_Name { get; set; }
        [Display(Name = "User Type")]
        public String User_Type { get; set; }

        [Display(Name = "Maintenance Type")]
        public String Maint_Type { get; set; }

        public int ShipperID { get; set; }
        public String ShipperName { get; set; }
        public String ActiveYN { get; set; }
    }
}
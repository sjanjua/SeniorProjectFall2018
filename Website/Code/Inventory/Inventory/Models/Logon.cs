using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class Logon
    {
        [Display(Name = "User Name"), Required(ErrorMessage = "Please enter User Name")]
        public String UserID { get; set; }
        [Display(Name = "Password"), Required(ErrorMessage = "Please enter Password"), DataType(DataType.Password)]
        public String Password_Field { get; set; }
        public String First_Name { get; set; }
        public String Last_Name { get; set; }
        public String Phone_Number { get; set; }
        public String Street { get; set; }
        public String City { get; set; }
        public String Zip_Code { get; set; }
        public String Email { get; set; }
    }
}
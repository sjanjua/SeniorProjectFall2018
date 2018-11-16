using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace Inventory.Models
{
    public class Users
    {
        [ScaffoldColumn(false)]
        public Int32 UserID { get; set; }
        [Display(Name = "User Name"), Required(ErrorMessage = "Please enter User Name")]
        public String UserName { get; set; }
        [Display(Name = "Password"), Required(ErrorMessage = "Please enter Password"), DataType(DataType.Password)]
        public String Password { get; set; }
        [Display(Name = "Re-Enter Password"), Required(ErrorMessage = "Please re-enter Password"), DataType(DataType.Password)]
        public String Password1 { get; set; }
        [Display(Name = "First Name"), Required(ErrorMessage = "Please enter First Name")]
        public String FirstName { get; set; }
        [Display(Name = "Last Name"), Required(ErrorMessage = "Please enter Last Name")]
        public String LastName { get; set; }
        [Display(Name = "Phone Number"), Required(ErrorMessage = "Please enter Phone Number")]
        public String PhoneNumber { get; set; }
        [Display(Name = "Street"), Required(ErrorMessage = "Please enter Street")]
        public String Street { get; set; }
        [Display(Name = "City"), Required(ErrorMessage = "Please enter City")]
        public String City { get; set; }
        [Display(Name = "Zip Code"), Required(ErrorMessage = "Please enter Zip Code")]
        public String ZipCode { get; set; }
        [Display(Name = "Email"), Required(ErrorMessage = "Please enter Email")]
        public String Email { get; set; }
        [Display(Name = "Role Id")]
        public Int16 RoleID { get; set; }
        public String ActiveYN { get; set; }

    }

    public class DisplayUsers
    {
        [ScaffoldColumn(false)]
        public int UserID { get; set; }
        [DisplayName("Shipper")]
        public string UserName { get; set; }
    }
}
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
        public String UserID { get; set; }
        public String First_Name { get; set; }
        public String Last_Name { get; set; }
        public String Phone_Number { get; set; }
        public String Street { get; set; }
        public String City { get; set; }
        public String Zip_Code { get; set; }
        public String Email { get; set; }

    }
}
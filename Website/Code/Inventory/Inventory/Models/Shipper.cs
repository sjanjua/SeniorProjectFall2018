using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Inventory.Models
{
    public class Shipper
    {
        [ScaffoldColumn(false)]
        public int ShipperID { get; set; }
        [DisplayName("Shipper")]
        public string ShipperName { get; set; }
        [DisplayName("Phone No")]
        public string Phone { get; set; }
        [DisplayName("Active")]
        public string ActiveYN { get; set; }
    }

    public class DisplayShipper
    {
        [ScaffoldColumn(false)]
        public int ShipperID { get; set; }
        [DisplayName("Shipper")]
        public string ShipperName { get; set; }
    }

    public class CreateShipper
    {
        [ScaffoldColumn(false)]
        [Display(Name = "Shipper"), Required(ErrorMessage = "Please select SHipper")]
        public int ShipperID { get; set; }
        [Display(Name = "Phone No"), DataType(DataType.PhoneNumber), Required(ErrorMessage = "Please enter Phone Number")]
        public DateTime Phone { get; set; }
        [Display(Name = "Active")]
        public decimal ActiveYN { get; set; }

        public List<DisplayShipper> ShipperList { get; set; }
    }
}
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
        public String Phone { get; set; }
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
        
}
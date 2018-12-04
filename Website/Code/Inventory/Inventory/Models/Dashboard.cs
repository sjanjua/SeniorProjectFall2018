using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class Dashboard
    {
        public int OrderID { get; set; }
        [Display(Name = "Order Date"), DataType(DataType.Date)]
        public Nullable<DateTime> OrderDate { get; set; }
        [Display(Name = "Shipped Date"), DataType(DataType.Date)]
        public Nullable<DateTime> ShippedDate { get; set; }
        [Display(Name = "Shipped To")]
        public string ShippedName { get; set; }
    }
}
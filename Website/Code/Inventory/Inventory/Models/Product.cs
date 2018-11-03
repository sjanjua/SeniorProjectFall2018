using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class Product
    {
        [ScaffoldColumn(false)]
        public int ProductID { get; set; }
        [ScaffoldColumn(false)]
        public int CategoryID { get; set; }
        [ScaffoldColumn(false)]
        public int SupplierID { get; set; }
        [Display(Name = "Name")]
        public string ProductName { get; set; }
        [Display(Name = "Quantity per unit")]
        public string QuantityPerUnit{ get; set; }
        [Display(Name ="Unit Price")]
        public decimal UnitPrice { get; set; }
        [Display(Name = "Units in Stock")]
        public short UnitsInStock { get; set; }
        [Display(Name = "Units on Order")]
        public short UnitsOnOrder { get; set; }
        [Display(Name = "ReOrder Level")]
        public short ReOrderLevel { get; set; }
        [Display(Name = "Active")]
        public string ActiveYN { get; set; }
    }

    public class DisplayProduct
    {
        [ScaffoldColumn(false)]
        public int ProductID { get; set; }
        [Display(Name = "Name")]
        public string ProductName { get; set; }
        [Display(Name = "Quantity per unit")]
        public string QuantityPerUnit { get; set; }
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }
        [Display(Name = "Units in Stock")]
        public short UnitsInStock { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class Orders
    {
        [ScaffoldColumn(false)]
        public int OrderID { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name = "Customer"), Required(ErrorMessage = "Please select Customer")]
        public string CustomerID { get; set; }
        [ScaffoldColumn(false)]
        public int UserID { get; set; }
        [ScaffoldColumn(false)]
        public int ShipperID { get; set; }
        [Display(Name = "Order Date")]
        public Nullable<DateTime> OrderDate { get; set; }
        [Display(Name = "Required Date")]
        public Nullable<DateTime> RequiredDate { get; set; }
        [Display(Name = "Shipped Date")]
        public Nullable<DateTime> ShippedDate { get; set; }
        [Display(Name = "Shipped To")]
        public string ShippedName { get; set; }
        [Display(Name = "Shipped Address")]
        public string ShippedAddress { get; set; }
        [Display(Name = "Shipped City")]
        public string ShippedCity { get; set; }
        [Display(Name = "Shipped Region")]
        public string ShippedRegion { get; set; }
        [Display(Name = "Freight")]
        public decimal Freight { get; set; }

        public List<OrderDetails> Details { get; set; }
        public List<DisplayCustomer> CustomerList { get; set; }
        public List<DisplayShipper> ShipperList { get; set; }
        public List<DisplayProduct> ProductList { get; set; }

    }

    public class OrderDetails
    {
        [ScaffoldColumn(false)]
        public int OrderID { get; set; }
        [ScaffoldColumn(false)]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public float UnitPrice { get; set; }
        public Int16 Quantity { get; set; }
        public decimal Discount { get; set; }
    }

    public class DisplayOrder
    {
        [ScaffoldColumn(false)]
        public int OrderID { get; set; }
        [Display(Name = "Order Date")]
        public Nullable<DateTime> OrderDate { get; set; }
        [Display(Name = "Required Date")]
        public Nullable<DateTime> RequiredDate { get; set; }
        [Display(Name = "Shipped Date")]
        public Nullable<DateTime> ShippedDate { get; set; }
        [Display(Name = "Shipped To")]
        public string ShippedName { get; set; }
        [Display(Name = "Shipped Address")]
        public string ShippedAddress { get; set; }
        [Display(Name = "Shipped City")]
        public string ShippedCity { get; set; }
        [Display(Name = "Shipped Region")]
        public string ShippedRegion { get; set; }
        [Display(Name ="Freight")]
        public decimal Freight { get; set; }
    }
}
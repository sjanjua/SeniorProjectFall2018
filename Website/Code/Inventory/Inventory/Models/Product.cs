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

    public class ProductDetails
    {
        [ScaffoldColumn(false)]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public float UnitPrice { get; set; }
        public Int16 Quantity { get; set; }
        public decimal Discount { get; set; }
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

    public class CreateProduct
    {
        [ScaffoldColumn(false)]
        [Display(Name = "Customer"), Required(ErrorMessage = "Please select Customer")]
        public string CustomerID { get; set; }
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

    public class DisplayProductDetails
    {
        [ScaffoldColumn(false)]
        public int ProductID { get; set; }

        public List<ProductDetails> Details { get; set; }
    }

    public class CreateProductDetail
    {
        [ScaffoldColumn(false)]
        [Display(Name = "Product"), Required(ErrorMessage = "Please select Product")]
        public int ProductID { get; set; }
        [Display(Name = "Quantity"), Required(ErrorMessage = "Please enter Quantity")]
        public Int16 Quantity { get; set; }
        [Display(Name = "Unit Price"), Required(ErrorMessage = "Please enter Unit Price")]
        public float UnitPrice { get; set; }

        public List<DisplayProduct> ProductList { get; set; }
    }
}
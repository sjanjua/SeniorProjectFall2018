using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Inventory.Models
{
    public class Supplier
    {
        [ScaffoldColumn(false)]
        [DisplayName("Supplier Id")]
        public int SupplierID { get; set; }
        [DisplayName("CompanyName")]
        public string CompanyName { get; set; }
        [DisplayName("ContactName")]
        public string ContactName { get; set; }
        [DisplayName("ContactTitle")]
        public string ContactTitle { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("City")]
        public string City { get; set; }
        [DisplayName("Region")]
        public string Region { get; set; }
        [DisplayName("PostalCode")]
        public string PostalCode { get; set; }
        [DisplayName("Country")]
        public string Country { get; set; }
        [DisplayName("Phone No")]
        public string Phone { get; set; }
        [DisplayName("Fax")]
        public string Fax { get; set; }
    }

    public class DisplaySupplier
    {
        [ScaffoldColumn(false)]
        public int SupplierID { get; set; }
        [DisplayName("Supplier")]
        public string CompanyName { get; set; }
    }

}
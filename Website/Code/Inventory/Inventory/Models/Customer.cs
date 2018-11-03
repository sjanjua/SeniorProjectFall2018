using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class Customer
    {
        [Display(Name ="Customer ID")]
        [StringLength(5,MinimumLength =5, ErrorMessage = "Customer ID must be 5 characters long")]
        public string CustomerID { get; set; }
        [Display(Name ="Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }
        [Display(Name = "Contact Title")]
        public string ContactTitle { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "Region")]
        public string Region { get; set; }
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Display(Name = "Fax")]
        public string Fax { get; set; }
    }

    public class DisplayCustomer
    {
        [Display(Name = "Customer ID")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Customer ID must be 5 characters long")]
        public string CustomerID { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
    }
}
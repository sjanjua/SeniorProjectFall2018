using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class Dashboard
    {
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal OrderTotal { get; set; }        
        public int OrdersInMonth { get; set; }        
        public int ProductsReorder { get; set; }        
    }
}
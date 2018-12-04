using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class SearchString
    {
        public string Query { get; set; }
    }

    public class SearchQueryResponse
    {
        public string RawJSON { get; set; }
        public string SearchQuery { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Server.LUIS
{
    public class LUISResponse
    {
        public string query { get; set; }
        public TopScoringIntent topScoringIntent { get; set; }
        public Entities[] entities { get; set; }
    }
    public class TopScoringIntent
    {
        public string intent { get; set; }
        public string score { get; set; }
    }
    public class Value
    {
        public string timex { get; set; }
        public string type { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string value { get; set; }
    }

    public class Entities
    {
        public string entity { get; set; }
        public string type { get; set; }
        public string startIndex { get; set; }
        public string endIndex { get; set; }
        public string score { get; set; }
        public string role { get; set; }
        public Resolution resolution { get; set; }
    }

    public class Resolution
    {
        public string subtype { get; set; }
        public string value { get; set; }
        public Value[] values { get; set; }
    }  
}
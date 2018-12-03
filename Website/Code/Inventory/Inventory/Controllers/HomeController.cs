using Inventory.Models;
using Inventory.Server.LUIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowChart()
        {
            return PartialView("ChartView");
        }

        [HttpPost]
        public ActionResult Search(string searchString)
        {
            SearchQueryResponse resp =  LUISAdapter.GetSearchQuery(searchString);
            
            return View(resp);
        }
    }
}
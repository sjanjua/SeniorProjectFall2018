using Inventory.DataLayer.Repository;
using Inventory.Models;
using Inventory.Server.LUIS;
using Inventory.Utils;
using MySql.Data.MySqlClient;
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
            Dashboard dash;
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                DashboardRepository repo = new DashboardRepository(conn);
                dash = repo.GetData();
            }

            return View(dash);
        }

        public ActionResult ShowChart()
        {
            IEnumerable<Chart> chartData;
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                ChartRepository repo = new ChartRepository(conn);
                chartData = repo.GetAll();
            }
            return PartialView("ChartView", chartData);
        }

        [HttpPost]
        public ActionResult Search(string searchString)
        {
            SearchQueryResponse resp =  LUISAdapter.GetSearchQuery(searchString);
            
            return View(resp);
        }
    }
}
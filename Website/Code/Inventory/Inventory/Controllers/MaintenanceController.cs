using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Utils;
using Inventory.Security;
using Inventory.Models;
using Inventory.DataLayer.Repository;

namespace Inventory.Controllers
{
    [Authorize]
    public class MaintenanceController : Controller
    {
        
        public ActionResult Shippers()
        {
            List<Shipper> shippers = new List<Shipper>();
            using (MySqlConnection conn = DBUtils.GetConnection()) {
                ShipperRepository repo = new ShipperRepository(conn);
                shippers = repo.GetAll().ToList<Shipper>();
            }
            return View(shippers);
        }

        public ActionResult Shipper(Int32 id) {
            Shipper shipper;
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                ShipperRepository repo = new ShipperRepository(conn);
                shipper = repo.GetById(id.ToString());
            }

            return View(shipper);
        }
    }
}
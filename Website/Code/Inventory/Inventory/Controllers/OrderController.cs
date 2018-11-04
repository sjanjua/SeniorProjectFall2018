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
    public class OrderController : Controller
    {
        
        public ActionResult Orders()
        {
            List<Orders> orders = new List<Orders>();
            using (MySqlConnection conn = DBUtils.GetConnection()) {
                OrderRepository repo = new OrderRepository(conn);
                orders = repo.GetAll().ToList<Orders>();
            }
            return View(orders);
        }

        public ActionResult Order(Int32 id) {
            Orders order;
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                OrderRepository repo = new OrderRepository(conn);
                order = repo.GetById(id);
            }

            return View(order);
        }
    }
}
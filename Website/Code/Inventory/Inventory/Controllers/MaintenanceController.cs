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

        public ActionResult PartialShippers()
        {
            List<Shipper> shippers = new List<Shipper>();
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                ShipperRepository repo = new ShipperRepository(conn);
                shippers = repo.GetAll().ToList<Shipper>();
            }
            return PartialView("Shippers", shippers);
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

        public ActionResult Suppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                SupplierRepository repo = new SupplierRepository(conn);
                suppliers = repo.GetAll().ToList<Supplier>();
            }
            return View(suppliers);
        }

        public ActionResult Supplier(Int32 id)
        {
            Supplier supplier;
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                SupplierRepository repo = new SupplierRepository(conn);
                supplier = repo.GetById(id.ToString());
            }

            return View(supplier);
        }

        public ActionResult Customers()
        {
            List<Customer> customers = new List<Customer>();
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                CustomerRepository repo = new CustomerRepository(conn);
                customers = repo.GetAll().ToList<Customer>();
            }
            return View(customers);
        }

        public ActionResult Customer(Int32 id)
        {
            Customer customer;
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                CustomerRepository repo = new CustomerRepository(conn);
                customer = repo.GetById(id.ToString());
            }

            return View(customer);
        }
    }
}
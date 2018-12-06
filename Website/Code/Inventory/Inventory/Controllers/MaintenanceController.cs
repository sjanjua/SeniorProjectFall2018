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

        [HttpPost]
        public ActionResult Shipper(Shipper ship)
        {
            if (ModelState.IsValid)
            {
                using (MySqlConnection conn = DBUtils.GetConnection())
                {
                    ShipperRepository repo = new ShipperRepository(conn);
                    repo.Save(ship);
                }

                return RedirectToAction("Shippers");
            }
            return View(ship);
        }

        public ActionResult CreateShipper()
        {
            Shipper ship = new Shipper();

            return View(ship);
        }

        [HttpPost]
        public ActionResult CreateShipper(Shipper ship)
        {
            if (ModelState.IsValid)
            {
                using (MySqlConnection conn = DBUtils.GetConnection())
                {
                    ShipperRepository repo = new ShipperRepository(conn);
                    repo.Save(ship);
                }

                return RedirectToAction("Shippers");
            }
            return View(ship);
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

        public ActionResult Customer(string id)
        {
            Customer customer;
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                CustomerRepository repo = new CustomerRepository(conn);
                customer = repo.GetById(id);
            }

            return View(customer);
        }

        public ActionResult Products()
        {
            List<Product> products = new List<Product>();
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                ProductRepository repo = new ProductRepository(conn);
                products = repo.GetAll().ToList<Product>();
            }
            return View(products);
        }

        public ActionResult Product(Int32 id)
        {
            Product product;
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                ProductRepository repo = new ProductRepository(conn);
                product = repo.GetById(id.ToString());
            }

            return View(product);
        }
    }
}
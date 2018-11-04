using Inventory.DataLayer.Repository;
using Inventory.Models;
using Inventory.Security;
using Inventory.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class AdministratorsController : Controller
    {
        SessionContext context = new SessionContext();

        public ActionResult Administrator()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangeRole(Administrators admin)
        {
            if (admin.UserName != null)
            {
                using (MySqlConnection conn = DBUtils.GetConnection())
                {
                    UsersRepository repo = new UsersRepository(conn);
                    Users user = repo.GetByName(admin.UserName);
                    if (user != null) { 
                    repo.changeUserRole(admin.UserName, admin.User_Type[0]);
                }
                }
            }
            return View("Administrator");
        }

        [HttpPost]
        public ActionResult RemoveShipper(Administrators admin)
        {
            Shipper shipper = null;
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                ShipperRepository repo = new ShipperRepository(conn);
                shipper = repo.GetById(admin.ShipperID.ToString());
                if (shipper != null)
                {
                    repo.Delete(shipper.ShipperID.ToString());
                }
            }
            return View("Administrator");
        }

        [HttpPost]
        public ActionResult RemoveSupplier(Administrators admin)
        {
            Supplier supplier = null;
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                SupplierRepository repo = new SupplierRepository(conn);
                supplier = repo.GetById(admin.ShipperID.ToString());
                if (supplier != null)
                {
                    repo.Delete(supplier.SupplierID.ToString());
                }
            }
            return View("Administrator");
        }

        [HttpPost]
        public ActionResult RemoveCustomer(Administrators admin)
        {
            Customer customer = null;
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                CustomerRepository repo = new CustomerRepository(conn);
                customer = repo.GetById(admin.ShipperID.ToString());
                if (customer != null)
                {
                    repo.Delete(customer.CustomerID.ToString());
                }
            }
            return View("Administrator");
        }

    }
}
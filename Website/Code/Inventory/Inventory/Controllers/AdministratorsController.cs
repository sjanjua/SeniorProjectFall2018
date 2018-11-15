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
    [Authorize]
    public class AdministratorsController : Controller
    {
        SessionContext context = new SessionContext();

        public ActionResult Administrator()
        {
            return View();
        }

        public ActionResult Users()
        {
            List<Users> users = new List<Users>();
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                UsersRepository repo = new UsersRepository(conn);
                users = repo.GetAll().ToList<Users>();
            }
            return PartialView("Users", users);
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
                        switch (admin.User_Type) {
                            case "Administrator":
                                repo.changeUserRole(admin.UserName, "1");
                                break;
                            case "Manager":
                                repo.changeUserRole(admin.UserName, "2");
                                break;
                            case "Cashier":
                                repo.changeUserRole(admin.UserName, "3");
                                break;
                        }
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

    }
}
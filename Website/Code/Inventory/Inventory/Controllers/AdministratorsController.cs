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

        public ActionResult Edit(Int32 userID)
        {
            Users users = null;
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                UsersRepository repo = new UsersRepository(conn);
                users = repo.GetById(userID.ToString());
            }
            if (users != null)
            {
                Administrators admin = new Administrators();
                admin.UserName = users.UserName;
                switch (users.RoleID)
                {
                    case 1:
                        admin.User_Type = "Administrator";
                        break;
                    case 2:
                        admin.User_Type = "Manager";
                        break;
                    case 3:
                        admin.User_Type = "Cashier";
                        break;
                }
                return View("Administrator", admin);
            }
            else
            {
                return View();
            }
        }

        public ActionResult ChangeUsers()
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
    }
}
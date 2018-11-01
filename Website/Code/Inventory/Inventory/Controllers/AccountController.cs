using Inventory.Models;
using Inventory.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.DataLayer.Repository;

namespace Inventory.Controllers
{
    
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login(String userNameField, String password)
        {
            ViewBag.Message = "Your Login page.";
            return View(userNameField, password);
        }

        public ActionResult SignUp()
        {
            ViewBag.Message = "Sign Up page.";
            return View();
        }

        public ActionResult ValidateLogin(String userNameField, String Password)
        {
            Users users = null;
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                UsersRepository repo = new UsersRepository(conn);
                users = repo.GetById(userNameField);
            }
            if (users.Password.Equals(Password))
            {
                return View();
            }
            return null;
        }
    }
}
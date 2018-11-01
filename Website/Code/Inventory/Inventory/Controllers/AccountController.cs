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

        [HttpPost]
        public ActionResult ValidateLogin(Users user)
        {
            if (ModelState.IsValid)
            {
                Users users = null;
                using (MySqlConnection conn = DBUtils.GetConnection())
                {
                    UsersRepository repo = new UsersRepository(conn);
                    users = repo.GetById(user.UserID);
                }
                if (users.Password.Equals(user.Password))
                {
                    return RedirectToAction("Index", "Home");
                }
                else {
                    ModelState.AddModelError(string.Empty, "Invalid Login Information.");
                    return View("Login", user);
                }
            }

            return View("Login", user);
        }
    }
}
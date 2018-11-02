using Inventory.Models;
using Inventory.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.DataLayer.Repository;
using Inventory.Security;
using System.Web.Security;

namespace Inventory.Controllers
{
    
    public class AccountController : Controller
    {
        SessionContext context = new SessionContext();

        // GET: Account
        public ActionResult Login()
        {
            ViewBag.Message = "Your Login page.";
            return View();
        }

        public ActionResult SignUp()
        {
            ViewBag.Message = "Sign Up page.";
            return View();
        }

        [HttpPost]
        public ActionResult ValidateLogin(Logon user)
        {
            if (ModelState.IsValid)
            {
                Logon users = null;
                using (MySqlConnection conn = DBUtils.GetConnection())
                {
                    LogonRepository repo = new LogonRepository(conn);
                    users = repo.GetById(user.UserID);
                }
                if (users.Password_Field.Equals(user.Password_Field))
                {
                    context.SetAuthenticationToken(user.UserID.ToString(), false, user);
                    return RedirectToAction("Index", "Home");
                }
                else {
                    ModelState.AddModelError(string.Empty, "Invalid Login Information.");
                    return View("Login", user);
                }
            }

            return View("Login", user);
        }

        [HttpPost]
        public ActionResult AddUser(Users user)
        {
            if (!user.Password_Field.Equals(user.Password_Field1))
            {
                ModelState.AddModelError("Password_Field", "The passwords entered do not match");
                return View("SignUp", user);
            }
            else
            {
                Logon logIn = null;
                if (ModelState.IsValid)
                {
                    using (MySqlConnection conn = DBUtils.GetConnection())
                    {
                        UsersRepository repo = new UsersRepository(conn);
                        Dictionary<String, Object> hash = new Dictionary<String, Object>();
                        hash.Add("UserID", user.UserID);
                        hash.Add("Password_Field", user.Password_Field);
                        hash.Add("First_Name", user.First_Name);
                        hash.Add("Last_Name", user.Last_Name);
                        hash.Add("Phone_Number", user.Phone_Number);
                        hash.Add("Street", user.Street);
                        hash.Add("City", user.City);
                        hash.Add("Zip_Code", user.Zip_Code);
                        hash.Add("Email", user.Email);
                        repo.SetAll(hash);
                        LogonRepository repo1 = new LogonRepository(conn);
                        logIn = repo1.GetById(user.UserID);
                    }
                }
                if (logIn != null)
                {
                    return View("Login", logIn);
                }
                else
                {
                    return View("SignUp");
                }
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
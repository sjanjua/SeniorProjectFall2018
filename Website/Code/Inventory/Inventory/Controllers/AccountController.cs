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
                    users = repo.GetByName(user.UserName);
                }
                if (users.Password.Equals(user.Password))
                {
                    context.SetAuthenticationToken(user.UserName.ToString(), false, user);
                    Session["UserID"] = users.UserID;
                    return RedirectToAction("Index", "Home");
                }
                else {
                    ModelState.AddModelError(string.Empty, "Invalid Login Information.");
                    Session["UserID"] = null;
                    return View("Login", user);
                }
            }
            return View("Login", user);
        }

        [HttpPost]
        public ActionResult AddUser(Users user)
        {
            Logon logIn = null;
            if (!user.Password.Equals(user.Password1))
            {
                ModelState.AddModelError("Password", "The passwords entered do not match");
                return View("SignUp", user);
            }
            else
            {
                using (MySqlConnection conn = DBUtils.GetConnection())
                {
                    UsersRepository repo = new UsersRepository(conn);
                    Users isNew = repo.GetByName(user.UserName);
                    if (isNew != null)
                    {
                        ModelState.AddModelError("UserName", "This User Name Already Exists");
                        return View("SignUp", user);
                    }
                    else
                    {
                    if (ModelState.IsValid)
                    {                       
                            Dictionary<String, Object> hash = new Dictionary<String, Object>();
                            hash.Add("UserName", user.UserName);
                            hash.Add("Password", user.Password);
                            hash.Add("FirstName", user.FirstName);
                            hash.Add("LastName", user.LastName);
                            hash.Add("PhoneNumber", user.PhoneNumber);
                            hash.Add("Street", user.Street);
                            hash.Add("City", user.City);
                            hash.Add("ZipCode", user.ZipCode);
                            hash.Add("Email", user.Email);
                            repo.SetAll(hash);
                            LogonRepository repo1 = new LogonRepository(conn);
                            logIn = repo1.GetByName(user.UserName);
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
            return RedirectToAction("Error");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class AccountController : Controller
    {
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

    }
}
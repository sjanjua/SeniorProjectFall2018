using Inventory.DataLayer.Repository;
using Inventory.Models;
using Inventory.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            List<Orders> orders = new List<Orders>();
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                OrderRepository repo = new OrderRepository(conn);
                orders = repo.GetAll().ToList<Orders>();
            }
            return View(orders);
        }
        

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            Orders order;
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                OrderRepository repo = new OrderRepository(conn);
                order = repo.GetById(id);
            }

            return View(order);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

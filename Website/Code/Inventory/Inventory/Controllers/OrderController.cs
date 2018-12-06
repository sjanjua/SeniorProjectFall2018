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
            List<DisplayOrder> orders = new List<DisplayOrder>();
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                DisplayOrderHistoryRepository repo = new DisplayOrderHistoryRepository(conn);
                orders = repo.GetAll().ToList<DisplayOrder>();
            }
            return View(orders);
        }

        [HttpPost]
        public ActionResult Index(string searchString)
        {
            List<DisplayOrder> orders = new List<DisplayOrder>();
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                DisplayOrderHistoryRepository repo = new DisplayOrderHistoryRepository(conn);
                orders = repo.GetByQuery(searchString).ToList<DisplayOrder>();
            }
            return PartialView("Orders", orders);
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
            CreateOrder order = new CreateOrder();

            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                DisplayCustomerRepository cust = new DisplayCustomerRepository(conn);
                order.CustomerList = cust.GetAll().ToList<DisplayCustomer>();

                DisplayShipperRepository shipper = new DisplayShipperRepository(conn);
                order.ShipperList = shipper.GetAll().ToList<DisplayShipper>();

                //DisplayProductRepository product = new DisplayProductRepository(conn);
                //order.ProductList = product.GetAll().ToList<DisplayProduct>();

            }

            return View(order);
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(CreateOrder order)
        {

            if (ModelState.IsValid)
            {
                Orders newOrder = new Orders();
                Int32 newOrderID = 0;
                using (MySqlConnection conn = DBUtils.GetConnection())
                {
                    CustomerRepository custRepo = new CustomerRepository(conn);
                    Customer cust = custRepo.GetById(order.CustomerID);
                    Logon user = (Logon)Session["User"];


                    newOrder.CustomerID = order.CustomerID;
                    newOrder.ShipperID = order.ShipperID;
                    newOrder.OrderDate = order.OrderDate;
                    newOrder.RequiredDate = order.RequiredDate;
                    newOrder.Freight = order.Freight;
                    newOrder.UserID = user.UserID;

                    newOrder.ShippedName = cust.CompanyName;
                    newOrder.ShippedAddress = cust.Address;
                    newOrder.ShippedCity = cust.City;
                    newOrder.ShippedRegion = cust.Region;
                    newOrder.ShippedPostalCode = cust.PostalCode;
                    newOrder.ShippedCountry = cust.Country;

                    OrderRepository orderRepo = new OrderRepository(conn);
                    newOrderID = orderRepo.Save(newOrder);
                }

                return RedirectToAction("Details", new { id = newOrderID });
            }

            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                DisplayCustomerRepository cust = new DisplayCustomerRepository(conn);
                order.CustomerList = cust.GetAll().ToList<DisplayCustomer>();

                DisplayShipperRepository shipper = new DisplayShipperRepository(conn);
                order.ShipperList = shipper.GetAll().ToList<DisplayShipper>();

                //DisplayProductRepository product = new DisplayProductRepository(conn);
                //order.ProductList = product.GetAll().ToList<DisplayProduct>();

            }

            return View("Create", order);

        }

        public ActionResult ShipOrder(int id)
        {
            Orders order;
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                OrderRepository repo = new OrderRepository(conn);
                order = repo.GetById(id);

            }

            EditOrder ship = new EditOrder();
            ship.OrderID = order.OrderID;
            ship.OrderDate = order.OrderDate;
            ship.RequiredDate = order.RequiredDate;
            ship.ShippedName = order.ShippedName;
            return View(ship);
        }

        [HttpPost]
        public ActionResult ShipOrder(EditOrder ship)
        {
            if (ModelState.IsValid)
            {
                using (MySqlConnection conn = DBUtils.GetConnection())
                {
                    Orders order = new Orders();

                    order.OrderID = ship.OrderID;
                    order.ShippedDate = ship.ShippedDate;

                    OrderRepository repo = new OrderRepository(conn);
                    repo.Save(order);
                }

                return RedirectToAction("Index");
            }

            return View(ship);
        }
        
        public ActionResult DeleteOrder(int orderId)
        {
            try
            {
                // TODO: Add delete logic here
                using (MySqlConnection conn = DBUtils.GetConnection())
                {
                    OrderRepository orderRepo = new OrderRepository(conn);
                    orderRepo.Delete(orderId);
                }               

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult OrderDetails(int id)
        {
            List<OrderDetails> orderdet;
            DisplayOrderDetails displayDet = new DisplayOrderDetails();
            displayDet.OrderID = id;

            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                OrderDetailsRepository repo = new OrderDetailsRepository(conn);
                orderdet = repo.GetById(id).ToList<OrderDetails>();
            }

            displayDet.Details = orderdet;
            return View(displayDet);
        }

        // GET: Order/Create
        public ActionResult CreateOrderDetail(int id)
        {
            CreateOrderDetail order = new CreateOrderDetail();
            order.OrderID = id;
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                DisplayProductRepository product = new DisplayProductRepository(conn);
                order.ProductList = product.GetAll().ToList<DisplayProduct>();

            }

            return View(order);
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult CreateOrderDetail(int id, CreateOrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                using (MySqlConnection conn = DBUtils.GetConnection())
                {
                    OrderDetails orderdet = new OrderDetails();

                    orderdet.OrderID = orderDetail.OrderID;
                    orderdet.ProductID = orderDetail.ProductID;
                    orderdet.Quantity = orderDetail.Quantity;
                    orderdet.UnitPrice = orderDetail.UnitPrice;
                    orderdet.Discount = 0;
                    
                    OrderDetailsRepository orderRepo = new OrderDetailsRepository(conn);
                    orderRepo.Save(orderdet);
                }

                return RedirectToAction("OrderDetails", new { id = orderDetail.OrderID });
            }

            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                DisplayProductRepository product = new DisplayProductRepository(conn);
                orderDetail.ProductList = product.GetAll().ToList<DisplayProduct>();

            }

            return View(orderDetail);
        }
              
        public ActionResult DeleteOrderDetails(int orderId, int productId)
        {
            try
            {
                // TODO: Add delete logic here
                using (MySqlConnection conn = DBUtils.GetConnection())
                {
                    OrderDetailsRepository orderRepo = new OrderDetailsRepository(conn);
                    orderRepo.Delete(orderId, productId);
                }

                return RedirectToAction("OrderDetails", new { id = orderId });
                
            }
            catch
            {
                return RedirectToAction("OrderDetails", new { id = orderId });
            }
        }
    }
}

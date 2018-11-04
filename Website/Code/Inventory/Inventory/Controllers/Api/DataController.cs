using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Inventory.DataLayer.Repository;
using Inventory.Models;
using Inventory.Utils;
using MySql.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace Inventory.ApiControllers
{
    public class DataController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Shippers()
        {
            try
            {
                List<DisplayShipper> shippers = new List<DisplayShipper>();
                using (MySqlConnection conn = DBUtils.GetConnection())
                {
                    DisplayShipperRepository repo = new DisplayShipperRepository(conn);
                    shippers = repo.GetAll().ToList<DisplayShipper>();
                }

                return Request.CreateResponse(HttpStatusCode.OK, shippers);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Suppliers()
        {
            try
            {
                List<DisplaySupplier> suppliers = new List<DisplaySupplier>();
                using (MySqlConnection conn = DBUtils.GetConnection())
                {
                    DisplaySupplierRepository repo = new DisplaySupplierRepository(conn);
                    suppliers = repo.GetAll().ToList<DisplaySupplier>();
                }

                return Request.CreateResponse(HttpStatusCode.OK, suppliers);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Customers()
        {
            try
            {
                List<DisplayCustomer> customers = new List<DisplayCustomer>();
                using (MySqlConnection conn = DBUtils.GetConnection())
                {
                    DisplayCustomerRepository repo = new DisplayCustomerRepository(conn);
                    customers = repo.GetAll().ToList<DisplayCustomer>();
                }

                return Request.CreateResponse(HttpStatusCode.OK, customers);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage Products()
        {
            try
            {
                List<DisplayProduct> products = new List<DisplayProduct>();
                using (MySqlConnection conn = DBUtils.GetConnection())
                {
                    DisplayProductRepository repo = new DisplayProductRepository(conn);
                    products = repo.GetAll().ToList<DisplayProduct>();
                }

                return Request.CreateResponse(HttpStatusCode.OK, products);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage Orders()
        {
            try
            {
                List<DisplayOrder> orders = new List<DisplayOrder>();
                using (MySqlConnection conn = DBUtils.GetConnection())
                {
                    DisplayOrderRepository repo = new DisplayOrderRepository(conn);
                    orders = repo.GetAll().ToList<DisplayOrder>();
                }

                return Request.CreateResponse(HttpStatusCode.OK, orders);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
        }
    }
}

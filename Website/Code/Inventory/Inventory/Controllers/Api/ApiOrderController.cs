using Inventory.DataLayer.Repository;
using Inventory.Models;
using Inventory.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Inventory.Controllers.Api
{
    public class OrderController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage Get()
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

        // GET api/<controller>/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Orders order = new Orders();
                using (MySqlConnection conn = DBUtils.GetConnection())
                {
                    OrderRepository repo = new OrderRepository(conn);
                    order = repo.GetById(id);

                    OrderDetailsRepository detailsRepo = new OrderDetailsRepository(conn);
                    order.Details = detailsRepo.GetById(id).ToList<OrderDetails>();
                }

                return Request.CreateResponse(HttpStatusCode.OK, order);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
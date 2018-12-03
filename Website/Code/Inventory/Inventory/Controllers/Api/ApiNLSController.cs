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
    public class NLSController : ApiController
    {
        public HttpResponseMessage Post([FromBody]string searchString)
        {
            try
            {
                List<DisplayOrder> orders = new List<DisplayOrder>();
                using (MySqlConnection conn = DBUtils.GetConnection())
                {
                    DisplayOrderHistoryRepository repo = new DisplayOrderHistoryRepository(conn);
                    orders = repo.GetByQuery(searchString).ToList<DisplayOrder>();
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
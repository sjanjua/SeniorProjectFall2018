using Inventory.DataLayer.Repository;
using Inventory.Models;
using Inventory.Server.LUIS;
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
        public HttpResponseMessage Post(SearchString searchString)
        {
            try
            {
                SearchQueryResponse resp = LUISAdapter.GetSearchQuery(searchString.Query);

                if (!String.IsNullOrEmpty(resp.SearchQuery))
                {
                    List<DisplayOrder> orders = new List<DisplayOrder>();
                    using (MySqlConnection conn = DBUtils.GetConnection())
                    {
                        DisplayOrderHistoryRepository repo = new DisplayOrderHistoryRepository(conn);
                        orders = repo.GetByQuery(resp.SearchQuery).ToList<DisplayOrder>();
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, orders);
                }

                return Request.CreateResponse(HttpStatusCode.OK, new List<DisplayOrder>());
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
        }
    }
}
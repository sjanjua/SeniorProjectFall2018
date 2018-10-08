using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using MySql.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace Inventory.Controllers
{
    public class DataController : ApiController
    {
        // GET: api/Data
        public HttpResponseMessage GetAll()
        {
            MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["invDB"].ConnectionString);

            try
            {

                string sqlCmd = "select * from shippers";

                MySqlDataAdapter adr = new MySqlDataAdapter(sqlCmd, cn);
                adr.SelectCommand.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                adr.Fill(dt); //opens and closes the DB connection automatically !! (fetches from pool)

                DataSet dsData = new DataSet();
                dsData.Tables.Add(dt);

                return Request.CreateResponse(HttpStatusCode.OK, dsData);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
            finally
            {
                cn.Dispose(); // return connection to pool
            }
        }

        // GET: api/Data/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Data
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Data/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Data/5
        public void Delete(int id)
        {
        }
    }
}

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

namespace Inventory.APIControllers
{
    public class AccountController : ApiController
    {
        public HttpResponseMessage Post(Logon logon)
        {
           
            Logon users = null;
            using (MySqlConnection conn = DBUtils.GetConnection())
            {
                LogonRepository repo = new LogonRepository(conn);
                users = repo.GetByName(logon.UserName);
            }
            if (users.Password.Equals(logon.Password))
            {
                
                return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Success" }); 
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Failure" });
            }
            
        }
    }
}

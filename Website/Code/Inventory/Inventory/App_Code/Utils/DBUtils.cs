using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Inventory.Utils
{
    public class DBUtils
    {
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConfigurationManager.ConnectionStrings["invDB"].ConnectionString);
        }
    }
}
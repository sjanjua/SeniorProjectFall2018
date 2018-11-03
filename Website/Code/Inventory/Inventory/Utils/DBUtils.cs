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

        public static string GetString(MySqlDataReader reader, string colName)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(colName)))
                return reader.GetString(colName);
            return string.Empty;
        }

        public static Nullable<DateTime> GetDate(MySqlDataReader reader, string colName)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(colName)))
                return reader.GetDateTime(colName);
            return null;
        }
    }
}
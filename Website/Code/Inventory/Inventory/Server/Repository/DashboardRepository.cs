using Inventory.Models;
using Inventory.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.DataLayer.Repository
{
    public class DashboardRepository : MySQLRepository<Dashboard>
    {
        public DashboardRepository(MySqlConnection connection) : base(connection)
        {
        }

        public IEnumerable<Dashboard> GetAll()
        {
            using (var command = new MySqlCommand("SELECT * FROM orders order by orderdate desc limit 20"))
            {
                return GetRecords(command);
            }
        }

        public IEnumerable<Dashboard> GetByQuery(string searchQuery)
        {
            using (var command = new MySqlCommand("SELECT * FROM orders order by orderdate desc limit 20"))
            {
                return GetRecords(command);
            }
        }

        public override Dashboard PopulateRecord(MySqlDataReader reader)
        {
            return new Dashboard
            {
                OrderID = reader.GetInt32("OrderID"),
                OrderDate = DBUtils.GetDate(reader, "OrderDate"),
                ShippedDate = DBUtils.GetDate(reader, "ShippedDate"),
                ShippedName = DBUtils.GetString(reader, "ShipName")
            };
        }

    }
}
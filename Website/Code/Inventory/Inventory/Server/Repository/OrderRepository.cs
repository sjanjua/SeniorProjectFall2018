using Inventory.Models;
using Inventory.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.DataLayer.Repository
{
    public class OrderRepository : MySQLRepository<Orders>
    {
        public OrderRepository(MySqlConnection connection) : base(connection)
        {
        }

        public IEnumerable<Orders> GetAll()
        {
            using (var command = new MySqlCommand("SELECT * FROM orders"))
            {
                return GetRecords(command);
            }
        }

        public Orders GetById(int id)
        {
            // PARAMETERIZED QUERIES!
            using (var command = new MySqlCommand("SELECT * FROM orders WHERE OrderID = @id"))
            {
                command.Parameters.Add(new MySqlParameter("id", id));
                return GetRecord(command);
            }
        }

        public override Orders PopulateRecord(MySqlDataReader reader)
        {
            return new Orders
            {
                OrderID = reader.GetInt32("OrderID"),
                ShipperID = reader.GetInt32("ShipperID"),
                CustomerID = reader.GetString("CustomerID"),
                UserID = reader.GetInt32("UserID"),
                OrderDate = DBUtils.GetDate(reader, "OrderDate"),
                RequiredDate = DBUtils.GetDate(reader, "RequiredDate"),
                ShippedDate = DBUtils.GetDate(reader, "ShippedDate"),
                ShippedName = reader.GetString("ShipName"),
                ShippedAddress = reader.GetString("ShipAddress"),
                ShippedCity = reader.GetString("ShipCity"),
                ShippedRegion = DBUtils.GetString(reader, "ShipRegion"),
                Freight = reader.GetDecimal("Freight")
            };
        }

    }
}
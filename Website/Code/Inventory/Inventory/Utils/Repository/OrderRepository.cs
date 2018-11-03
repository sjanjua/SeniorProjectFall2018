using Inventory.Models;
using Inventory.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.DataLayer.Repository
{
    public class OrderRepository : MySQLRepository<Order>
    {
        public OrderRepository(MySqlConnection connection) : base(connection)
        {
        }

        public Order GetById(int id)
        {
            // PARAMETERIZED QUERIES!
            using (var command = new MySqlCommand("SELECT * FROM orders WHERE OrderID = @id"))
            {
                command.Parameters.Add(new MySqlParameter("id", id));
                return GetRecord(command);
            }
        }

        public override Order PopulateRecord(MySqlDataReader reader)
        {
            return new Order
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
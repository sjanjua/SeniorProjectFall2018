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

        public Int32 Save(Orders order) {

            if (order.OrderID > 0)
            {
                using (var command = new MySqlCommand("UPDATE orders SET ShippedDate = @shippedDate WHERE OrderID = @id"))
                {
                    command.Parameters.Add(new MySqlParameter("shippedDate", order.ShippedDate));
                    command.Parameters.Add(new MySqlParameter("id", order.OrderID));
                    AddRecord(command);
                }
            }
            else
            {
                using (var command = new MySqlCommand("INSERT INTO Orders (CustomerID, UserID, OrderDate, RequiredDate, ShipperID, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry) Values(@custid, @uid, @orddate, @reqdate, @shipid, @freight, @shipname, @shipaddress, @shipcity, @shipregion, @shippost, @shipcountry);"))
                {
                    command.Parameters.Add(new MySqlParameter("custid", order.CustomerID));
                    command.Parameters.Add(new MySqlParameter("uid", order.UserID));
                    command.Parameters.Add(new MySqlParameter("orddate", order.OrderDate));
                    command.Parameters.Add(new MySqlParameter("reqdate", order.RequiredDate));
                    command.Parameters.Add(new MySqlParameter("shipid", order.ShipperID));
                    command.Parameters.Add(new MySqlParameter("freight", order.Freight));
                    command.Parameters.Add(new MySqlParameter("shipname", order.ShippedName));
                    command.Parameters.Add(new MySqlParameter("shipaddress", order.ShippedAddress));
                    command.Parameters.Add(new MySqlParameter("shipcity", order.ShippedCity));
                    command.Parameters.Add(new MySqlParameter("shipregion", order.ShippedRegion));
                    command.Parameters.Add(new MySqlParameter("shippost", order.ShippedPostalCode));
                    command.Parameters.Add(new MySqlParameter("shipcountry", order.ShippedCountry));
                    AddRecord(command);
                }
                
                order.OrderID = GetIdentity();                
            }

            return order.OrderID;
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
                ShippedPostalCode = reader.GetString("ShipPostalCode"),
                ShippedCountry = DBUtils.GetString(reader, "ShipCountry"),
                Freight = reader.GetDecimal("Freight")
            };
        }

    }
}
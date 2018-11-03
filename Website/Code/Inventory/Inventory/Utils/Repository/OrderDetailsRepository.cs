using Inventory.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.DataLayer.Repository
{
    public class OrderDetailsRepository : MySQLRepository<OrderDetails>
    {
        public OrderDetailsRepository(MySqlConnection connection) : base(connection)
        {
        }

        //comment by erik again
        public IEnumerable<OrderDetails> GetById(int id)
        {
            using (var command = new MySqlCommand("SELECT * FROM orderdetails where OrderID=@id"))
            {
                command.Parameters.Add(new MySqlParameter("id", id));

                return GetRecords(command);
            }
        }

        public override OrderDetails PopulateRecord(MySqlDataReader reader)
        {
            return new OrderDetails
            {
                OrderID = reader.GetInt32("OrderID"),
                ProductID = reader.GetInt32("ProductID"),
                Quantity = reader.GetInt16("Quantity"),
                UnitPrice = reader.GetFloat("UnitPrice"),
                Discount = reader.GetDecimal("Discount")
            };
        }

    }
}
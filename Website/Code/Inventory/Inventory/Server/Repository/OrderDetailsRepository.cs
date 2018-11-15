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
            using (var command = new MySqlCommand("SELECT od.*, p.ProductName FROM orderdetails od inner join product p on od.ProductID = p.ProductID  where OrderID=@id"))
            {
                command.Parameters.Add(new MySqlParameter("id", id));

                return GetRecords(command);
            }
        }

        public bool Save(OrderDetails orderdet)
        {

            //if (order.OrderID > 0)
            //{
            //    using (var command = new MySqlCommand("UPDATE orderdetails SET UnitPrice = @uprice, Quantity =@qty WHERE OrderID = @id and ProductID=@pid"))
            //    {
            //        command.Parameters.Add(new MySqlParameter("uprice", orderdet.UnitPrice));
            //        command.Parameters.Add(new MySqlParameter("qty", orderdet.Quantity));
            //        command.Parameters.Add(new MySqlParameter("pid", orderdet.ProductID));
            //        command.Parameters.Add(new MySqlParameter("id", orderdet.OrderID));
            //        AddRecord(command);
            //    }
            //}
            //else
            //{
                using (var command = new MySqlCommand("INSERT INTO orderdetails (OrderID, ProductID, Quantity, UnitPrice, Discount) Values(@id, @pid, @qty, @uprice, @disc);"))
                {
                command.Parameters.Add(new MySqlParameter("uprice", orderdet.UnitPrice));
                command.Parameters.Add(new MySqlParameter("qty", orderdet.Quantity));
                command.Parameters.Add(new MySqlParameter("pid", orderdet.ProductID));
                command.Parameters.Add(new MySqlParameter("id", orderdet.OrderID));
                command.Parameters.Add(new MySqlParameter("disc", orderdet.Discount));
                    AddRecord(command);
                }
            return true;
              //  order.OrderID = GetIdentity();
            //}

            //return order.OrderID;
        }

        public override OrderDetails PopulateRecord(MySqlDataReader reader)
        {
            return new OrderDetails
            {
                OrderID = reader.GetInt32("OrderID"),
                ProductID = reader.GetInt32("ProductID"),
                ProductName = reader.GetString("ProductName"),
                Quantity = reader.GetInt16("Quantity"),
                UnitPrice = reader.GetFloat("UnitPrice"),
                Discount = reader.GetDecimal("Discount")
            };
        }

    }
}
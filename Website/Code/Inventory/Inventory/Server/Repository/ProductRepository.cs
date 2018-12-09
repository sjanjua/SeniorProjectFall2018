using Inventory.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.DataLayer.Repository
{
    public class ProductRepository : MySQLRepository<Product>
    {
        public ProductRepository(MySqlConnection connection) : base(connection)
        {
        }

        public IEnumerable<Product> GetAll()
        {
            using (var command = new MySqlCommand("SELECT * FROM product p"))
            {
                return GetRecords(command);
            }
        }

        public Product GetById(string id)
        {
            using (var command = new MySqlCommand("SELECT * FROM product WHERE ProductId = @id"))
            {
                command.Parameters.Add(new MySqlParameter("id", id));
                return GetRecord(command);
            }
        }

        public Int32 Save(Product product)
        {

            if (product.ProductID > 0)
            {
                using (var command = new MySqlCommand("UPDATE product SET unitsinstock = @stock, unitsonorder= @order, activeyn =@active WHERE ProductId = @id"))
                {
                    command.Parameters.Add(new MySqlParameter("stock", product.UnitsInStock));
                    command.Parameters.Add(new MySqlParameter("order", product.UnitsOnOrder));
                    command.Parameters.Add(new MySqlParameter("active", product.ActiveYN));
                    command.Parameters.Add(new MySqlParameter("id", product.ProductID));
                    ExecuteQuery(command);
                }
            }
            else
            {
                //using (var command = new MySqlCommand("INSERT INTO shipper (ShipperName, phone, activeyn) Values(@shippername, @phone, @active);"))
                //{
                //    command.Parameters.Add(new MySqlParameter("shippername", product.ShipperName));
                //    command.Parameters.Add(new MySqlParameter("phone", product.Phone));
                //    command.Parameters.Add(new MySqlParameter("active", product.ActiveYN));
                //    ExecuteQuery(command);
                //}

                //product.ShipperID = GetIdentity();
            }

            return product.ProductID;
        }

        public override Product PopulateRecord(MySqlDataReader reader)
        {
            return new Product
            {
                ProductID = reader.GetInt32("ProductID"),
                ProductName = reader.GetString("ProductName"),
                SupplierID = reader.GetInt32("SupplierID"),
                CategoryID = reader.GetInt32("CategoryID"),
                QuantityPerUnit = reader.GetString("QuantityPerUnit"),
                UnitPrice = reader.GetDecimal("UnitPrice"),
                UnitsInStock = reader.GetInt16("UnitsInStock"),
                UnitsOnOrder = reader.GetInt16("UnitsOnOrder"),
                ReOrderLevel = reader.GetInt16("ReOrderLevel"),
                ActiveYN = reader.GetString("ActiveYN")
            };
        }
    }
}
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
            using (var command = new MySqlCommand("SELECT * FROM product"))
            {
                return GetRecords(command);
            }
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
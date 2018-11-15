using Inventory.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.DataLayer.Repository
{
    public class DisplayProductRepository : MySQLRepository<DisplayProduct>
    {
        public DisplayProductRepository(MySqlConnection connection) : base(connection)
        {
        }

        //comment by erik again
        public IEnumerable<DisplayProduct> GetAll()
        {
            using (var command = new MySqlCommand("SELECT * FROM product where activeyn='Y'"))
            {
                return GetRecords(command);
            }
        }

        public override DisplayProduct PopulateRecord(MySqlDataReader reader)
        {
            return new DisplayProduct
            {
                ProductID = reader.GetInt32("ProductID"),
                ProductName = reader.GetString("ProductName"),               
                QuantityPerUnit = reader.GetString("QuantityPerUnit"),
                UnitPrice = reader.GetDecimal("UnitPrice"),
                UnitsInStock = reader.GetInt16("UnitsInStock")
            };
        }
    }
}
using Inventory.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.DataLayer.Repository
{
    public class DisplaySupplierRepository : MySQLRepository<DisplaySupplier>
    {
        public DisplaySupplierRepository(MySqlConnection connection) : base(connection)
        {
        }

        public IEnumerable<DisplaySupplier> GetAll()
        {
            using (var command = new MySqlCommand("SELECT * FROM supplier"))
            {
                return GetRecords(command);
            }
        }

        public override DisplaySupplier PopulateRecord(MySqlDataReader reader)
        {
            return new DisplaySupplier
            {
                SupplierID = reader.GetInt32("SupplierId"),
                CompanyName = reader.GetString("CompanyName")                
            };
        }
    }
}
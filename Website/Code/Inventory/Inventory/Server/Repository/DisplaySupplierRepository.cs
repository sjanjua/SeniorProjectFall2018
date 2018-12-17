using Inventory.Models;
using Inventory.Utils;
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
                CompanyName = reader.GetString("CompanyName"),
                ContactName = reader.GetString("ContactName"),
                ContactTitle = reader.GetString("ContactTitle"),
                Address = reader.GetString("Address"),
                City = reader.GetString("City"),
                Region = DBUtils.GetString(reader, "Region"),
                PostalCode = reader.GetString("PostalCode"),
                Country = reader.GetString("Country"),
                Phone = reader.GetString("Phone"),
                Fax = DBUtils.GetString(reader, "Fax")
            };
        }
    }
}
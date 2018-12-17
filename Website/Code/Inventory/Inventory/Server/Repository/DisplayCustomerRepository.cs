using Inventory.Models;
using Inventory.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.DataLayer.Repository
{
    public class DisplayCustomerRepository :MySQLRepository<DisplayCustomer>
    {
        public DisplayCustomerRepository(MySqlConnection connection) : base(connection)
        {
        }

        public IEnumerable<DisplayCustomer> GetAll()
        {
            using (var command = new MySqlCommand("SELECT * FROM customer"))
            {
                return GetRecords(command);
            }
        }

        public override DisplayCustomer PopulateRecord(MySqlDataReader reader)
        {
            return new DisplayCustomer
            {
                CustomerID = reader.GetString("CustomerId"),
                CompanyName = reader.GetString("CompanyName"),
                ContactName = reader.GetString("ContactName"),
                ContactTitle = DBUtils.GetString(reader, "ContactTitle"),
                Address = DBUtils.GetString(reader, "Address"),
                City = DBUtils.GetString(reader, "City"),
                Region = DBUtils.GetString(reader, "Region"),
                PostalCode = DBUtils.GetString(reader, "PostalCode"),
                Country = DBUtils.GetString(reader, "Country"),
                Phone = DBUtils.GetString(reader, "Phone"),
                Fax = DBUtils.GetString(reader, "Fax")
            };
        }
    }
}
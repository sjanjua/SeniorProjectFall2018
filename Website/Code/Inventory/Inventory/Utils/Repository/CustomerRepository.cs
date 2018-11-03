using Inventory.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.DataLayer.Repository
{
    public class CustomerRepository : MySQLRepository<Customer>
    {
        public CustomerRepository(MySqlConnection connection) : base(connection)
        {
        }
        
        public IEnumerable<Customer> GetAll()
        {
            using (var command = new MySqlCommand("SELECT * FROM customer"))
            {
                return GetRecords(command);
            }
        }

        public override Customer PopulateRecord(MySqlDataReader reader)
        {
            return new Customer
            {
                CustomerID = reader.GetString("CustomerId"),
                CompanyName = reader.GetString("CompanyName"),
                ContactName = reader.GetString("ContactName"),
                ContactTitle = reader.GetString("ContactTitle"),
                Address = reader.GetString("Address"),
                City = reader.GetString("City"),
                Region = reader.GetString("Region"),
                PostalCode = reader.GetString("PostalCode"),
                Country = reader.GetString("Country"),
                Phone = reader.GetString("Phone"),
                Fax = reader.GetString("Fax")
            };
        }
    }
}
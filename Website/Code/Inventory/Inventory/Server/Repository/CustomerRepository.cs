using Inventory.Models;
using Inventory.Utils;
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

        public Customer GetById(string id)
        {
            using (var command = new MySqlCommand("SELECT * FROM customer WHERE CustomerId = @id"))
            {
                command.Parameters.Add(new MySqlParameter("id", id));
                return GetRecord(command);
            }
        }

        public override Customer PopulateRecord(MySqlDataReader reader)
        {
            return new Customer
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

        public void Delete(String id)
        {
            using (var command = new MySqlCommand("DELETE FROM customer WHERE CustomerId = @id"))
            {
                command.Parameters.Add(new MySqlParameter("id", id));
                ExecuteStoredProc(command);
            }
        }
    }
}
using Inventory.Models;
using Inventory.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.DataLayer.Repository
{
    public class SupplierRepository : MySQLRepository<Supplier>
    {
        public SupplierRepository(MySqlConnection connection) : base(connection)
        {
        }

        public IEnumerable<Supplier> GetAll()
        {
            using (var command = new MySqlCommand("SELECT * FROM supplier"))
            {
                return GetRecords(command);
            }
        }

        public Supplier GetById(string id)
        {
            using (var command = new MySqlCommand("SELECT * FROM supplier WHERE SupplierId = @id"))
            {
                command.Parameters.Add(new MySqlParameter("id", id));
                return GetRecord(command);
            }
        }

        public override Supplier PopulateRecord(MySqlDataReader reader)
        {
            return new Supplier
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
                Fax = DBUtils.GetString(reader, "Fax"),
              //homepage
            };
        }

        public void Delete(String id)
        {
            using (var command = new MySqlCommand("DELETE FROM supplier WHERE SupplierId = @id"))
            {
                command.Parameters.Add(new MySqlParameter("id", id));
                ExecuteStoredProc(command);
            }
        }
    }
}
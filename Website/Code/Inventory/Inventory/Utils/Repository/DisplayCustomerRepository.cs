using Inventory.Models;
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

        //comment by erik again
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
                CompanyName = reader.GetString("CompanyName")
            };
        }
    }
}
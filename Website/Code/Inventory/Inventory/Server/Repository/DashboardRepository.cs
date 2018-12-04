using Inventory.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.DataLayer.Repository
{
    public class DashboardRepository : MySQLRepository<Dashboard>
    {
        public DashboardRepository(MySqlConnection connection) : base(connection)
        {
        }

        public IEnumerable<Dashboard> GetAll()
        {
            using (var command = new MySqlCommand("SELECT * FROM customer"))
            {
                return GetRecords(command);
            }
        }

    }
}
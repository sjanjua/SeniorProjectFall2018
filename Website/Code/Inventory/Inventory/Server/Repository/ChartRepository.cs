using Inventory.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Inventory.DataLayer.Repository
{
    public class ChartRepository : MySQLRepository<Chart>
    {
        public ChartRepository(MySqlConnection connection) : base(connection)
        {
        }

        public IEnumerable<Chart> GetAll()
        {
            using (var command = new MySqlCommand("select month(Orderdate) as name, count(*) as count from orders where orderdate > DATE_SUB(now(), INTERVAL 6 MONTH) group by month(orderdate)  order by month(orderdate);"))
            {
                return GetRecords(command);
            }
        }

        public override Chart PopulateRecord(MySqlDataReader reader)
        {
            return new Chart
            {
                MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(reader.GetInt16("name")),
                NoOfOrders = reader.GetInt16("count")
            };
        }
    }
}
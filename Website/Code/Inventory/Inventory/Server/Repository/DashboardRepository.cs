using Inventory.Models;
using Inventory.Utils;
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

        public Dashboard GetData()
        {
            // PARAMETERIZED QUERIES!
            using (var command = new MySqlCommand("SELECT (SELECT COUNT(*) FROM orders WHERE orderdate BETWEEN DATE_ADD(DATE_ADD(LAST_DAY(CURDATE()), " +
                                                  "INTERVAL 1 DAY), INTERVAL - 1 MONTH) AND LAST_DAY(CURDATE())) AS OrdersInMonth, " +
                                                  "ifnull((SELECT SUM(od.unitprice * od.quantity) FROM orders o INNER JOIN orderdetails od " +
                                                  "ON o.orderid = od.orderid WHERE orderdate BETWEEN DATE_ADD(DATE_ADD(LAST_DAY(CURDATE()), " +
                                                  "INTERVAL 1 DAY), INTERVAL - 1 MONTH) AND LAST_DAY(CURDATE())), 0.0) AS OrderTotal, " +
                                                  "(SELECT COUNT(*) FROM product WHERE UnitsInStock < ReorderLevel AND activeYn = 'Y') AS ProductInReorder; "))
            {                
                return GetRecord(command);
            }
        }

        public override Dashboard PopulateRecord(MySqlDataReader reader)
        {
            return new Dashboard
            {
                OrdersInMonth = reader.GetInt32("OrdersInMonth"),
                OrderTotal = reader.GetDecimal("OrderTotal"),
                ProductsReorder = reader.GetInt32("ProductInReorder")
            };
        }

    }
}
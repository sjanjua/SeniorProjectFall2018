using Inventory.Models;
using Inventory.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.DataLayer.Repository
{
    public class DisplayOrderRepository : MySQLRepository<DisplayOrder>
    {
        public DisplayOrderRepository(MySqlConnection connection) : base(connection)
        {
        }

        public IEnumerable<DisplayOrder> GetAll()
        {
            using (var command = new MySqlCommand("SELECT * FROM orders order by orderdate desc limit 20"))
            {
                return GetRecords(command);
            }
        }

        public override DisplayOrder PopulateRecord(MySqlDataReader reader)
        {
            return new DisplayOrder
            {
                OrderID = reader.GetInt32("OrderID"),
                OrderDate = DBUtils.GetDate(reader, "OrderDate"),
                RequiredDate = DBUtils.GetDate(reader, "RequiredDate"),
                ShippedDate = DBUtils.GetDate(reader, "ShippedDate"),
                ShippedName = reader.GetString("ShipName"),
                ShippedAddress = reader.GetString("ShipAddress"),
                ShippedCity = reader.GetString("ShipCity"),
                ShippedRegion = DBUtils.GetString(reader, "ShipRegion"),
                Freight = reader.GetDecimal("Freight")
            };
        }
    }
}
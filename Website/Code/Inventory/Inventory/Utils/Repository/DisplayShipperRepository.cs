using Inventory.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.DataLayer.Repository
{
    public class DisplayShipperRepository : MySQLRepository<DisplayShipper>
    {
        public DisplayShipperRepository(MySqlConnection connection) : base(connection)
        {
        }

        //comment by erik again
        public IEnumerable<DisplayShipper> GetAll()
        {
            using (var command = new MySqlCommand("SELECT * FROM shipper where activeyn='Y'"))
            {
                return GetRecords(command);
            }
        }

        public override DisplayShipper PopulateRecord(MySqlDataReader reader)
        {
            return new DisplayShipper
            {
                ShipperID = reader.GetInt32("ShipperId"),
                ShipperName = reader.GetString("ShipperName")                
            };
        }
    }
}
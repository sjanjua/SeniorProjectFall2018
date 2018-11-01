using Inventory.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.DataLayer.Repository
{
    public class ShipperRepository : MySQLRepository<Shipper>
    {
        public ShipperRepository(MySqlConnection connection) : base(connection)
        {
        }

        //comment by erik
        public IEnumerable<Shipper> GetAll()
        {
            using (var command = new MySqlCommand("SELECT * FROM shipper"))
            {
                return GetRecords(command);
            }
        }

        public Shipper GetById(string id)
        {
            // PARAMETERIZED QUERIES!
            using (var command = new MySqlCommand("SELECT * FROM shipper WHERE ShipperId = @id"))
            {
                command.Parameters.Add(new MySqlParameter("id", id));
                return GetRecord(command);
            }
        }

        public override Shipper PopulateRecord(MySqlDataReader reader)
        {
            return new Shipper
            {
                ShipperID = reader.GetInt32("ShipperId"),
                ShipperName = reader.GetString("ShipperName"),
                Phone = reader.GetString("Phone"),
                ActiveYN = reader.GetString("ActiveYN")
            };
        }
    }
}
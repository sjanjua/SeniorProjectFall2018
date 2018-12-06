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

        public Int32 Save(Shipper shipper)
        {

            if (shipper.ShipperID > 0)
            {
                using (var command = new MySqlCommand("UPDATE shipper SET ShipperName = @shippername, phone= @phone, activeyn =@active WHERE ShipperId = @id"))
                {
                    command.Parameters.Add(new MySqlParameter("shippername", shipper.ShipperName));
                    command.Parameters.Add(new MySqlParameter("phone", shipper.Phone));
                    command.Parameters.Add(new MySqlParameter("active", shipper.ActiveYN));
                    command.Parameters.Add(new MySqlParameter("id", shipper.ShipperID));
                    ExecuteQuery(command);
                }
            }
            else
            {
                using (var command = new MySqlCommand("INSERT INTO shipper (ShipperName, phone, activeyn) Values(@shippername, @phone, @active);"))
                {
                    command.Parameters.Add(new MySqlParameter("shippername", shipper.ShipperName));
                    command.Parameters.Add(new MySqlParameter("phone", shipper.Phone));
                    command.Parameters.Add(new MySqlParameter("active", shipper.ActiveYN));
                    ExecuteQuery(command);
                }

                shipper.ShipperID = GetIdentity();
            }

            return shipper.ShipperID;
        }
    }
}
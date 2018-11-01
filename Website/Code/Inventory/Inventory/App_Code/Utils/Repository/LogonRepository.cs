using Inventory.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.DataLayer.Repository
{
    public class LogonRepository : MySQLRepository<Logon>
    {
        public LogonRepository(MySqlConnection connection) : base(connection)
        {
        }

        public IEnumerable<Logon> GetAll()
        {
            // DBAs across the country are having strokes 
            //  over this next command!
            using (var command = new MySqlCommand("SELECT * FROM Users"))
            {
                return GetRecords(command);
            }
        }

        public Logon GetById(string id)
        {
            // PARAMETERIZED QUERIES!
            using (var command = new MySqlCommand("SELECT * FROM Users WHERE UserID = @id"))
            {
                command.Parameters.Add(new MySqlParameter("id", id));
                return GetRecord(command);
            }
        }

        public override Logon PopulateRecord(MySqlDataReader reader)
        {
            return new Logon
            {
                UserID = reader.GetString("UserID"),
                Password_Field = reader.GetString("Password_Field"),
                First_Name = reader.GetString("First_Name"),
                Last_Name = reader.GetString("Last_Name"),
                Phone_Number = reader.GetString("Phone_Number"),
                Street = reader.GetString("Street"),
                City = reader.GetString("City"),
                Zip_Code = reader.GetString("Zip_Code"),
                Email = reader.GetString("Email")
            };
        }
    }
}
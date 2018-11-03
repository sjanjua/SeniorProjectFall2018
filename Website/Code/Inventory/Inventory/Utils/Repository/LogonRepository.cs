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
            using (var command = new MySqlCommand("SELECT * FROM user"))
            {
                return GetRecords(command);
            }
        }

        public Logon GetByName(string name)
        {
            // PARAMETERIZED QUERIES!
            using (var command = new MySqlCommand("SELECT u.*, aes_decrypt(Password,'seniorproject') as DecryptPassword FROM user u WHERE UserName = @id"))
            {
                command.Parameters.Add(new MySqlParameter("id", name));
                return GetRecord(command);
            }
        }

        public override Logon PopulateRecord(MySqlDataReader reader)
        {
            return new Logon
            {
                 UserName= reader.GetString("UserName"),
                Password = reader.GetString("DecryptPassword")
            };
        }
    }
}
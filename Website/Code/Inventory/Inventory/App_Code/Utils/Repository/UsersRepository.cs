using Inventory.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.DataLayer.Repository
{
    public class UsersRepository : MySQLRepository<Users>
    {
        public UsersRepository(MySqlConnection connection) : base(connection)
        {
        }

        public IEnumerable<Users> GetAll()
        {
            // DBAs across the country are having strokes 
            //  over this next command!
            using (var command = new MySqlCommand("SELECT * FROM Users"))
            {
                return GetRecords(command);
            }
        }

        public Users GetById(string id)
        {
            // PARAMETERIZED QUERIES!
            using (var command = new MySqlCommand("SELECT * FROM Users WHERE UserID = @id"))
            {
                command.Parameters.Add(new MySqlParameter("id", id));
                return GetRecord(command);
            }
        }

        public void SetAll(Dictionary<String, Object> hash)
        {
            Users user = GetById((String)hash["UserID"]);
            if (user != null)
            {
                using (var command = new MySqlCommand("UPDATE Users SET First_Name = @firstName, Last_Name = @lastName, Phone_Number = @phoneNumber, Street = @street, City = @city, Zip_Code = @zipCode, Email = @email WHERE UserID = @id"))
                {
                    command.Parameters.Add(new MySqlParameter("firstName", (String)hash["First_Name"]));
                    command.Parameters.Add(new MySqlParameter("lastName", (String)hash["Last_Name"]));
                    command.Parameters.Add(new MySqlParameter("phoneNumber", (String)hash["Phone_Number"]));
                    command.Parameters.Add(new MySqlParameter("street", (String)hash["Street"]));
                    command.Parameters.Add(new MySqlParameter("city", (String)hash["City"]));
                    command.Parameters.Add(new MySqlParameter("zipCode", (String)hash["Zip_Code"]));
                    command.Parameters.Add(new MySqlParameter("email", (String)hash["Email"]));
                    AddRecord(command);
                }
            }
            else
            {
                using (var command = new MySqlCommand("INSERT INTO Users UserID = @id, Password = @password, First_Name = @firstName, Last_Name = @lastName, Phone_Number = @phoneNumber, Street = @street, City = @city, Zip_Code = @zipCode, Email = @email"))
                {
                    command.Parameters.Add(new MySqlParameter("id", (String)hash["UserID"]));
                    command.Parameters.Add(new MySqlParameter("password", (String)hash["Password"]));
                    command.Parameters.Add(new MySqlParameter("firstName", (String)hash["First_Name"]));
                    command.Parameters.Add(new MySqlParameter("lastName", (String)hash["Last_Name"]));
                    command.Parameters.Add(new MySqlParameter("phoneNumber", (String)hash["Phone_Number"]));
                    command.Parameters.Add(new MySqlParameter("street", (String)hash["Street"]));
                    command.Parameters.Add(new MySqlParameter("city", (String)hash["City"]));
                    command.Parameters.Add(new MySqlParameter("zipCode", (String)hash["Zip_Code"]));
                    command.Parameters.Add(new MySqlParameter("email", (String)hash["Email"]));
                    AddRecord(command);
                }
            }
        }


        public override Users PopulateRecord(MySqlDataReader reader)
        {
            return new Users
            {
                UserID = reader.GetString("UserID"),
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
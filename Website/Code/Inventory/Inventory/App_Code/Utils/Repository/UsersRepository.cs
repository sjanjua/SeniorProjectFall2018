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
            using (var command = new MySqlCommand("SELECT * FROM user"))
            {
                return GetRecords(command);
            }
        }

        public Users GetById(string id)
        {
            // PARAMETERIZED QUERIES!
            using (var command = new MySqlCommand("SELECT * FROM user WHERE UserID = @id"))
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
                using (var command = new MySqlCommand("UPDATE users SET FirstName = @firstName, LastName = @lastName, PhoneNumber = @phoneNumber, Street = @street, City = @city, ZipCode = @zipCode, Email = @email WHERE UserID = @id"))
                {
                    command.Parameters.Add(new MySqlParameter("firstName", (String)hash["FirstName"]));
                    command.Parameters.Add(new MySqlParameter("lastName", (String)hash["LastName"]));
                    command.Parameters.Add(new MySqlParameter("phoneNumber", (String)hash["PhoneNumber"]));
                    command.Parameters.Add(new MySqlParameter("street", (String)hash["Street"]));
                    command.Parameters.Add(new MySqlParameter("city", (String)hash["City"]));
                    command.Parameters.Add(new MySqlParameter("zipCode", (String)hash["ZipCode"]));
                    command.Parameters.Add(new MySqlParameter("email", (String)hash["Email"]));
                    AddRecord(command);
                }
            }
            else
            {
                using (var command = new MySqlCommand("INSERT INTO Users (UserName, Password, FirstName, LastName, PhoneNumber, Street, City, ZipCode, Email, RoleID) Values(@username, aes_encrypt(@password,'seniorproject'), @firstName, @lastName, @phoneNumber, @street, @city, @zipCode, @email, @roleid);"))
                {
                    command.Parameters.Add(new MySqlParameter("username", (String)hash["UserName"]));
                    command.Parameters.Add(new MySqlParameter("password", (String)hash["Password"]));
                    command.Parameters.Add(new MySqlParameter("firstName", (String)hash["FirstName"]));
                    command.Parameters.Add(new MySqlParameter("lastName", (String)hash["LastName"]));
                    command.Parameters.Add(new MySqlParameter("phoneNumber", (String)hash["PhoneNumber"]));
                    command.Parameters.Add(new MySqlParameter("street", (String)hash["Street"]));
                    command.Parameters.Add(new MySqlParameter("city", (String)hash["City"]));
                    command.Parameters.Add(new MySqlParameter("zipCode", (String)hash["ZipCode"]));
                    command.Parameters.Add(new MySqlParameter("email", (String)hash["Email"]));
                    command.Parameters.Add(new MySqlParameter("roleid", "2"));
                    AddRecord(command);
                }
            }
        }

        public void changeUserRole(String userID, char userType)
        {
            using (var command = new MySqlCommand("Update user SET User_Type = @type WHERE UserID = @id"))
            {
                command.Parameters.Add(new MySqlParameter("type", userType));
                command.Parameters.Add(new MySqlParameter("id", userID));
                AddRecord(command);
            }
        }


        public override Users PopulateRecord(MySqlDataReader reader)
        {
            return new Users
            {
                UserID = reader.GetInt32("UserID"),
                UserName = reader.GetString("UserName"),
                Password_Field = reader.GetString("Password_Field"),
                FirstName = reader.GetString("First_Name"),
                LastName = reader.GetString("Last_Name"),
                PhoneNumber = reader.GetString("Phone_Number"),
                Street = reader.GetString("Street"),
                City = reader.GetString("City"),
                ZipCode = reader.GetString("Zip_Code"),
                Email = reader.GetString("Email")
            };
        }

    }

}
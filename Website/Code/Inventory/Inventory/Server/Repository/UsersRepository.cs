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
            using (var command = new MySqlCommand("select * from user order by RoleID, FirstName"))
            {
                return GetRecords(command);
            }
        }

        public Users GetByName(string name)
        {
            // PARAMETERIZED QUERIES!
            using (var command = new MySqlCommand("SELECT * FROM user WHERE UserName = @name"))
            {
                command.Parameters.Add(new MySqlParameter("name", name));
                return GetRecord(command);
            }
        }

        public void SetAll(Dictionary<String, Object> hash)
        {
            Users user = GetByName((String)hash["UserName"]);
            if (user != null)
            {
                using (var command = new MySqlCommand("UPDATE user SET FirstName = @firstName, LastName = @lastName, PhoneNumber = @phoneNumber, Street = @street, City = @city, ZipCode = @zipCode, Email = @email WHERE UserID = @id"))
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
                using (var command = new MySqlCommand("INSERT INTO user (UserName, Password, FirstName, LastName, PhoneNumber, Street, City, ZipCode, Email, RoleID) Values(@username, aes_encrypt(@password,'seniorproject'), @firstName, @lastName, @phoneNumber, @street, @city, @zipCode, @email, @roleid);"))
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
                    command.Parameters.Add(new MySqlParameter("roleid", "3"));
                    AddRecord(command);
                }
            }
        }

        public void changeUserRole(String userID, string userType)
        {
            using (var command = new MySqlCommand("Update user SET RoleID = @type WHERE UserName = @id"))
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
                Password = reader.GetString("Password"),
                FirstName = reader.GetString("FirstName"),
                LastName = reader.GetString("LastName"),
                PhoneNumber = reader.GetString("PhoneNumber"),
                Street = reader.GetString("Street"),
                City = reader.GetString("City"),
                ZipCode = reader.GetString("ZipCode"),
                Email = reader.GetString("Email"),
                RoleID = reader.GetInt16("RoleID"),
                ActiveYN = reader.GetString("ActiveYN")
            };
        }

    }

}
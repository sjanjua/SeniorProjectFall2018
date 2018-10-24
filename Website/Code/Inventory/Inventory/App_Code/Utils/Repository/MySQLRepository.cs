using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace Inventory.DataLayer.Repository
{
    public abstract class MySQLRepository<T> where T : class
    {
        private MySqlConnection _connection;
        
        public MySQLRepository(MySqlConnection connection)
        {
            _connection = connection;
        }
        public virtual T PopulateRecord(MySqlDataReader reader)
        {
            return null;
        }
        protected IEnumerable<T> GetRecords(MySqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _connection;
            _connection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                        list.Add(PopulateRecord(reader));
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _connection.Close();
            }
            return list;
        }
        protected T GetRecord(MySqlCommand command)
        {
            T record = null;
            command.Connection = _connection;
            _connection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        record = PopulateRecord(reader);
                        break;
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _connection.Close();
            }
            return record;
        }
        protected IEnumerable<T> ExecuteStoredProc(MySqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _connection;
            command.CommandType = CommandType.StoredProcedure;
            _connection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        var record = PopulateRecord(reader);
                        if (record != null) list.Add(record);
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _connection.Close();
            }
            return list;
        }

        protected void AddRecord(MySqlCommand command)
        {
            command.Connection = _connection;
            _connection.Open();
            try
            {
                command.ExecuteNonQuery();
                _connection.Close();
            }
            finally
            {
                _connection.Close();
            }
        } 
    }

}
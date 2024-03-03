using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeafoodSQLCommands
{
    public class DatabaseHelper
    {
        public void ConnectToDatabase(SqlConnection connection, string query)
        {
            connection.Open();
            ExecuteQuery(query, connection);
        }

        public List<string> ExecuteQuery(string query, SqlConnection connection) 
        {
            List<string> results = new List<string>();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string data = reader.GetString(0);
                            results.Add(data);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found. Reader was empty.");
                    }
                }
            }

            return results;
        }

        public void DisconnectFromDatabase(SqlConnection connection)
        {
            connection.Dispose();
        }
    }
}

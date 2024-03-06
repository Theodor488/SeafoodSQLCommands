using System;
using System.Data.SqlClient;

namespace SeafoodSQLCommands
{
    public class DatabaseHelper
    {
        DataProcessor dataProcessor = new DataProcessor();
        public void ConnectToDatabase(SqlConnection connection)
        {
            connection.Open();
        }

        public List<Dictionary<string, object>> ExecuteQuery(SqlCommand command, SqlConnection connection, bool printResults) 
        {
            List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read()) 
                {
                    var row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++) 
                    {
                        row[reader.GetName(i)] = reader.GetValue(i);    
                    }
                    results.Add(row);
                }
            }

            if (printResults)
            {
                dataProcessor.PrintResults(results);
            }

            return results;
        }

        public void DisconnectFromDatabase(SqlConnection connection)
        {
            connection.Dispose();
        }
    }
}

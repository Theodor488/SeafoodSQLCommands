using System;
using System.Data.SqlClient;

namespace SeafoodSQLCommands
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=THEO-COMPUTER\\SQLEXPRESS;Initial Catalog=SeafoodDB;Integrated Security=True";

            SeafoodSpeciesManager seafoodSpeciesManager = new SeafoodSpeciesManager();
            DatabaseHelper databaseHelper = new DatabaseHelper();

            try
            {
                // SQL query to retrieve species names containing the text "fish"
                string query = seafoodSpeciesManager.GetAllSpecies();

                // Create and open connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    databaseHelper.ConnectToDatabase(connection, query);
                }
                    

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}

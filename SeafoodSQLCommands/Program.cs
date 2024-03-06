using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace SeafoodSQLCommands
{

    public enum QueryOption
    {
        GetAllSpeciesNamesAndIds,
        GetAllSpecies,
        GetSpeciesById,
        GetAllSpeciesFullCatchInfo,
        GetAllSpeciesFullCatchInfoByName,
        GetSpecificInfoForSpecies
    }

    public class Program
    {
        static void Main(string[] args)
        {
            
            string connectionString = "Data Source=THEO-COMPUTER\\SQLEXPRESS;Initial Catalog=SeafoodDB;Integrated Security=True";

            QueryCommandsManager queryCommandsManager = new QueryCommandsManager();
            DatabaseHelper databaseHelper = new DatabaseHelper();
            UserInputHelper userInputHelper = new UserInputHelper();
            DataProcessor dataProcessor = new DataProcessor();

            try
            {
                // Create and open connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    databaseHelper.ConnectToDatabase(connection);

                    // Map names to IDs
                    Dictionary<string, string> speciesNamesAndIDs = dataProcessor.GenerateSpeciesNamesAndIDs(queryCommandsManager, databaseHelper, dataProcessor, connection);

                    // Choose and run query
                    string choice = userInputHelper.ChooseQueryToRun();
                    SqlCommand command = userInputHelper.RunChosenQuery(queryCommandsManager, choice, speciesNamesAndIDs, connection);

                    // Get results from query
                    List<Dictionary<string, object>> queryResults = databaseHelper.ExecuteQuery(command, connection, true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        
    }   
}

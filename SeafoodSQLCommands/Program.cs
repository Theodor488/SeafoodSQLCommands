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

                    string speciesNamesAndIDsQuery = queryCommandsManager.GetAllSpeciesNamesAndIds();

                    // Get Species Names and IDs --> convert to dictionary
                    List<Dictionary<string, object>> speciesNamesAndIDs_RawData = databaseHelper.ExecuteQuery(speciesNamesAndIDsQuery, connection, false);
                    Dictionary<string, string> speciesNamesAndIDs = dataProcessor.ConvertNamesAndIDsToDictionary(speciesNamesAndIDs_RawData);

                    // Choose and run query
                    string choice = userInputHelper.ChooseQueryToRun();
                    string query = userInputHelper.RunChosenQuery(queryCommandsManager, choice, speciesNamesAndIDs);

                    // Get results from query
                    List<Dictionary<string, object>> queryResults = databaseHelper.ExecuteQuery(query, connection, true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }   
}

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
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            List<string> allowedColumnsSection = configuration.GetSection("AllowedColumns:Catches").Get<List<string>>();
            string connectionString = configuration.GetSection("ConnectionStrings:SeafoodDBConnection").Get<string>();

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
                    SqlCommand command = userInputHelper.RunChosenQuery(queryCommandsManager, choice, speciesNamesAndIDs, allowedColumnsSection, connection);

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

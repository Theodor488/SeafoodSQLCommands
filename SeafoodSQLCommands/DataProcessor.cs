using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeafoodSQLCommands
{
    public class DataProcessor
    {
        public void PrintResults(List<Dictionary<string, object>> results)
        {
            foreach (Dictionary<string, object> dict in results)
            {
                foreach (var kvp in dict)
                {
                    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                }
                Console.WriteLine();
            }
        }

        public Dictionary<string, string> ConvertNamesAndIDsToDictionary(List<Dictionary<string, object>> results)
        {
            Dictionary<string, string> dictResults = new Dictionary<string, string>();

            foreach (Dictionary<string, object> dict in results)
            {
                string speciesName = dict.Values.ElementAt(0).ToString();
                string speciesID = dict.Values.ElementAt(1).ToString();
                dictResults.Add(speciesID, speciesName);
            }

            return dictResults;
        }

        public Dictionary<string, string> GenerateSpeciesNamesAndIDs(QueryCommandsManager queryCommandsManager, DatabaseHelper databaseHelper, DataProcessor dataProcessor, SqlConnection connection)
        {
            SqlCommand speciesNamesAndIDsQuery = queryCommandsManager.GetAllSpeciesNamesAndIds(connection);

            // Get Species Names and IDs --> convert to dictionary
            List<Dictionary<string, object>> speciesNamesAndIDs_RawData = databaseHelper.ExecuteQuery(speciesNamesAndIDsQuery, connection, false);
            Dictionary<string, string> speciesNamesAndIDs = dataProcessor.ConvertNamesAndIDsToDictionary(speciesNamesAndIDs_RawData);
            return speciesNamesAndIDs;
        }
    }
}

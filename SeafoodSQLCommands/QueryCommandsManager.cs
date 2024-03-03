using System;
using System.Data.SqlClient;

namespace SeafoodSQLCommands
{
    public class QueryCommandsManager
    {
        public string GetAllSpeciesNamesAndIds()
        {
            return "SELECT Name, SpeciesId\r\nFROM SpeciesTable species \r\n;";
        }

        public string GetAllSpecies()
        {
            return "SELECT Name FROM SpeciesTable";
        }

        public string GetSpeciesById(int speciesId)
        {
            return $"SELECT Name FROM SpeciesTable WHERE SpeciesId = '{speciesId}'";
        }

        public string GetAllSpeciesFullCatchInfo() 
        {
            return "SELECT *\r\nFROM SpeciesTable species \r\nINNER JOIN CatchesTable catches ON species.SpeciesId = catches.SpeciesId\r\nORDER BY species.Name DESC;";
        }

        public string GetAllSpeciesFullCatchInfoByName(string name)
        {
            string query;

            if (int.TryParse(name, out var speciesId)) 
            {
                query = $"SELECT *\r\nFROM SpeciesTable species \r\nINNER JOIN CatchesTable catches ON species.SpeciesId = catches.SpeciesId\r\nWHERE species.SpeciesId = {speciesId}\r\nORDER BY species.Name DESC;";
            }
            else
            {
                query = $"SELECT *\r\nFROM SpeciesTable species \r\nINNER JOIN CatchesTable catches ON species.SpeciesId = catches.SpeciesId\r\nWHERE species.Name = '{name}'\r\nORDER BY species.Name DESC;";
            }

            return query;
        }

        public string GetSpecificInfoForSpecies(string speciesName, string speciesColumn, Dictionary<string, string> speciesNamesAndIDs_RawData)
        {
            string query;

            if (int.TryParse(speciesName, out var speciesId))
            {
                string speciesId_string = speciesId.ToString();
                speciesName = speciesNamesAndIDs_RawData[speciesId_string];
                Console.WriteLine($"Species Name: {speciesName}");

                query = $"SELECT {speciesColumn}\r\nFROM SpeciesTable species \r\nINNER JOIN CatchesTable catches ON species.SpeciesId = catches.SpeciesId\r\nWHERE species.SpeciesId = {speciesId}\r\nORDER BY species.Name DESC;";
            }
            else
            {
                Console.WriteLine($"Species Name: {speciesName}");
                query = $"SELECT {speciesColumn}\r\nFROM SpeciesTable species \r\nINNER JOIN CatchesTable catches ON species.SpeciesId = catches.SpeciesId\r\nWHERE species.Name = '{speciesName}'\r\nORDER BY species.Name DESC;";
            }

            return query;
        }
    }
}

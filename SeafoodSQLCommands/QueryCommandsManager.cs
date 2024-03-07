using System;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace SeafoodSQLCommands
{
    public class QueryCommandsManager
    {
        public SqlCommand GetAllSpeciesNamesAndIds(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT Name, SpeciesId\r\nFROM SpeciesTable species \r\n;", connection);
            return command;
        }

        public SqlCommand GetAllSpecies(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT Name FROM SpeciesTable", connection);
            return command;
        }

        public SqlCommand GetSpeciesById(int speciesId, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand($"SELECT Name FROM SpeciesTable WHERE SpeciesId = @speciesId", connection);
            command.Parameters.AddWithValue("@speciesId", speciesId);
            return command;
        }

        public SqlCommand GetAllSpeciesFullCatchInfo(SqlConnection connection) 
        {
            SqlCommand command = new SqlCommand("SELECT *\r\nFROM SpeciesTable species \r\nINNER JOIN CatchesTable catches ON species.SpeciesId = catches.SpeciesId\r\nORDER BY species.Name DESC;", connection);
            return command;
        }

        public SqlCommand GetAllSpeciesFullCatchInfoByName(string name, SqlConnection connection)
        {
            SqlCommand command;

            if (int.TryParse(name, out var speciesId)) 
            {
                command = new SqlCommand($"SELECT *\r\nFROM SpeciesTable species \r\nINNER JOIN CatchesTable catches ON species.SpeciesId = catches.SpeciesId\r\nWHERE species.SpeciesId = @speciesId\r\nORDER BY species.Name DESC;", connection);
                command.Parameters.AddWithValue("@speciesId", speciesId);
            }
            else
            {
                command = new SqlCommand($"SELECT *\r\nFROM SpeciesTable species \r\nINNER JOIN CatchesTable catches ON species.SpeciesId = catches.SpeciesId\r\nWHERE species.Name = @name\r\nORDER BY species.Name DESC;", connection);
                command.Parameters.AddWithValue("@name", name);
            }

            return command;
        }

        public SqlCommand GetSpecificInfoForSpecies(string speciesName, string speciesColumn, Dictionary<string, string> speciesNamesAndIDs_RawData, SqlConnection connection)
        {
            SqlCommand command;

            if (int.TryParse(speciesName, out var speciesId))
            {
                string speciesId_string = speciesId.ToString();
                speciesName = speciesNamesAndIDs_RawData[speciesId_string];
                Console.WriteLine($"Species Name: {speciesName}");

                command = new SqlCommand($"SELECT {speciesColumn}\r\nFROM SpeciesTable species \r\nINNER JOIN CatchesTable catches ON species.SpeciesId = catches.SpeciesId\r\nWHERE species.SpeciesId = '{speciesId}'\r\nORDER BY species.Name DESC;", connection);
                command.Parameters.AddWithValue("@speciesId", speciesId);
            }
            else
            {
                Console.WriteLine($"Species Name: {speciesName}");
                command = new SqlCommand($"SELECT {speciesColumn}\r\nFROM SpeciesTable species \r\nINNER JOIN CatchesTable catches ON species.SpeciesId = catches.SpeciesId\r\nWHERE species.Name = '{speciesName}'\r\nORDER BY species.Name DESC;", connection);
            }

            return command;
        }
    }
}

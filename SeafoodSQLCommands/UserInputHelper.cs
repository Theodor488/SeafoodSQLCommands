using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SeafoodSQLCommands
{
    public class UserInputHelper
    {
        public SqlCommand RunChosenQuery(QueryCommandsManager queryCommandsManager, string choice, Dictionary<string, string> speciesNamesAndIDs, List<string> allowedColumnsSection, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand();

            switch (choice)
            {
                case "0":
                    command = queryCommandsManager.GetAllSpeciesNamesAndIds(connection);
                    break;
                case "1":
                    command = queryCommandsManager.GetAllSpecies(connection);
                    break;
                case "2":
                    command = queryCommandsManager.GetSpeciesById(GetSpeciesIdFromUser(speciesNamesAndIDs), connection);
                    break;
                case "3":
                    command = queryCommandsManager.GetAllSpeciesFullCatchInfo(connection);
                    break;
                case "4":
                    command = queryCommandsManager.GetAllSpeciesFullCatchInfoByName(GetSpeciesNameFromUser(speciesNamesAndIDs), connection);
                    break;
                case "5":
                    command = queryCommandsManager.GetSpecificInfoForSpecies(GetSpeciesNameFromUser(speciesNamesAndIDs), GetSpeciesColumnFromUser(allowedColumnsSection), speciesNamesAndIDs, connection);
                    break;
            }

            return command;
        }

        public string ChooseQueryToRun()
        {
            Console.WriteLine("Choose a query to run: ");
            foreach (QueryOption option in Enum.GetValues(typeof(QueryOption)))
            {
                Console.WriteLine($"{(int)option}. {option}");
            }

            Console.Write("Enter the number of the query you want to run: ");
            string choice = Console.ReadLine();
            return choice;
        }

        // Helper methods to get user input for specific queries
        private static int GetSpeciesIdFromUser(Dictionary<string, string> speciesNamesAndIDs)
        {
            Console.Write("Enter the species ID: ");
            string speciesId = Console.ReadLine();

            if (speciesNamesAndIDs.ContainsKey(speciesId))
            {
                return int.Parse(speciesId);
            }
            else
            {
                throw new Exception("Invalid Species ID");
            }
        }

        private static string GetSpeciesNameFromUser(Dictionary<string, string> speciesNamesAndIDs)
        {
            Console.Write("Enter the species name: ");
            string speciesName = Console.ReadLine();

            if (speciesNamesAndIDs.ContainsValue(speciesName)) 
            {
                return speciesName;
            }
            else
            {
                throw new Exception("Invalid SpeciesName");
            }
        }

        private static string GetSpeciesColumnFromUser(List<string> allowedColumnsSection)
        {
            bool keepChoosing = true;

            while (keepChoosing)
            {
                Console.Write("Enter the species column: ");
                string columnChoice = Console.ReadLine();

                // Limiting column choices to allowedColumnsSection protects against SQL Injection Attacks
                if (allowedColumnsSection.Contains(columnChoice))
                {
                    keepChoosing = false;
                    return columnChoice;
                }
                else
                {
                    Console.WriteLine("Invalid column choice. Choose a column from the list of allowed columns: ");
                    Console.WriteLine(string.Format("Allowed Columns: ({0}).", string.Join(", ", allowedColumnsSection)));
                }
            }

            return string.Empty;
        }
    }
}

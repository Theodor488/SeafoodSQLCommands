using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeafoodSQLCommands
{
    public class UserInputHelper
    {
        public SqlCommand RunChosenQuery(QueryCommandsManager queryCommandsManager, string choice, Dictionary<string, string> speciesNamesAndIDs, SqlConnection connection)
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
                    command = queryCommandsManager.GetSpeciesById(GetSpeciesIdFromUser(), connection);
                    break;
                case "3":
                    command = queryCommandsManager.GetAllSpeciesFullCatchInfo(connection);
                    break;
                case "4":
                    command = queryCommandsManager.GetAllSpeciesFullCatchInfoByName(GetSpeciesNameFromUser(), connection);
                    break;
                case "5":
                    command = queryCommandsManager.GetSpecificInfoForSpecies(GetSpeciesNameFromUser(), GetSpeciesColumnFromUser(), speciesNamesAndIDs, connection);
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
        private static int GetSpeciesIdFromUser()
        {
            Console.Write("Enter the species ID: ");
            return int.Parse(Console.ReadLine());
        }

        private static string GetSpeciesNameFromUser()
        {
            Console.Write("Enter the species name: ");
            return Console.ReadLine();
        }

        private static string GetSpeciesColumnFromUser()
        {
            Console.Write("Enter the species column: ");
            return Console.ReadLine();
        }
    }
}

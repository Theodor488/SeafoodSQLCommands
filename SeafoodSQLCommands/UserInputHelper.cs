using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeafoodSQLCommands
{
    public class UserInputHelper
    {
        public string RunChosenQuery(QueryCommandsManager queryCommandsManager, string choice, Dictionary<string, string> speciesNamesAndIDs)
        {
            string query = "";

            switch (choice)
            {
                case "0":
                    query = queryCommandsManager.GetAllSpeciesNamesAndIds();
                    break;
                case "1":
                    query = queryCommandsManager.GetAllSpecies();
                    break;
                case "2":
                    query = queryCommandsManager.GetSpeciesById(GetSpeciesIdFromUser());
                    break;
                case "3":
                    query = queryCommandsManager.GetAllSpeciesFullCatchInfo();
                    break;
                case "4":
                    query = queryCommandsManager.GetAllSpeciesFullCatchInfoByName(GetSpeciesNameFromUser());
                    break;
                case "5":
                    query = queryCommandsManager.GetSpecificInfoForSpecies(GetSpeciesNameFromUser(), GetSpeciesColumnFromUser(), speciesNamesAndIDs);
                    break;
            }

            return query;
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

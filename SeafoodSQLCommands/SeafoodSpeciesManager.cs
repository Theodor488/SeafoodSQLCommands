using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeafoodSQLCommands
{
    public class SeafoodSpeciesManager
    {
        public SeafoodSpeciesManager() { }

        public string GetAllSpecies()
        {
            string query = "SELECT Name FROM SpeciesTable";

            return query;
        }
    }
}

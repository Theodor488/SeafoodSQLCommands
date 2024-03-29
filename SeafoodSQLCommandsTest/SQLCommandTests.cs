using Xunit;
using System.Data.SqlClient; 
using Moq;
using SeafoodSQLCommands;

namespace SeafoodSQLCommands
{
    public class SQLCommandTests
    {
        string connectionString = "Data Source=THEO-COMPUTER\\SQLEXPRESS;Initial Catalog=SeafoodDB;Integrated Security=True";

        [Theory]
        [InlineData("SELECT Remarks\r\nFROM SpeciesTable species \r\nINNER JOIN CatchesTable catches ON species.SpeciesId = catches.SpeciesId\r\nWHERE species.Name = 'Tuna'\r\nORDER BY species.Name DESC;", true)]
        public void TestSqlCommandOutput(string queryString, bool hasOutput)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();

                    Assert.Equal(hasOutput, reader.HasRows);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        [Fact]
        public void TestSqlInjection()
        {
            string sqlInjectionInput = "SELECT * FROM SecretUserInfoTable";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    QueryCommandsManager queryCommandsManager = new QueryCommandsManager();

                    Assert.Throws<Exception>(() => queryCommandsManager.GetAllSpeciesFullCatchInfoByName(sqlInjectionInput, connection));
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}


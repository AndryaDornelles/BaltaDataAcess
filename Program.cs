
using BaltaDataAccess.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BaltaDataAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            //String de conexao
            const string connectionString = "Server=127.0.0.1,1433;Database=Balta;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=true";
            // using cria um bloco de código que é executado e depois descartado
            using (var connection = new SqlConnection(connectionString))
            {
                // query faz a consulta no banco de dados em lista de categorias, usando comando sql
                var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
                // foreach faz um loop em cada categoria
                foreach (var category in categories)
                {
                    Console.WriteLine($"{category.Id} - {category.Title}");
                }
            }
        }
    }
}

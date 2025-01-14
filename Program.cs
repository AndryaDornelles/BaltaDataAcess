using System;
using Microsoft.Data.SqlClient;

namespace BaltaDataAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            //String de conexao
            const string connectionString = "Server=127.0.0.1,1433;Database=Balta;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=true";

            //Conexao com o banco de dados
            // using serve para fechar a conexao automaticamente
            using (var connection = new SqlConnection(connectionString))
            {
                Console.WriteLine("Conectado");
                connection.Open();

                //Command SQL serve para executar comandos no banco de dados
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    // CommandType.Text é o tipo de comando que sera executado, nesse caso um comando de texto
                    command.CommandType = System.Data.CommandType.Text;
                    // Comando SQL que sera executado
                    command.CommandText = "SELECT [Id], [Title] FROM [Category]";

                    // Reader serve para ler os dados retornados pelo comando SQL, executeReader executa o comando SQL
                    var reader = command.ExecuteReader();
                    // Enquanto houver dados para serem lidos
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader.GetGuid(0)} - {reader.GetString(1)}");
                    }
                }
            }
            Console.WriteLine("Hello World!");
        }
    }
}
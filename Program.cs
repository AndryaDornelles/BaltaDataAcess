﻿using System;
using System.Data;
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
                // ListCategories(connection);
                // CreateCategory(connection); // Descomente para inserir uma nova categoria
                // CreateManyCategory(connection); // Descomente para inserir muitas categorias
                // UpdateCategory(connection); // Descomente para atualizar uma categoria
                // DeleteCategory(connection); // Descomente para deletar uma categoria
                // ExecuteProcedure(connection); // Descomente para executar uma procedure
                // ExecuteReadProcedure(connection); // 
                // ExecuteScalar(connection);
                // ReadView(connection);
                // OnetoOne(connection);
                // OnetoMany(connection);
                // QueryMultiple(connection);
                // SelectIn(connection);
                // Like(connection, "backend");
                Transaction(connection);
            }
        }
        // Metodo para listar categorias
        static void ListCategories(SqlConnection connection)
        {
            // query faz a consulta no banco de dados em lista de categorias, usando comando sql
            var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
            // foreach faz um loop em cada categoria
            foreach (var item in categories)
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }
        }
        // Metodo para criar uma categoria
        static void CreateCategory(SqlConnection connection)
        {
            //Instancia de categoria
            var category = new Category();
            // Atribuicao de valores
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Summary = "Amazon AWS cloud";
            category.Order = 8;
            category.Description = "Amazon AWS descricao";
            category.Featured = false;

            //Query de insercao
            // @ é um parametro que será substituido pelo valor da variavel
            var insertSql = @"INSERT INTO [Category] 
                                VALUES(
                            @Id, 
                            @Title,
                            @Url, 
                            @Summary, 
                            @Order, 
                            @Description, 
                            @Featured)";

            // Executa a query de inserção no banco de dados e retorna a quantidade de linhas afetadas
            var rows = connection.Execute(insertSql, new
            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });
            Console.WriteLine($"{rows} linhas inseridas");
        }
        // Metodo para atualizar uma categoria
        static void UpdateCategory(SqlConnection connection)
        {
            // Query de atualizacao
            var updateQuery = "UPDATE [Category] SET [Title] = @title WHERE [Id] = @id";
            // Executa a query de atualizacao no banco de dados e retorna a quantidade de linhas afetadas
            var rows = connection.Execute(updateQuery, new
            {
                id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"),
                title = "Frontend 2025"
            });
            Console.WriteLine($"{rows} registros atualizados");
        }
        // Metodo para deletar uma categoria
        static void DeleteCategory(SqlConnection connection)
        {
            // Query de exclusao
            var deleteQuery = "DELETE FROM [Category] WHERE [Id] = @id";
            // Executa a query de exclusao no banco de dados e retorna a quantidade de linhas afetadas
            var rows = connection.Execute(deleteQuery, new
            {
                id = new Guid("d74f79b4-aab6-4f5c-b979-e07cc9454be4")
            });
            Console.WriteLine($"{rows} registros deletados");
        }
        // Metodo para criar muitas categorias
        static void CreateManyCategory(SqlConnection connection)
        {
            //Instancia de categoria
            var category = new Category();
            // Atribuicao de valores
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Summary = "Amazon AWS cloud";
            category.Order = 8;
            category.Description = "Amazon AWS descricao";
            category.Featured = false;

            var Category2 = new Category();
            // Atribuicao de valores
            Category2.Id = Guid.NewGuid();
            Category2.Title = "Categoria Nova";
            Category2.Url = "categoria-nova";
            Category2.Summary = "Categoria nova resumo";
            Category2.Order = 9;
            Category2.Description = "Categoria nova descricao";
            Category2.Featured = false;

            //Query de insercao
            // @ é um parametro que será substituido pelo valor da variavel
            var insertSql = @"INSERT INTO [Category] 
                                VALUES(
                            @Id, 
                            @Title,
                            @Url, 
                            @Summary, 
                            @Order, 
                            @Description, 
                            @Featured)";

            // Executa a query de inserção no banco de dados e retorna a quantidade de linhas afetadas
            var rows = connection.Execute(insertSql, new[]{
            new
            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            },
            new
            {
                Category2.Id,
                Category2.Title,
                Category2.Url,
                Category2.Summary,
                Category2.Order,
                Category2.Description,
                Category2.Featured
            }
            });
            Console.WriteLine($"{rows} linhas inseridas");
        }
        // Metodo para executar uma procedure
        static void ExecuteProcedure(SqlConnection connection)
        {
            // Query de execucao de procedure
            var procedure = "[spDeleteStudent]";
            // Cria um objeto com o parametro StudentId
            var pars = new { StudentId = "8a395934-6464-4f63-963a-25cbacea1512" };
            // Executa a query no banco de dados e retorna a quantidade de linhas afetadas
            var affectedRows = connection.Execute(procedure, pars, commandType: CommandType.StoredProcedure);

            Console.WriteLine($"{affectedRows} registros deletados");
        }
        static void ExecuteReadProcedure(SqlConnection connection)
        {
            var procedure = "[spGetCoursesByCategory]";
            var pars = new { CategoryId = "09ce0b7b-cfca-497b-92c0-3290ad9d5142" };
            var courses = connection.Query(procedure, pars, commandType: CommandType.StoredProcedure);

            foreach (var item in courses)
            {
                Console.WriteLine(item.Title);
            }
        }
        static void ExecuteScalar(SqlConnection connection)
        {
            //Instancia de categoria
            var category = new Category();
            // Atribuicao de valores
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Summary = "Amazon AWS cloud";
            category.Order = 8;
            category.Description = "Amazon AWS descricao";
            category.Featured = false;

            //Query de insercao
            // @ é um parametro que será substituido pelo valor da variavel
            var insertSql = @"INSERT INTO [Category] 
                              OUTPUT inserted.[Id]
                            VALUES(
                                NEWID(), 
                                @Title,
                                @Url, 
                                @Summary, 
                                @Order, 
                                @Description, 
                                @Featured) 
                        SELECT SCOPE_IDENTITY()";
            // Executa a query de inserção no banco de dados e retorna a quantidade de linhas afetadas
            var id = connection.ExecuteScalar<Guid>(insertSql, new
            {
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });
            Console.WriteLine($"A categoria inserida foi: {id}");
        }
        static void ReadView(SqlConnection connection)
        {
            var sql = "SELECT * FROM [vwCourses]";
            var courses = connection.Query(sql);

            foreach (var item in courses)
            {
                Console.WriteLine($"{item.Id} -- {item.Title}");
            }
        }
        static void OnetoOne(SqlConnection connection)
        {
            var sql = @"
                SELECT * FROM [CareerItem] 
                INNER JOIN [Course]
                ON [CareerItem].[CourseId] = [Course].[Id]";

            var items = connection.Query<CareerItem, Course, CareerItem>(
                sql,
                (careerItem, course) =>
                {
                    careerItem.Course = course;
                    return careerItem;
                }, splitOn: "Id");

            foreach (var item in items)
            {
                Console.WriteLine($"{item.Title} - Curso: {item.Course}");
            }
        }
        static void OnetoMany(SqlConnection connection)
        {
            var sql = @"
               SELECT 
                    [Career].[Id],
                    [Career].[Title],
                    [CareerItem].[CareerId],    
                    [CareerItem].[Title]
                FROM 
                    [Career] 
                INNER JOIN 
                    [CareerItem] ON [CareerItem].[CareerId] = [Career].[Id]
                ORDER BY 
                    [Career].[Title]";

            var careers = new List<Career>();
            var items = connection.Query<Career, CareerItem, Career>(
                sql,
                (career, item) =>
                {
                    var car = careers.Where(x => x.Id == career.Id).FirstOrDefault();

                    if (car == null)
                    {
                        car = career;
                        car.Items.Add(item);
                        careers.Add(car);
                    }
                    else
                    {
                        car.Items.Add(item);
                    }
                    return career;
                }, splitOn: "CareerId");

            foreach (var career in careers)
            {
                Console.WriteLine($"{career.Title}");
                foreach (var item in career.Items)
                {
                    Console.WriteLine($"  -  {item.Title}");
                }
            }
        }
        static void QueryMultiple(SqlConnection connection)
        {
            var sql = @"
                SELECT * FROM [Category];
                SELECT * FROM [Course];";

            using (var multi = connection.QueryMultiple(sql))
            {
                var categories = multi.Read<Category>().ToList();
                var courses = multi.Read<Course>().ToList();

                foreach (var item in categories)
                {
                    Console.WriteLine(item.Title);
                }
                foreach (var item in courses)
                {
                    Console.WriteLine(item.Title);
                }
            }
        }
        static void SelectIn(SqlConnection connection)
        {
            var sql = @"SELECT * FROM [Career] WHERE [Id] IN @Id";

            var items = connection.Query<Career>(sql, new
            {
                Id = new[] {
                    "4327ac7e-963b-4893-9f31-9a3b28a4e72b",
                    "e6730d1c-6870-4df3-ae68-438624e04c72"
                }
            });

            foreach (var item in items)
            {
                Console.WriteLine(item.Title);
            }
        }
        static void Like(SqlConnection connection, string term)
        {
            var sql = @"SELECT * FROM [Course] WHERE [Title] LIKE @exp";

            var items = connection.Query<Course>(sql, new
            {
                exp = $"%{term}%"
            });

            foreach (var item in items)
            {
                Console.WriteLine(item.Title);
            }
        }
        static void Transaction(SqlConnection connection)
        {
            //Instancia de categoria
            var category = new Category();
            // Atribuicao de valores
            category.Id = Guid.NewGuid();
            category.Title = "Minha Categoria q nn quero";
            category.Url = "amazon";
            category.Summary = "Amazon AWS cloud";
            category.Order = 8;
            category.Description = "Amazon AWS descricao";
            category.Featured = false;

            //Query de insercao
            // @ é um parametro que será substituido pelo valor da variavel
            var insertSql = @"INSERT INTO [Category] 
                                VALUES(
                            @Id, 
                            @Title,
                            @Url, 
                            @Summary, 
                            @Order, 
                            @Description, 
                            @Featured)";

            connection.Open();

            using var transaction = connection.BeginTransaction();
            var rows = connection.Execute(insertSql, new
            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            }, transaction);

            // transaction.Commit(); //Só commit se tiver essa linha
            transaction.Rollback();
            Console.WriteLine($"{rows} linhas inseridas");
        }
    }
}
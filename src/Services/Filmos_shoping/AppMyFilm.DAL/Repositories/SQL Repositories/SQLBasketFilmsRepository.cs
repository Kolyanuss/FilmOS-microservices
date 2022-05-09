using AppMyFilm.DAL.Entities.SQLEntities;
using AppMyFilm.DAL.Interfaces.SQLInterfaces.ISQLRepositories;
using Microsoft.Extensions.Configuration;
using SkillManagement.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AppMyFilm.DAL.Repositories.SQL_Repositories
{
    public class SQLBasketFilmsRepository : ISQLBasketFilmsRepository
    {
        protected IConnectionFactory _connectionFactory;
        private static readonly string _tableName = "Basket_films";

        public SQLBasketFilmsRepository(IConnectionFactory connectionFactory, IConfiguration config)
        {
            _connectionFactory = connectionFactory;
            var connectionString = config.GetConnectionString("DefaultConnection3");
            _connectionFactory.SetConnection(connectionString);
        }

        public async IAsyncEnumerable<SQLBasketFilms> Get(string sqlExpression)
        {
            using (SqlConnection connection = (SqlConnection)_connectionFactory.GetSqlAsyncConnection)
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        long Field1 = reader.GetInt32(0);
                        long Field2 = reader.GetInt32(1);

                        yield return new SQLBasketFilms(Field1, Field2);
                    }
                }
                reader.Close();
            }
            yield break;
        }

        public IAsyncEnumerable<SQLBasketFilms> GetAll()//
        {
            return Get("SELECT * FROM " + _tableName);
        }

        public IAsyncEnumerable<SQLBasketFilms> GetByIdFilms(long Id)
        {
            return Get("SELECT * FROM " + _tableName + " WHERE IdFilms=" + Id);

        }

        public async IAsyncEnumerable<SQLBasketFilms> GetByIdUsers(long Id)
        {
            // название процедуры
            string sqlExpression = "Show_basket_films_by_id_user";

            using (SqlConnection connection = (SqlConnection)_connectionFactory.GetSqlAsyncConnection)
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            /*int id = reader.GetInt32(0);
                            string name = reader.GetString(2);
                            int age = reader.GetInt32(1);
                            Console.WriteLine($"{id} \t{name} \t{age}");*/
                            long Field1 = reader.GetInt32(0);
                            String Field2 = reader.GetString(1);
                            long Field3 = reader.GetInt32(2);
                            yield return new SQLBasketFilms(Field1, Field3);
                        }
                    }
                }
            }
            yield break;
        }

        public async IAsyncEnumerable<SQLListFilmsStr> GetFilmsJoinUser()
        {
            string sqlExpression = @"
            SELECT DISTINCT UserName, NameFilm FROM 
                (SELECT IdFilms as Id, NameFilm FROM ListFilms INNER JOIN Films ON ListFilms.IdFilms = Films.Id) as tab1
                INNER JOIN
                (SELECT IdFilms as Id, UserName FROM ListFilms INNER JOIN Users ON ListFilms.IdUser = Users.Id) as tab2
            ON tab1.Id = tab2.Id
		    ORDER BY UserName
            ";
            
            using (SqlConnection connection = (SqlConnection)_connectionFactory.GetSqlAsyncConnection)
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        string Field1 = reader.GetString(0);
                        string Field2 = reader.GetString(1);

                        yield return new SQLListFilmsStr(Field1, Field2);
                    }
                }
                reader.Close();
            }
            yield break;

        }

        public async Task<long> Add(SQLBasketFilms entity)
        {
            string sqlExpression = string.Format(
                "INSERT INTO {0} (IdFilms, IdUser) VALUES ({1},{2})",
                _tableName, entity.id_film, entity.id_user);

            using (SqlConnection connection = (SqlConnection)_connectionFactory.GetSqlAsyncConnection)
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                long num = await command.ExecuteNonQueryAsync();
                return num;
            }
        }

        public async void Update(SQLBasketFilms entity)
        {
            string sqlExpression = string.Format(@"
            UPDATE {0} SET IdFilms={1}
            WHERE IdUser={2}
            LIMIT 1
            ", _tableName, entity.id_film, entity.id_user);

            using (SqlConnection connection = (SqlConnection)_connectionFactory.GetSqlAsyncConnection)
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async void Delete(SQLBasketFilms entity)
        {
            string sqlExpression = string.Format(@"
            DELETE * FROM {0}
            WHERE IdFilms={1} && IdUser={2}
            LIMIT 1
            ", _tableName, entity.id_film, entity.id_user);

            using (SqlConnection connection = (SqlConnection)_connectionFactory.GetSqlAsyncConnection)
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}

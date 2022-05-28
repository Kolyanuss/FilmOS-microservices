using Shoping.DAL.Entities.SQLEntities;
using Shoping.DAL.Interfaces.SQLInterfaces.ISQLRepositories;
using Microsoft.Extensions.Configuration;
using Shoping.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Shoping.DAL.Repositories.SQL_Repositories
{
    public class SQLBasketFilmsRepository : ISQLBasketFilmsRepository
    {
        protected IConnectionFactory _connectionFactory;
        private static readonly string _tableName = "Basket_films";

        public SQLBasketFilmsRepository(IConnectionFactory connectionFactory, IConfiguration config)
        {
            _connectionFactory = connectionFactory;
            using (SqlConnection connection = (SqlConnection)_connectionFactory.GetSqlAsyncConnection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT TOP(1) * FROM " + _tableName, connection); // for fix
                    using (command.ExecuteReader()) { } // fix bug (first request always faild )
                }
                catch { }
            }
        }

        public async Task<IEnumerable<SQLBasketFilms>> Get(string sqlExpression)
        {
            var list = new List<SQLBasketFilms>();
            using (SqlConnection connection = (SqlConnection)_connectionFactory.GetSqlAsyncConnection)
            {
                
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new SQLBasketFilms() { id_film = reader.GetInt32(0), id_user = reader.GetInt32(1) });
                        }
                    }
                }
            }
            return list;
        }

        public async Task<IEnumerable<SQLBasketFilms>> GetAll()
        {
            return await Get("SELECT * FROM " + _tableName);
        }

        public async Task<IEnumerable<SQLBasketFilms>> GetByIdFilms(long Id)
        {
            return await Get("SELECT * FROM " + _tableName + " WHERE id_film=" + Id);
        }

        public async Task<IEnumerable<SQLBasketFilms>> GetByIdUsers(long Id)
        {
            // название процедуры
            string sqlExpression = "Show_basket_films_by_id_user";

            var list = new List<SQLBasketFilms>();
            using (SqlConnection connection = (SqlConnection)_connectionFactory.GetSqlAsyncConnection)
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@id_user", SqlDbType.Int).Value = Id;
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new SQLBasketFilms() { id_film = reader.GetInt32(0), id_user = reader.GetInt32(1) });
                        }
                    }
                }
            }
            return list;
        }

        public async Task<IEnumerable<SQLListFilmsStr>> GetFilmsJoinUser()
        {
            string sqlExpression = @"
            SELECT DISTINCT name_film, [user_name] FROM 

	            (SELECT id_film, name_film 
                    FROM Basket_films INNER JOIN Films 
                ON Films.Id = Basket_films.id_film) as tab1

	            INNER JOIN

	            (SELECT id_film, [user_name] 
                    FROM Basket_films INNER JOIN Users 
                ON Users.Id = Basket_films.id_user) as tab2

            ON tab1.id_film = tab2.id_film
            ";

            var list = new List<SQLListFilmsStr>();
            using (SqlConnection connection = (SqlConnection)_connectionFactory.GetSqlAsyncConnection)
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new SQLListFilmsStr(reader.GetString(0), reader.GetString(1)));
                        }
                    }
                }
            }
            return list;
        }

        public async Task<(long,long)> Add(SQLBasketFilms entity)
        {
            string sqlExpression = string.Format(
                @"INSERT INTO {0} (id_film, id_user) VALUES ({1},{2})",
                _tableName, entity.id_film, entity.id_user);

            using (SqlConnection connection = (SqlConnection)_connectionFactory.GetSqlAsyncConnection)
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                await command.ExecuteNonQueryAsync();
                return (entity.id_film, entity.id_user);
            }
        }

        public async Task Delete(long id_film, long id_user)
        {
            string sqlExpression = string.Format(
            @"DELETE FROM {0}
            WHERE id_film={1} AND id_user={2}",
            _tableName, id_film, id_user);

            using (SqlConnection connection = (SqlConnection)_connectionFactory.GetSqlAsyncConnection)
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Delete(long idUser) // dellete all record by user id
        {
            string sqlExpression = string.Format(
            @"DELETE FROM {0}
            WHERE id_user={1}",
            _tableName, idUser);

            using (SqlConnection connection = (SqlConnection)_connectionFactory.GetSqlAsyncConnection)
            {
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}

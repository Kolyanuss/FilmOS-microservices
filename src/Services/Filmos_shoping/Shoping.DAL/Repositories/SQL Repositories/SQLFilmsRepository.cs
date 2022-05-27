using Microsoft.Extensions.Configuration;
using Shoping.DAL.Core;
using Shoping.DAL.Interfaces;
using Shoping.DAL.Interfaces.SQLInterfaces.ISQLRepositories;
using Shoping.DAL.Entities.SQLEntities;
using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Threading.Tasks;

namespace Shoping.DAL.Repositories.SQL_Repositories
{
    public class SQLFilmsRepository : GenericRepository<SQLFilms, int>, ISQLFilmsRepository
    {
        private static readonly string _tableName = "Films";
        private static readonly bool _isSoftDelete = false;
        public SQLFilmsRepository(IConnectionFactory connectionFactory, IConfiguration config) : base(connectionFactory, _tableName, _isSoftDelete)
        {
            //var connectionString = config.GetConnectionString("test");
            //connectionFactory.SetConnection(connectionString);
        }

        public async Task<IEnumerable<SQLFilms>> GetNotPopularFilms()
        {
            string sqlExpression = @"
            SELECT [id],[name_film],[release_data],[country],[type_price_id]
                FROM Basket_films RIGHT JOIN Films ON Basket_films.id_film = Films.Id  
            WHERE Basket_films.id_film IS NULL
            ";

            using (var db = _connectionFactory.GetSqlAsyncConnection)
            {
                return await db.QueryAsync<SQLFilms>(sqlExpression);
            }
        }

        public async Task<IEnumerable<SQLFilms>> GetFreeFilms()
        {
            string sqlExpression = "Select_free_films";

            using (var db = _connectionFactory.GetSqlAsyncConnection)
            {
                return await db.QueryAsync<SQLFilms>(sqlExpression,
                    new { P_tableName = _tableName },
                    commandType: CommandType.StoredProcedure);
            }
        }

    }
}

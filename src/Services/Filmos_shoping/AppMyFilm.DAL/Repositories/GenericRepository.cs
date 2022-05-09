using Dapper;
using SkillManagement.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SkillManagement.DataAccess.Core
{
    public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId> where TEntity : IEntity<TId>
    {
        protected IConnectionFactory _connectionFactory;
        private readonly string _tableName;
        private readonly bool _isSoftDelete;

        public GenericRepository(IConnectionFactory connectionFactory, string tableName, bool isSoftDelete)
        {
            _connectionFactory = connectionFactory;
            _tableName = tableName;
            _isSoftDelete = isSoftDelete;
        }

        public async Task<TEntity> Get(TId Id)
        {
            var query = "SP_GetRecordByIdFromTable";

            using (var db = _connectionFactory.GetSqlConnection)
            {
                return await db.QuerySingleOrDefaultAsync<TEntity>(query,
                    new { P_tableName = _tableName, P_Id = Id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var query = "SP_GetAllRecordsFromTable";

            using (var db = _connectionFactory.GetSqlConnection)
            {
                return await db.QueryAsync<TEntity>(query,
                    new { P_tableName = _tableName },
                    commandType: CommandType.StoredProcedure);
            }
        }

        private IEnumerable<string> GetColumns()
        {
            return typeof(TEntity)
                    .GetProperties()
                    .Where(e => e.Name != "Id" && !e.PropertyType.GetTypeInfo().IsGenericType)
                    .Select(e => e.Name);
        }


        public async Task<int> Add(TEntity entity)
        {
            var columns = GetColumns();
            var stringOfColumns = string.Join(", ", columns);
            var stringOfProperties = string.Join(", ", columns.Select(e => "@" + e));
            var query = "SP_InsertRecordToTable";

            using (var db = _connectionFactory.GetSqlConnection)
            {
                var InsertionStatement = await db.QuerySingleOrDefaultAsync<string>(
                    sql: query,
                    param: new { P_tableName = _tableName, P_columnsString = stringOfColumns, P_propertiesString = stringOfProperties },
                    commandType: CommandType.StoredProcedure);

                var InsertedEntityId = await db.ExecuteAsync(
                    sql: InsertionStatement,
                    param: entity,
                    commandType: CommandType.Text);

                return InsertedEntityId;
            }
        }

        public async Task Update(TEntity entity)
        {
            var columns = GetColumns();
            var stringOfColumns = string.Join(", ", columns.Select(e => $"{e} = @{e}"));

            using (var db = _connectionFactory.GetSqlAsyncConnection)
            {
                var query = "SP_UpdateRecordInTable";

                var UpdateStatement = await db.QuerySingleOrDefaultAsync<string>(
                    sql: query,
                    param: new { P_tableName = _tableName, P_columnsString = stringOfColumns, P_Id = entity.Id },
                    commandType: CommandType.StoredProcedure);

                await db.ExecuteAsync(
                    sql: UpdateStatement,
                    param: entity,
                    commandType: CommandType.Text);
            }
        }

        public async Task Delete(TEntity entity)
        {
            if (_isSoftDelete) // applying soft delete
            {
                var columns = GetColumns();
                var isActiveColumnUpdateString = columns.Where(e => e == "IsActive").Select(e => $"{e} = 0").FirstOrDefault();

                using (var db = _connectionFactory.GetSqlAsyncConnection)
                {
                    var query = "SP_UnActivateRecordInTable";

                    var UnActivateStatement = await db.QuerySingleOrDefaultAsync<string>(
                        sql: query,
                        param: new { P_tableName = _tableName, P_columnsString = isActiveColumnUpdateString, P_Id = entity.Id },
                        commandType: CommandType.StoredProcedure);

                    await db.ExecuteAsync(
                        sql: UnActivateStatement,
                        param: entity,
                        commandType: CommandType.Text);
                }
            }
            else // delete directly
            {
                using (var db = _connectionFactory.GetSqlAsyncConnection)
                {
                    var query = "SP_DeleteRecordFromTable";
                    var result = await db.ExecuteAsync(
                        sql: query,
                        param: new { P_tableName = _tableName, P_Id = entity.Id },
                        commandType: CommandType.StoredProcedure);
                }
            }
        }
    }
}

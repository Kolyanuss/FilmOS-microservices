using System.Data;

namespace Shoping.DAL.Interfaces
{
    public interface IConnectionFactory
    {
        IDbConnection GetSqlConnection { get; }
        IDbConnection GetSqlAsyncConnection { get; }
        void SetConnection(string connectionString);
    }
}

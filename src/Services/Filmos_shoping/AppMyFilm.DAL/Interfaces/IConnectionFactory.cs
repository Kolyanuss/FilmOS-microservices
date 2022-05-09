using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillManagement.DataAccess.Interfaces
{
    public interface IConnectionFactory
    {
        IDbConnection GetSqlConnection { get; }
        IDbConnection GetSqlAsyncConnection { get; }
        void SetConnection(string connectionString);
    }
}

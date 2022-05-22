using Shoping.DAL.Entities.SQLEntities;
using Shoping.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoping.DAL.Interfaces.SQLInterfaces.ISQLRepositories
{
    public interface ISQLFilmsRepository : IGenericRepository<SQLFilms, int>
    {
        Task<IEnumerable<SQLFilms>> GetNotPopularFilms();
        Task<IEnumerable<SQLFilms>> GetFreeFilms();
    }
}

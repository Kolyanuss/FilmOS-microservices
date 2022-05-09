using AppMyFilm.DAL.Entities.SQLEntities;
using SkillManagement.DataAccess.Entities.SQLEntities;
using SkillManagement.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppMyFilm.DAL.Interfaces.SQLInterfaces.ISQLRepositories
{
    public interface ISQLFilmsRepository : IGenericRepository<SQLFilms, int>
    {
        Task<IEnumerable<SQLFilms>> GetNotPopularFilms();
        Task<IEnumerable<SQLFilms>> GetFreeFilms();
    }
}

using AppMyFilm.DAL.Entities.SQLEntities;
using SkillManagement.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppMyFilm.DAL.Interfaces.SQLInterfaces.ISQLRepositories
{
    public interface ISQLBasketFilmsRepository
    {
        IAsyncEnumerable<SQLBasketFilms> GetAll();

        IAsyncEnumerable<SQLBasketFilms> GetByIdFilms(long Id);

        IAsyncEnumerable<SQLBasketFilms> GetByIdUsers(long Id);

        Task<long> Add(SQLBasketFilms entity);

        void Update(SQLBasketFilms entity);

        void Delete(SQLBasketFilms entity);

        IAsyncEnumerable<SQLListFilmsStr> GetFilmsJoinUser();
    }
}

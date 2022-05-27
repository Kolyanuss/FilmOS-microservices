using Shoping.DAL.Entities.SQLEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoping.DAL.Interfaces.SQLInterfaces.ISQLRepositories
{
    public interface ISQLBasketFilmsRepository
    {
        Task<IAsyncEnumerable<SQLBasketFilms>> GetAll();

        Task<IAsyncEnumerable<SQLBasketFilms>> GetByIdFilms(long Id);

        IAsyncEnumerable<SQLBasketFilms> GetByIdUsers(long Id);

        IAsyncEnumerable<SQLListFilmsStr> GetFilmsJoinUser();

        Task<long> Add(SQLBasketFilms entity);
        Task Delete(SQLBasketFilms entity);
        Task Delete(long idUser);
    }
}

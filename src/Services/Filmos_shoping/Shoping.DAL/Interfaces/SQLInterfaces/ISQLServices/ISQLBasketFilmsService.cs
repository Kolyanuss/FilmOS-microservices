using Shoping.DAL.Entities.SQLEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoping.DAL.Interfaces.SQLInterfaces.ISQLServices
{
    public interface ISQLBasketFilmsService
    {
        Task<long> AddBasketFilm(SQLBasketFilms BasketFilm);
        Task DeleteBasketFilm(long idUser);
        Task DeleteBasketFilm(long idFilm, long idUser);

        Task<IAsyncEnumerable<SQLBasketFilms>> GetAllBasketFilms();
        Task<IAsyncEnumerable<SQLBasketFilms>> GetBasketByIdFilm(long Id);

        IAsyncEnumerable<SQLBasketFilms> GetBasketByIdUser(long Id);
        //IAsyncEnumerable<SQLBasketFilmsStr> GetBasketFilmsJoinUser();
    }
}

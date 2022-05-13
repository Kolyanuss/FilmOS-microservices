using Shoping.DAL.Entities.SQLEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoping.DAL.Interfaces.SQLInterfaces.ISQLServices
{
    public interface ISQLBasketFilmsService
    {
        Task<long> AddBasketFilm(SQLBasketFilms BasketFilm);

        void UpdateBasketFilm(SQLBasketFilms BasketFilm);

        void DeleteBasketFilm(SQLBasketFilms BasketFilm);

        IAsyncEnumerable<SQLBasketFilms> GetBasketFilmByIdFilm(long Id);

        IAsyncEnumerable<SQLBasketFilms> GetBasketFilmByIdUser(long Id);

        IAsyncEnumerable<SQLBasketFilms> GetAllBasketFilms();

        //IAsyncEnumerable<SQLBasketFilmsStr> GetBasketFilmsJoinUser();
    }
}

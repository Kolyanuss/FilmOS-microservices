using Shoping.DAL.Entities.SQLEntities;
using Shoping.DAL.EntitiesDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoping.DAL.Interfaces.SQLInterfaces.ISQLServices
{
    public interface ISQLBasketFilmsService
    {
        Task<(long, long)> AddBasketFilm(SQLBasketFilmsDTO BasketFilm);
        Task DeleteBasketFilm(long idUser);
        Task DeleteBasketFilm(long idFilm, long idUser);

        Task<IEnumerable<SQLBasketFilms>> GetAllBasketFilms();
        Task<IEnumerable<SQLBasketFilms>> GetBasketByIdFilm(long Id);
        Task<IEnumerable<SQLBasketFilms>> GetBasketByIdUser(long Id);
        Task<IEnumerable<SQLListFilmsStr>> GetBasketFilmsJoinUser();
    }
}

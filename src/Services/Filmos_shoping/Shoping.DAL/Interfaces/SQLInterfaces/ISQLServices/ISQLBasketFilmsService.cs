using Shoping.DAL.Entities.SQLEntities;
using Shoping.DAL.EntitiesDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoping.DAL.Interfaces.SQLInterfaces.ISQLServices
{
    public interface ISQLBasketFilmsService
    {
        Task<(int, int)> AddBasketFilm(SQLBasketFilmsDTO BasketFilm);
        Task DeleteBasketFilm(int idUser);
        Task DeleteBasketFilm(int idFilm, int idUser);

        Task<IEnumerable<SQLBasketFilms>> GetAllBasketFilms();
        Task<IEnumerable<SQLBasketFilms>> GetBasketByIdFilm(int Id);
        Task<IEnumerable<SQLBasketFilms>> GetBasketByIdUser(int Id);
        Task<IEnumerable<SQLListFilmsStr>> GetBasketFilmsJoinUser();
        Task<IEnumerable<int>> GetAllIdByUserName(string UserName);
    }
}

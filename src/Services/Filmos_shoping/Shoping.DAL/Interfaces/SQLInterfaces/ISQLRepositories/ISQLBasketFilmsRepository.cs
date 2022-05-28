using Shoping.DAL.Entities.SQLEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoping.DAL.Interfaces.SQLInterfaces.ISQLRepositories
{
    public interface ISQLBasketFilmsRepository
    {
        Task<IEnumerable<SQLBasketFilms>> GetAll();
        Task<IEnumerable<SQLBasketFilms>> GetByIdFilms(long Id);
        Task<IEnumerable<SQLBasketFilms>> GetByIdUsers(long Id);
        Task<IEnumerable<SQLListFilmsStr>> GetFilmsJoinUser();

        Task<(long, long)> Add(SQLBasketFilms entity);
        Task Delete(long id_film, long id_user);
        Task Delete(long idUser);
    }
}

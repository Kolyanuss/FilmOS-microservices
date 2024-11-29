using Shoping.DAL.Entities.SQLEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoping.DAL.Interfaces.SQLInterfaces.ISQLRepositories
{
    public interface ISQLBasketFilmsRepository
    {
        Task<IEnumerable<SQLBasketFilms>> GetAll();
        Task<SQLBasketFilms> GetByTwoId(int IdFilm, int IdUser);
        Task<IEnumerable<SQLBasketFilms>> GetByIdFilms(int Id);
        Task<IEnumerable<SQLBasketFilms>> GetByIdUsers(int Id);
        Task<IEnumerable<int>> GetAllIdByUserName(string UserName);
        Task<IEnumerable<SQLListFilmsStr>> GetFilmsJoinUser();

        Task<(int, int)> Add(SQLBasketFilms entity);
        Task Delete(int id_film, int id_user);
        Task Delete(int idUser);
    }
}

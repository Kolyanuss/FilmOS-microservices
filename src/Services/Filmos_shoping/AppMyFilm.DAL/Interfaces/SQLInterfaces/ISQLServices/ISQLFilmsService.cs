using AppMyFilm.DAL.Entities.SQLEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppMyFilm.DAL.Interfaces.SQLInterfaces.ISQLServices
{
    public interface ISQLFilmsService
    {
        Task<IEnumerable<SQLFilmsDTO>> GetAllFilms();

        Task<SQLFilmsDTO> GetFilmById(int Id);

        Task<IEnumerable<SQLFilmsDTO>> GetNotPopularFilms();

        Task<IEnumerable<SQLFilmsDTO>> GetFreeFilms();

        Task<int> AddFilm(SQLFilmsDTO filmDto);

        Task UpdateFilm(int id, SQLFilmsDTO filmDto);

        Task DeleteFilm(int id);
    }
}

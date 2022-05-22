using Shoping.DAL.EntitiesDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoping.DAL.Interfaces.SQLInterfaces.ISQLServices
{
    public interface ISQLFilmsService
    {
        Task<IEnumerable<SQLFilmsDTO>> GetAllFilms();

        Task<SQLFilmsDTO> GetFilmById(int Id);

        Task<IEnumerable<SQLFilmsDTO>> GetNotPopularFilms();

        Task<IEnumerable<SQLFilmsDTO>> GetFreeFilms();

        Task<int> AddFilm(SQLFilmsForAddDTO filmDto);

        Task UpdateFilm(int id, SQLFilmsForAddDTO filmDto);

        Task DeleteFilm(int id);
    }
}

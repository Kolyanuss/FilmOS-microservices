using EFCoreCodeFirstSampleWEBAPI.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCoreCodeFirstSampleWEBAPI.DAL.Interfaces.ISQLRepositories
{
    public interface IFilmsUsersRepository : IRepositoryBase<FilmsUsers>
    {
        Task<IEnumerable<FilmsUsers>> GetAllAsync();
        Task<FilmsUsers> GetByPairIdAsync(int id1, int id2);
        Task<IEnumerable<FilmsUsers>> GetAllUsersByFilmIdAsync(int id);
        Task<IEnumerable<FilmsUsers>> GetAllFilmsByUserIdAsync(int id);
    }
}

using EFCoreCodeFirstSampleWEBAPI.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCoreCodeFirstSampleWEBAPI.DAL.Interfaces.ISQLRepositories
{
    public interface IFilmsRepository : IRepositoryBase<Films>
    {
        Task<IEnumerable<Films>> GetAllAsync();
        Task<Films> GetByIdAsync(int id);
        Task<Films> GetByIdWithDetailsAsync(int id);
    }
}

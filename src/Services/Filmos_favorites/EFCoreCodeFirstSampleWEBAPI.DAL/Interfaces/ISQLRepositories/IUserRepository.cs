using EFCoreCodeFirstSampleWEBAPI.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCoreCodeFirstSampleWEBAPI.DAL.Interfaces.ISQLRepositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
    }
}

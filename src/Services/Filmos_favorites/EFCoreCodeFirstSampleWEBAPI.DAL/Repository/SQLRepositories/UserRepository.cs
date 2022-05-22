using EFCoreCodeFirstSampleWEBAPI.DAL.Interfaces.ISQLRepositories;
using EFCoreCodeFirstSampleWEBAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCoreCodeFirstSampleWEBAPI.DAL.Repository.SQLRepositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(MyAppContext myAppContext) : base(myAppContext)
        {
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await GetByCondition(e => e.Id == id).FirstOrDefaultAsync();
        }

    }
}

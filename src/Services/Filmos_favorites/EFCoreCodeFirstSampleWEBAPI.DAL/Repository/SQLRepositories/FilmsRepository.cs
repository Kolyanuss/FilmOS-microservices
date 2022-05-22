using EFCoreCodeFirstSampleWEBAPI.DAL.Interfaces.ISQLRepositories;
using EFCoreCodeFirstSampleWEBAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCoreCodeFirstSampleWEBAPI.DAL.Repository.SQLRepositories
{
    public class FilmsRepository : RepositoryBase<Films>, IFilmsRepository
    {
        public FilmsRepository(MyAppContext myAppContext) : base(myAppContext)
        {
        }

        public async Task<IEnumerable<Films>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<Films> GetByIdAsync(int id)
        {
            return await GetByCondition(e => e.Id == id).FirstOrDefaultAsync();
        }

        #region eager loading
        public async Task<Films> GetByIdWithDetailsAsync(int id)
        {
            return await GetByCondition(e => e.Id == id).Include(e => e.Description).FirstOrDefaultAsync();
        }
        #endregion

    }
}

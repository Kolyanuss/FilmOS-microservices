using EFCoreCodeFirstSampleWEBAPI.DAL.Interfaces.ISQLRepositories;
using EFCoreCodeFirstSampleWEBAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace EFCoreCodeFirstSampleWEBAPI.DAL.Repository.SQLRepositories
{
    public class FilmsUsersRepository : RepositoryBase<FilmsUsers>, IFilmsUsersRepository
    {
        public FilmsUsersRepository(MyAppContext myAppContext) : base(myAppContext)
        {
        }

        public async Task<IEnumerable<FilmsUsers>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<FilmsUsers> GetByPairIdAsync(int id1, int id2)
        {
            return await GetByCondition(e => e.IdFilms == id1 && e.IdUser == id2).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<FilmsUsers>> GetAllUsersByFilmIdAsync(int id)
        {
            return await GetByCondition(e => e.IdFilms == id).ToListAsync();
        }
        public async Task<IEnumerable<FilmsUsers>> GetAllFilmsByUserIdAsync(int id)
        {
            return await GetByCondition(e => e.IdUser == id).ToListAsync();
        }
    }
}

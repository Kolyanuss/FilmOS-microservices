using EFCoreCodeFirstSampleWEBAPI.DAL.Interfaces.ISQLRepositories;
using EFCoreCodeFirstSampleWEBAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCoreCodeFirstSampleWEBAPI.DAL.Repository.SQLRepositories
{
    public class FilmsRepository : RepositoryBase<Films>, IFilmsRepository
    {
        private readonly ILogger<IFilmsRepository> _logger;

        public FilmsRepository(MyAppContext myAppContext, ILogger<IFilmsRepository> logger,
            ILogger<RepositoryBase<Films>> loggerBase) : base(myAppContext, loggerBase)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Films>> GetAllAsync()
        {
            _logger.LogInformation("In "+ this.GetType() + " call GetAll");
            return await GetAll().ToListAsync();
        }

        public async Task<Films> GetByIdAsync(int id)
        {
            _logger.LogInformation("In " + this.GetType() + " call GetByCondition");
            return await GetByCondition(e => e.Id == id).FirstOrDefaultAsync();
        }

        #region eager loading
        public async Task<Films> GetByIdWithDetailsAsync(int id)
        {
            _logger.LogInformation("In " + this.GetType() + " call GetByCondition");
            return await GetByCondition(e => e.Id == id).Include(e => e.Description).FirstOrDefaultAsync();
        }
        #endregion

    }
}

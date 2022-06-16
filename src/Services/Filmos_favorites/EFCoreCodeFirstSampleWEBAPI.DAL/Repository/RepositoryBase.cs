using EFCoreCodeFirstSampleWEBAPI.DAL.Interfaces;
using EFCoreCodeFirstSampleWEBAPI.DAL.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EFCoreCodeFirstSampleWEBAPI.DAL.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected MyAppContext MyAppContext { get; set; }
        private readonly ILogger<RepositoryBase<T>> _logger;
        public RepositoryBase(MyAppContext myAppContext)
        {
            MyAppContext = myAppContext;
            _logger = null;
        }
        public RepositoryBase(MyAppContext myAppContext, ILogger<RepositoryBase<T>> logger)
        {
            MyAppContext = myAppContext;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IQueryable<T> GetAll()
        {
            _logger.LogInformation("In " + this.GetType() + " work GetAll");
            return MyAppContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            _logger.LogInformation("In " + this.GetType() + " work GetByCondition");
            return MyAppContext.Set<T>().Where(expression);
        }

        public IEnumerable<T> FindWithSpecificationPattern(ISpecification<T> specification = null)
        {
            _logger.LogInformation("In " + this.GetType() + " work FindWithSpecificationPattern");
            return SpecificationEvaluator<T>.GetQuery(MyAppContext.Set<T>().AsQueryable(), specification);
        }

        public async Task Add(T entity)
        {
            await MyAppContext.Set<T>().AddAsync(entity);
            MyAppContext.SaveChanges();
            _logger.LogInformation("In " + this.GetType() + " SaveChanges AddAsync");
        }

        public void Update(T entity)
        {
            MyAppContext.Set<T>().Update(entity);
            MyAppContext.SaveChanges();
            _logger.LogInformation("In " + this.GetType() + " SaveChanges Update");
        }

        public void Delete(T entity)
        {
            MyAppContext.Set<T>().Remove(entity);
            MyAppContext.SaveChanges();
            _logger.LogInformation("In " + this.GetType() + " SaveChanges Remove");
        }
    }
}

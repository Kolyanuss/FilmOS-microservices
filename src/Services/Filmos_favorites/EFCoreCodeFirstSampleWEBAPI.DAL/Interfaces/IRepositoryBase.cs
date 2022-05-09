using EFCoreCodeFirstSampleWEBAPI.DAL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EFCoreCodeFirstSampleWEBAPI.DAL.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
        IEnumerable<T> FindWithSpecificationPattern(ISpecification<T> specification = null);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}

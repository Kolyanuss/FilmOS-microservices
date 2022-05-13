using Shoping.DAL.Interfaces.EntityInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoping.DAL.Interfaces
{
    public interface IGenericRepository<TEntity, TId> where TEntity : IClearEntity
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> Get(TId Id);

        Task<int> Add(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(TEntity entity);
    }
}

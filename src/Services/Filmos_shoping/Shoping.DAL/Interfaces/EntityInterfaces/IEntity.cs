using Shoping.DAL.Interfaces.EntityInterfaces;

namespace Shoping.DAL.Interfaces
{
    public interface IEntity<T> : IClearEntity
    {
        T Id { get; set; }
    }
}

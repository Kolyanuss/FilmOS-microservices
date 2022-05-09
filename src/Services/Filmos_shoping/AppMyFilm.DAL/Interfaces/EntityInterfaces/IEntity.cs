using AppMyFilm.DAL.Interfaces.EntityInterfaces;

namespace SkillManagement.DataAccess.Interfaces
{
    public interface IEntity<T> : IClearEntity
    {
        T Id { get; set; }
    }
}

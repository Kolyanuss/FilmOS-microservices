using AppMyFilm.DAL.Interfaces.SQLInterfaces.ISQLRepositories;

namespace SkillManagement.DataAccess.Interfaces
{
    public interface ISQLUnitOfWork
    {
        ISQLFilmsRepository FilmsRepo { get; }
        ISQLBasketFilmsRepository BasketFilmsRepo { get; }

        void Complete();
    }
}

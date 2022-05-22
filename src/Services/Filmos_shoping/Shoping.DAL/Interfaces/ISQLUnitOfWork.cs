using Shoping.DAL.Interfaces.SQLInterfaces.ISQLRepositories;

namespace Shoping.DAL.Interfaces
{
    public interface ISQLUnitOfWork
    {
        ISQLFilmsRepository FilmsRepo { get; }
        ISQLBasketFilmsRepository BasketFilmsRepo { get; }

        void Complete();
    }
}

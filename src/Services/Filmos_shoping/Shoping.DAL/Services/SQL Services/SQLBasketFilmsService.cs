using Shoping.DAL.Entities.SQLEntities;
using Shoping.DAL.Interfaces.SQLInterfaces.ISQLServices;
using Shoping.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoping.DAL.Services.SQL_Services
{
    public class SQLBasketFilmsService : ISQLBasketFilmsService
    {
        ISQLUnitOfWork _UnitOfWork;

        public SQLBasketFilmsService(ISQLUnitOfWork sqlSqlUnitOfWork)
        {
            _UnitOfWork = sqlSqlUnitOfWork;
        }

        public Task<long> AddBasketFilm(SQLBasketFilms BasketFilm)
        {
            return _UnitOfWork.BasketFilmsRepo.Add(BasketFilm);
        }

        public async Task DeleteBasketFilm(long idUser)
        {
            await _UnitOfWork.BasketFilmsRepo.Delete(idUser);
        }

        public async Task DeleteBasketFilm(long idFilm, long idUser)
        {
            await _UnitOfWork.BasketFilmsRepo.Delete(new SQLBasketFilms(idFilm, idUser));
        }

        public async Task<IAsyncEnumerable<SQLBasketFilms>> GetAllBasketFilms()
        {
            return await _UnitOfWork.BasketFilmsRepo.GetAll();
        }

        public async Task<IAsyncEnumerable<SQLBasketFilms>> GetBasketByIdFilm(long Id)
        {
            return await _UnitOfWork.BasketFilmsRepo.GetByIdFilms(Id);
        }

        public IAsyncEnumerable<SQLBasketFilms> GetBasketByIdUser(long Id)
        {
            return _UnitOfWork.BasketFilmsRepo.GetByIdUsers(Id);
        }

        /*public IAsyncEnumerable<SQLBasketFilmsStr> GetBasketFilmsJoinUser()
        {
            return _SqlsqlUnitOfWork.SQLBasketFilmsRepository.GetFilmsJoinUser();
        }*/

    }
}

using AppMyFilm.DAL.Entities.SQLEntities;
using AppMyFilm.DAL.Interfaces.SQLInterfaces.ISQLServices;
using SkillManagement.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppMyFilm.DAL.Services.SQL_Services
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

        public void DeleteBasketFilm(SQLBasketFilms BasketFilm)
        {
            _UnitOfWork.BasketFilmsRepo.Delete(BasketFilm);
        }

        public IAsyncEnumerable<SQLBasketFilms> GetAllBasketFilms()
        {
            return _UnitOfWork.BasketFilmsRepo.GetAll();
        }

        public IAsyncEnumerable<SQLBasketFilms> GetBasketFilmByIdFilm(long Id)
        {
            return _UnitOfWork.BasketFilmsRepo.GetByIdFilms(Id);
        }

        public IAsyncEnumerable<SQLBasketFilms> GetBasketFilmByIdUser(long Id)
        {
            return _UnitOfWork.BasketFilmsRepo.GetByIdUsers(Id);
        }

        /*public IAsyncEnumerable<SQLBasketFilmsStr> GetBasketFilmsJoinUser()
        {
            return _SqlsqlUnitOfWork.SQLBasketFilmsRepository.GetFilmsJoinUser();
        }*/

        public void UpdateBasketFilm(SQLBasketFilms BasketFilm)
        {
            _UnitOfWork.BasketFilmsRepo.Update(BasketFilm);
        }
    }
}

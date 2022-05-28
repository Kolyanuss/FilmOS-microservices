using Shoping.DAL.Entities.SQLEntities;
using Shoping.DAL.Interfaces.SQLInterfaces.ISQLServices;
using Shoping.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shoping.DAL.EntitiesDTO;
using AutoMapper;

namespace Shoping.DAL.Services.SQL_Services
{
    public class SQLBasketFilmsService : ISQLBasketFilmsService
    {
        ISQLUnitOfWork _UnitOfWork;
        private IMapper _mapper;

        public SQLBasketFilmsService(ISQLUnitOfWork sqlSqlUnitOfWork, IMapper mapper)
        {
            _UnitOfWork = sqlSqlUnitOfWork;
            _mapper = mapper;
        }

        public Task<(long, long)> AddBasketFilm(SQLBasketFilmsDTO BasketFilm)
        {
            var rez = _mapper.Map<SQLBasketFilms>(BasketFilm);
            return _UnitOfWork.BasketFilmsRepo.Add(rez);
        }

        public async Task DeleteBasketFilm(long idUser)
        {
            await _UnitOfWork.BasketFilmsRepo.Delete(idUser);
        }

        public async Task DeleteBasketFilm(long idFilm, long idUser)
        {
            await _UnitOfWork.BasketFilmsRepo.Delete(idFilm, idUser);
        }

        public async Task<IEnumerable<SQLBasketFilms>> GetAllBasketFilms()
        {
            return await _UnitOfWork.BasketFilmsRepo.GetAll();
        }

        public async Task<IEnumerable<SQLBasketFilms>> GetBasketByIdFilm(long Id)
        {
            return await _UnitOfWork.BasketFilmsRepo.GetByIdFilms(Id);
        }

        public Task<IEnumerable<SQLBasketFilms>> GetBasketByIdUser(long Id)
        {
            return _UnitOfWork.BasketFilmsRepo.GetByIdUsers(Id);
        }

        public Task<IEnumerable<SQLListFilmsStr>> GetBasketFilmsJoinUser()
        {
            return _UnitOfWork.BasketFilmsRepo.GetFilmsJoinUser();
        }
    }
}

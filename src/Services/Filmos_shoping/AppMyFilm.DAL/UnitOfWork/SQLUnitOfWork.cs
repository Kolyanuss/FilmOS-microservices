using AppMyFilm.DAL.Interfaces.SQLInterfaces.ISQLRepositories;
using SkillManagement.DataAccess.Interfaces;
using System;

namespace SkillManagement.DataAccess.sqlunitOfWork
{
    public class SQLsqlunitOfWork : ISQLUnitOfWork
    {
        private readonly ISQLFilmsRepository _sqlFilmsRepository;
        private readonly ISQLBasketFilmsRepository _sqlBasketFilmsRepository;

        public SQLsqlunitOfWork(
            ISQLFilmsRepository sqlFilmsRepository,
            ISQLBasketFilmsRepository sqlBasketFilmsRepository)
        {
            _sqlFilmsRepository = sqlFilmsRepository;
            _sqlBasketFilmsRepository = sqlBasketFilmsRepository;
        }
        
        public ISQLFilmsRepository FilmsRepo
        {
            get
            {
                return _sqlFilmsRepository;
            }
        }

        public ISQLBasketFilmsRepository BasketFilmsRepo
        {
            get
            {
                return _sqlBasketFilmsRepository;
            }
        }

        public void Complete()
        {
            throw new NotImplementedException();
        }
    }
}

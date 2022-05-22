using EFCoreCodeFirstSampleWEBAPI.DAL.Interfaces;
using EFCoreCodeFirstSampleWEBAPI.DAL.Interfaces.ISQLRepositories;
using EFCoreCodeFirstSampleWEBAPI.DAL.Repository.SQLRepositories;

namespace EFCoreCodeFirstSampleWEBAPI.DAL.UnitOfWork
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private MyAppContext _myAppContext;
        private IUserRepository _user;
        private IFilmsRepository _films;
        private IFilmsUsersRepository _filmsUsers;

        public RepositoryWrapper(MyAppContext myAppContext)
        {
            _myAppContext = myAppContext;
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_myAppContext);
                }
                return _user;
            }
        }

        public IFilmsRepository Films
        {
            get
            {
                if (_films == null)
                {
                    _films = new FilmsRepository(_myAppContext);
                }
                return _films;
            }
        }

        public IFilmsUsersRepository FilmsUsers
        {
            get
            {
                if (_filmsUsers == null)
                {
                    _filmsUsers = new FilmsUsersRepository(_myAppContext);
                }
                return _filmsUsers;
            }
        }

        public async void SaveAsync()
        {
            await _myAppContext.SaveChangesAsync();
        }
    }
}

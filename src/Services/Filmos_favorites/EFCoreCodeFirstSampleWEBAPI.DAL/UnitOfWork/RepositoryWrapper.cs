using EFCoreCodeFirstSampleWEBAPI.DAL.Interfaces;
using EFCoreCodeFirstSampleWEBAPI.DAL.Interfaces.ISQLRepositories;
using EFCoreCodeFirstSampleWEBAPI.DAL.Models;
using EFCoreCodeFirstSampleWEBAPI.DAL.Repository;
using EFCoreCodeFirstSampleWEBAPI.DAL.Repository.SQLRepositories;
using Microsoft.Extensions.Logging;
using System;

namespace EFCoreCodeFirstSampleWEBAPI.DAL.UnitOfWork
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private MyAppContext _myAppContext;
        private IUserRepository _user;
        private IFilmsRepository _films;
        private IFilmsUsersRepository _filmsUsers;

        private readonly ILogger<IFilmsRepository> _logger;
        private readonly ILogger<RepositoryBase<Films>> _loggerBase;

        public RepositoryWrapper(MyAppContext myAppContext, ILogger<FilmsRepository> logger, ILogger<RepositoryBase<Films>> baseloger)
        {
            _myAppContext = myAppContext;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _loggerBase = baseloger ?? throw new ArgumentNullException(nameof(baseloger));
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
                    _films = new FilmsRepository(_myAppContext, _logger, _loggerBase);
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

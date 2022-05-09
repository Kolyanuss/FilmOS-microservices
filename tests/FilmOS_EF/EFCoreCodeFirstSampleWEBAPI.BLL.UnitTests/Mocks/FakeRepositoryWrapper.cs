using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCoreCodeFirstSampleWEBAPI.DAL.Interfaces;
using EFCoreCodeFirstSampleWEBAPI.DAL.Interfaces.ISQLRepositories;
using Moq;

namespace EFCoreCodeFirstSampleWEBAPI.BLL.UnitTests.Mocks
{
    class FakeRepositoryWrapper : IRepositoryWrapper
    {
        public Mock<IUserRepository> _user;
        public Mock<IFilmsRepository> _films;
        public Mock<IFilmsUsersRepository> _filmsUsers;

        public IUserRepository User
        {
            get
            {
                return _user.Object;
            }
        }

        public IFilmsRepository Films
        {
            get
            {
                return _films.Object;
            }
        }

        public IFilmsUsersRepository FilmsUsers
        {
            get
            {
                return _filmsUsers.Object;
            }
        }

        public FakeRepositoryWrapper()
        {
            _user = new Mock<IUserRepository>();
            _films = new Mock<IFilmsRepository>();
            _filmsUsers = new Mock<IFilmsUsersRepository>();
        }

        public async void SaveAsync()
        {
        }
    }
}

using EFCoreCodeFirstSampleWEBAPI.BLL.Interfaces;
using EFCoreCodeFirstSampleWEBAPI.BLL.Interfaces.ISQLServices;
using System;

namespace EFCoreCodeFirstSampleWEBAPI.UnitTests.Controller
{
    public class ServiceManagerFake : IServiceManager
    {
        public IFilmsService FilmsService => new FilmsServiceFake();

        public IUsersService UsersService => throw new NotImplementedException();

        public IFilmsUsersService FilmsUsersService => throw new NotImplementedException();
    }
}

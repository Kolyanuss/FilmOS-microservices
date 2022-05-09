using EFCoreCodeFirstSampleWEBAPI.BLL.Interfaces;
using EFCoreCodeFirstSampleWEBAPI.BLL.Interfaces.ISQLServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreCodeFirstSampleWEBAPI.UnitTests
{
    public class ServiceManagerFake : IServiceManager
    {
        public IFilmsService FilmsService => new FilmsServiceFake();

        public IUsersService UsersService => throw new NotImplementedException();

        public IFilmsUsersService FilmsUsersService => throw new NotImplementedException();
    }
}

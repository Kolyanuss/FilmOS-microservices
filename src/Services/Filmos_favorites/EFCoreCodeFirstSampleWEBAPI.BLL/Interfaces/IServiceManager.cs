using EFCoreCodeFirstSampleWEBAPI.BLL.Interfaces.ISQLServices;

namespace EFCoreCodeFirstSampleWEBAPI.BLL.Interfaces
{
    public interface IServiceManager
    {
        IFilmsService FilmsService { get; }
        IUsersService UsersService { get; }
        IFilmsUsersService FilmsUsersService { get; }
    }
}

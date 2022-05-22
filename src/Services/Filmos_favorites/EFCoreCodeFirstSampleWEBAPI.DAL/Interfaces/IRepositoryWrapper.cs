using EFCoreCodeFirstSampleWEBAPI.DAL.Interfaces.ISQLRepositories;

namespace EFCoreCodeFirstSampleWEBAPI.DAL.Interfaces
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IFilmsRepository Films { get; }
        IFilmsUsersRepository FilmsUsers { get; }
        void SaveAsync();
    }
}

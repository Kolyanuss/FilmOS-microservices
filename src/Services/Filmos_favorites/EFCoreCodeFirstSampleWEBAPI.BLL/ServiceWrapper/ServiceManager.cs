using AutoMapper;
using EFCoreCodeFirstSampleWEBAPI.BLL.Interfaces;
using EFCoreCodeFirstSampleWEBAPI.BLL.Interfaces.ISQLServices;
using EFCoreCodeFirstSampleWEBAPI.BLL.Services.SQLServices;
using EFCoreCodeFirstSampleWEBAPI.DAL.Interfaces;
using MassTransit;
using System;

namespace EFCoreCodeFirstSampleWEBAPI.BLL.ServiceWrapper
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IFilmsService> _lazyFilmService;
        private readonly Lazy<IUsersService> _lazyUserService;
        private readonly Lazy<IFilmsUsersService> _lazyFilmUserService;
        public ServiceManager(IRepositoryWrapper repositoryManager, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _lazyFilmService = new Lazy<IFilmsService>(() => new FilmsService(repositoryManager, mapper, publishEndpoint));
            _lazyUserService = new Lazy<IUsersService>(() => new UsersService(repositoryManager, mapper, publishEndpoint));
            _lazyFilmUserService = new Lazy<IFilmsUsersService>(() => new FilmsUsersService(repositoryManager, mapper));
        }

        public IFilmsService FilmsService => _lazyFilmService.Value;
        public IUsersService UsersService => _lazyUserService.Value;
        public IFilmsUsersService FilmsUsersService => _lazyFilmUserService.Value;
    }
}

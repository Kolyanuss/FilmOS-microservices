using AutoMapper;
using EFCoreCodeFirstSampleWEBAPI.BLL.DataTransferObjects;
using EFCoreCodeFirstSampleWEBAPI.BLL.Exceptions;
using EFCoreCodeFirstSampleWEBAPI.BLL.Exceptions.Abstract;
using EFCoreCodeFirstSampleWEBAPI.BLL.Interfaces.ISQLServices;
using EFCoreCodeFirstSampleWEBAPI.DAL.Interfaces;
using EFCoreCodeFirstSampleWEBAPI.DAL.Models;
using EFCoreCodeFirstSampleWEBAPI.DAL.Specifications;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventBus.Messages.Events;
using Microsoft.Extensions.Logging;

namespace EFCoreCodeFirstSampleWEBAPI.BLL.Services.SQLServices
{
    public class FilmsService : IFilmsService
    {
        private IRepositoryWrapper _wraper;
        private IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<FilmsService> _logger;

        public FilmsService(IRepositoryWrapper wraper, IMapper mapper, IPublishEndpoint publishEndpoint, ILogger<FilmsService> logger)
        {
            _wraper = wraper;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IEnumerable<FilmsDTO>> GetAll()
        {
            _logger.LogInformation("In " + this.GetType() + " call GetAllAsync");
            IEnumerable<Films> Filmes = await _wraper.Films.GetAllAsync();
            var map = _mapper.Map<IEnumerable<FilmsDTO>>(Filmes);
            _logger.LogInformation("return maps " + map.GetType() + ": ", map);
            return map;
        }

        public async Task<FilmsDTO> GetById(int id)
        {
            _logger.LogInformation("In " + this.GetType() + " call GetByIdAsync");
            var films = await _wraper.Films.GetByIdAsync(id);

            if (films == null)
            {
                throw new FilmsNotFoundException(id);
            }
            var map = _mapper.Map<FilmsDTO>(films);
            _logger.LogInformation("return maps " + map.GetType() + ": ", map);
            return map;
        }

        public async Task<FilmsDTO> GetByIdSpec(int id)
        {
            var specification = new GetFilmByIdAsync(id);
            _logger.LogInformation("In " + this.GetType() + " call FindWithSpecificationPattern");
            var films = _wraper.Films.FindWithSpecificationPattern(specification);

            if (films == null)
            {
                throw new FilmsNotFoundException(id);
            }
            var map = _mapper.Map<FilmsDTO>(films);
            _logger.LogInformation("return maps " + map.GetType() + ": ", map);
            return map;
        }

        public async Task<FilmsDetailDTO> GetWithDetailsById(int id)
        {
            _logger.LogInformation("In " + this.GetType() + " call GetByIdWithDetailsAsync");
            var films = await _wraper.Films.GetByIdWithDetailsAsync(id);
            if (films == null)
            {
                throw new FilmsNotFoundException(id);
            }
            var map = _mapper.Map<FilmsDetailDTO>(films);
            _logger.LogInformation("return maps " + map.GetType() + ": ", map);
            return map;
        }

        public async Task<FilmsDTO> Post(FilmsForCreationDto filmsDto)
        {
            if (filmsDto == null)
            {
                throw new BadRequestException("Films is null.");
            }
            if (filmsDto.NameFilm == null)
            {
                throw new BadRequestException("Parametr NameFilm in Films is null.");
            }

            var films = _mapper.Map<Films>(filmsDto);
            _logger.LogInformation("In " + this.GetType() + " call Add");
            await _wraper.Films.Add(films);

            // send add event to rabbitmq
            var eventMessage = _mapper.Map<FilmsUpsertDtoEvent>(filmsDto);
            _logger.LogInformation("send " + eventMessage.GetType() + " message to rabbitMQ");
            eventMessage.Id_Film = films.Id;
            eventMessage._is_add = true;
            await _publishEndpoint.Publish<FilmsUpsertDtoEvent>(eventMessage);

            var map = _mapper.Map<FilmsDTO>(films);
            _logger.LogInformation("return maps " + map.GetType() + ": ", map);
            return map;
        }

        public async Task Put(int id, FilmsForCreationDto filmsDto)
        {
            if (filmsDto == null)
            {
                throw new BadRequestException("Films is null.");
            }
            if (filmsDto.NameFilm == null)
            {
                throw new BadRequestException("Parametr NameFilm in Films is null.");
            }
            Films ToUpdate = await _wraper.Films.GetByIdAsync(id);
            if (ToUpdate == null)
            {
                throw new FilmsNotFoundException(id);
            }
            _mapper.Map(filmsDto, ToUpdate);
            _logger.LogInformation("In " + this.GetType() + " call Update");
            _wraper.Films.Update(ToUpdate);

            // send update event to rabbitmq
            var eventMessage = _mapper.Map<FilmsUpsertDtoEvent>(filmsDto);
            _logger.LogInformation("send " + eventMessage.GetType() + " message to rabbitMQ");
            eventMessage.Id_Film = id;
            eventMessage._is_add = false;
            await _publishEndpoint.Publish<FilmsUpsertDtoEvent>(eventMessage);
        }

        public async Task Delete(int id)
        {
            Films films = await _wraper.Films.GetByIdAsync(id);
            if (films == null)
            {
                throw new FilmsNotFoundException(id);
            }
            _logger.LogInformation("In " + this.GetType() + " call Delete");
            _wraper.Films.Delete(films);

            // send delete event to rabbitmq
            var eventMessage = new FilmsDeleteDtoEvent() { Id_Film = id };
            _logger.LogInformation("send " + eventMessage.GetType() + " message to rabbitMQ");
            await _publishEndpoint.Publish<FilmsDeleteDtoEvent>(eventMessage);
        }
    }
}

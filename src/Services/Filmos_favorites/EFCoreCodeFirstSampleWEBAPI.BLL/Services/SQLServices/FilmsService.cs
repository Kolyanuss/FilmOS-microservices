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

namespace EFCoreCodeFirstSampleWEBAPI.BLL.Services.SQLServices
{
    public class FilmsService : IFilmsService
    {
        private IRepositoryWrapper _wraper;
        private IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public FilmsService(IRepositoryWrapper wraper, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _wraper = wraper;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }
        public async Task<IEnumerable<FilmsDTO>> GetAll()
        {
            IEnumerable<Films> Filmes = await _wraper.Films.GetAllAsync();
            return _mapper.Map<IEnumerable<FilmsDTO>>(Filmes);
        }

        public async Task<FilmsDTO> GetById(int id)
        {
            var films = await _wraper.Films.GetByIdAsync(id);

            if (films == null)
            {
                throw new FilmsNotFoundException(id);
            }
            return _mapper.Map<FilmsDTO>(films);
        }

        public async Task<FilmsDTO> GetByIdSpec(int id)
        {
            var specification = new GetFilmByIdAsync(id);
            var films = _wraper.Films.FindWithSpecificationPattern(specification);

            if (films == null)
            {
                throw new FilmsNotFoundException(id);
            }
            return _mapper.Map<FilmsDTO>(films);
        }

        public async Task<FilmsDetailDTO> GetWithDetailsById(int id)
        {
            var films = await _wraper.Films.GetByIdWithDetailsAsync(id);
            if (films == null)
            {
                throw new FilmsNotFoundException(id);
            }
            return _mapper.Map<FilmsDetailDTO>(films);
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
            await _wraper.Films.Add(films);

            // send add event to rabbitmq
            var eventMessage = _mapper.Map<FilmsUpsertDtoEvent>(filmsDto);
            eventMessage.Id_Film = films.Id;
            eventMessage._is_add = true;
            await _publishEndpoint.Publish<FilmsUpsertDtoEvent>(eventMessage);

            return _mapper.Map<FilmsDTO>(films);
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
            _wraper.Films.Update(ToUpdate);

            // send update event to rabbitmq
            var eventMessage = _mapper.Map<FilmsUpsertDtoEvent>(filmsDto);
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
            _wraper.Films.Delete(films);

            // send delete event to rabbitmq
            var eventMessage = new FilmsDeleteDtoEvent() { Id_Film = id };
            await _publishEndpoint.Publish<FilmsDeleteDtoEvent>(eventMessage);
        }
    }
}

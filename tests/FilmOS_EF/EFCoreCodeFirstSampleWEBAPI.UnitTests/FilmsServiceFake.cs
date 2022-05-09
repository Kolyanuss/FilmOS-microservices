using AutoMapper;
using EFCoreCodeFirstSampleWEBAPI.BLL.DataTransferObjects;
using EFCoreCodeFirstSampleWEBAPI.BLL.Exceptions;
using EFCoreCodeFirstSampleWEBAPI.BLL.Exceptions.Abstract;
using EFCoreCodeFirstSampleWEBAPI.BLL.Interfaces.ISQLServices;
using EFCoreCodeFirstSampleWEBAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreCodeFirstSampleWEBAPI.UnitTests
{
    public class FilmsServiceFake : IFilmsService
    {
        private readonly IEnumerable<Films> _filmesList;
        private readonly IMapper _mapper;
        public FilmsServiceFake()
        {
            _filmesList = new List<Films>()
            {
                new Films()
                {
                    Id = 0, NameFilm = "Marvel",
                    ReleaseData=new DateTime(2002,5,21),
                    Country = "USA", FKDescriptionId=1
                },
                new Films()
                {
                    Id = 1, NameFilm = "Djagernaut",
                    ReleaseData=new DateTime(2002,5,22),
                    Country = "Ikraine", FKDescriptionId=2
                }
            };

            var config = new MapperConfiguration(cfg =>
                    cfg.AddProfile<MappingProfile>()
                );
            _mapper = new Mapper(config);
        }


        public async Task<IEnumerable<FilmsDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<FilmsDTO>>(_filmesList);
        }

        public async Task<FilmsDTO> GetById(int id)
        {
            var films = _filmesList.Where(a => a.Id == id).FirstOrDefault();
            if (films == null)
            {
                throw new FilmsNotFoundException(id);
            }
            else
            {
                return _mapper.Map<FilmsDTO>(films);
            }
        }

        public Task<FilmsDTO> GetByIdSpec(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<FilmsDetailDTO> GetWithDetailsById(int id)
        {
            var films = _filmesList.Where(a => a.Id == id).FirstOrDefault();
            if (films == null)
            {
                throw new FilmsNotFoundException(id);
            }
            else
            {
                return _mapper.Map<FilmsDetailDTO>(films);
            }
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
            _filmesList.Append(films);            
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
            Films ToUpdate = _filmesList.Where(a => a.Id == id).FirstOrDefault();
            if (ToUpdate == null)
            {
                throw new FilmsNotFoundException(id);
            }
            _mapper.Map(filmsDto, ToUpdate);
            
            var obj = _filmesList.FirstOrDefault(x => x.Id == id);
            if (obj != null) obj = ToUpdate;
        }

        public async Task Delete(int id)
        {
            Films films = _filmesList.Where(a => a.Id == id).FirstOrDefault();
            if (films == null)
            {
                throw new FilmsNotFoundException(id);
            }
            (_filmesList as List<Films>).RemoveAt(id);
        }
    }
}

using AppMyFilm.DAL.Entities.SQLEntities;
using AppMyFilm.DAL.EntitiesDTO;
using AppMyFilm.DAL.Exceptions;
using AppMyFilm.DAL.Exceptions.Abstract;
using AppMyFilm.DAL.Interfaces.SQLInterfaces.ISQLServices;
using AutoMapper;
using SkillManagement.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace AppMyFilm.DAL.Services.SQL_Services
{
    public class SQLFilmsService : ISQLFilmsService
    {
        private ISQLUnitOfWork _UnitOfWork;
        private IMapper _mapper;

        public SQLFilmsService(ISQLUnitOfWork sqlSqlUnitOfWork, IMapper mapper)
        {
            _UnitOfWork = sqlSqlUnitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SQLFilmsDTO>> GetAllFilms()
        {
            IEnumerable<SQLFilms> Filmes = await _UnitOfWork.FilmsRepo.GetAll();
            return _mapper.Map<IEnumerable<SQLFilmsDTO>>(Filmes);
        }

        public async Task<SQLFilmsDTO> GetFilmById(int Id)
        {
            var film = await _UnitOfWork.FilmsRepo.Get(Id);
            if (film == null)
            {
                throw new FilmsNotFoundException(Id);
            }
            else
            {
                return _mapper.Map<SQLFilmsDTO>(film);
            }
        }

        public async Task<IEnumerable<SQLFilmsDTO>> GetNotPopularFilms()
        {
            IEnumerable<SQLFilms> Filmes = await _UnitOfWork.FilmsRepo.GetNotPopularFilms();
            return _mapper.Map<IEnumerable<SQLFilmsDTO>>(Filmes);
        }

        public async Task<IEnumerable<SQLFilmsDTO>> GetFreeFilms()
        {
            IEnumerable<SQLFilms> Filmes = await _UnitOfWork.FilmsRepo.GetFreeFilms();
            return _mapper.Map<IEnumerable<SQLFilmsDTO>>(Filmes);
        }

        public async Task<int> AddFilm(SQLFilmsDTO filmDto)
        {
            if (filmDto == null)
            {
                throw new BadRequestException("Film is null.");
            }
            var film = _mapper.Map<SQLFilms>(filmDto);
            return await _UnitOfWork.FilmsRepo.Add(film);
        }

        public async Task UpdateFilm(int id, SQLFilmsForAddDTO filmDto)
        {
            if (filmDto == null)
            {
                throw new BadRequestException("Films is null.");
            }
            SQLFilms ToUpdate = await _UnitOfWork.FilmsRepo.Get(id);
            if (ToUpdate == null)
            {
                throw new FilmsNotFoundException(id);
            }
            _mapper.Map(filmDto, ToUpdate);
            await _UnitOfWork.FilmsRepo.Update(ToUpdate);
        }

        public async Task DeleteFilm(int id)
        {
            SQLFilms films = await _UnitOfWork.FilmsRepo.Get(id);
            if (films == null)
            {
                throw new FilmsNotFoundException(id);
            }
            await _UnitOfWork.FilmsRepo.Delete(films);
        }
    }
}

using AutoMapper;
using EFCoreCodeFirstSampleWEBAPI.BLL.DataTransferObjects;
using EFCoreCodeFirstSampleWEBAPI.BLL.Exceptions;
using EFCoreCodeFirstSampleWEBAPI.BLL.Exceptions.Abstract;
using EFCoreCodeFirstSampleWEBAPI.BLL.Interfaces.ISQLServices;
using EFCoreCodeFirstSampleWEBAPI.DAL.Interfaces;
using EFCoreCodeFirstSampleWEBAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCoreCodeFirstSampleWEBAPI.BLL.Services.SQLServices
{
    public class FilmsUsersService : IFilmsUsersService
    {
        private IRepositoryWrapper _wrapper;
        private IMapper _mapper;

        public FilmsUsersService(IRepositoryWrapper wraper, IMapper mapper)
        {
            _wrapper = wraper;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FilmsUsersDTO>> Get()
        {
            IEnumerable<FilmsUsers> filmsUsers = await _wrapper.FilmsUsers.GetAllAsync();
            return _mapper.Map<IEnumerable<FilmsUsersDTO>>(filmsUsers);
        }

        public async Task<FilmsUsersDTO> GetById(int id1, int id2)
        {
            var films = await _wrapper.FilmsUsers.GetByPairIdAsync(id1, id2);
            if (films == null)
            {
                throw new FilmsUsersNotFoundException(id1, id2);
            }
            else
            {
                return _mapper.Map<FilmsUsersDTO>(films);
            }
        }

        public async Task<FilmsUsers_DetailDTO> GetByIdWithDetails(int id1, int id2)
        {
            var entity = await _wrapper.FilmsUsers.GetByPairIdAsync(id1, id2);
            if (entity == null)
            {
                throw new FilmsUsersNotFoundException(id1, id2);
            }
            else
            {
                await _wrapper.Films.GetByCondition(film => film.Id == entity.IdFilms).LoadAsync();
                await _wrapper.User.GetByCondition(user => user.Id == entity.IdUser).LoadAsync();
                return _mapper.Map<FilmsUsers_DetailDTO>(entity);
            }
        }

        public async Task<IEnumerable<FilmsUsersDTO>> GetFilmsByUserId(int id1)
        {
            var entity = await _wrapper.FilmsUsers.GetAllFilmsByUserIdAsync(id1);
            if (entity == null)
            {
                throw new FilmsUsersNotFoundException(id1);
            }
            else
            {
                return _mapper.Map<IEnumerable<FilmsUsersDTO>>(entity);
            }
        }

        public async Task<IEnumerable<FilmsDetailUsersIdDTO>> GetFilmsByUserIdDetails(int id1)
        {
            var entity = await _wrapper.FilmsUsers.GetAllFilmsByUserIdAsync(id1);
            if (entity == null)
            {
                throw new FilmsUsersNotFoundException(id1);
            }
            else
            {
                //explicit loading
                foreach (var it in entity)
                {
                    await _wrapper.Films.GetByCondition(film => film.Id == it.IdFilms).LoadAsync();
                }
                return _mapper.Map<IEnumerable<FilmsDetailUsersIdDTO>>(entity);
            }
        }

        public async Task<IEnumerable<FilmsUsersDTO>> GetUsersByFilmId(int id1)
        {
            var entity = await _wrapper.FilmsUsers.GetAllUsersByFilmIdAsync(id1);
            if (entity == null)
            {
                throw new FilmsUsersNotFoundException(id1);
            }
            else
            {
                return _mapper.Map<IEnumerable<FilmsUsersDTO>>(entity);
            }
        }

        public async Task<IEnumerable<FilmsIdUsersDetailsDTO>> GetUsersByFilmIdDetails(int id1)
        {
            var entity = await _wrapper.FilmsUsers.GetAllUsersByFilmIdAsync(id1);
            if (entity == null)
            {
                throw new FilmsUsersNotFoundException(id1);
            }
            else
            {
                //explicit loading
                foreach (var it in entity)
                {
                    await _wrapper.User.GetByCondition(user => user.Id == it.IdUser).LoadAsync();
                }
                return _mapper.Map<IEnumerable<FilmsIdUsersDetailsDTO>>(entity);
            }
        }

        public async Task<FilmsUsersDTO> Post(FilmsUsersDTO filmsDto)
        {
            if (filmsDto == null)
            {
                throw new BadRequestException("FilmsUsers is null.");
            }
            var clearEntity = _mapper.Map<FilmsUsers>(filmsDto);
            await _wrapper.FilmsUsers.Add(clearEntity);
            return _mapper.Map<FilmsUsersDTO>(clearEntity);
        }

        public async Task Put(int id1, int id2, FilmsUsersDTO clearDto)
        {
            if (clearDto == null)
            {
                throw new BadRequestException("FilmsUsers is null.");
            }
            FilmsUsers ToUpdate = await _wrapper.FilmsUsers.GetByPairIdAsync(id1, id2);
            if (ToUpdate == null)
            {
                throw new FilmsUsersNotFoundException(id1, id2);
            }
            _mapper.Map(clearDto, ToUpdate);
            _wrapper.FilmsUsers.Update(ToUpdate);
        }
        public async Task Delete(int id1, int id2)
        {
            FilmsUsers films = await _wrapper.FilmsUsers.GetByPairIdAsync(id1, id2);
            if (films == null)
            {
                throw new FilmsUsersNotFoundException(id1, id2);
            }
            _wrapper.FilmsUsers.Delete(films);
        }
    }
}

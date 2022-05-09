using System.Collections.Generic;
using System.Threading.Tasks;
using EFCoreCodeFirstSampleWEBAPI.BLL.DataTransferObjects;

namespace EFCoreCodeFirstSampleWEBAPI.BLL.Interfaces.ISQLServices
{
    public interface IFilmsUsersService
    {
        public Task<IEnumerable<FilmsUsersDTO>> Get();
        public Task<FilmsUsersDTO> GetById(int id1, int id2);
        public Task<FilmsUsers_DetailDTO> GetByIdWithDetails(int id1, int id2);
        public Task<IEnumerable<FilmsUsersDTO>> GetFilmsByUserId(int id1);
        public Task<IEnumerable<FilmsDetailUsersIdDTO>> GetFilmsByUserIdDetails(int id1);
        public Task<IEnumerable<FilmsUsersDTO>> GetUsersByFilmId(int id1);
        public Task<IEnumerable<FilmsIdUsersDetailsDTO>> GetUsersByFilmIdDetails(int id1);
        public Task<FilmsUsersDTO> Post(FilmsUsersDTO filmsDto);
        public Task Put(int id1, int id2, FilmsUsersDTO clearDto);
        public Task Delete(int id1, int id2);

    }
}

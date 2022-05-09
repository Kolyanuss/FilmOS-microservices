using System.Threading.Tasks;
using System.Collections.Generic;
using EFCoreCodeFirstSampleWEBAPI.BLL.DataTransferObjects;

namespace EFCoreCodeFirstSampleWEBAPI.BLL.Interfaces.ISQLServices
{
    public interface IFilmsService
    {
        public Task<IEnumerable<FilmsDTO>> GetAll();
        public Task<FilmsDTO> GetById(int id);
        public Task<FilmsDTO> GetByIdSpec(int id);
        public Task<FilmsDetailDTO> GetWithDetailsById(int id);
        public Task<FilmsDTO> Post(FilmsForCreationDto filmsDto);
        public Task Put(int id, FilmsForCreationDto filmsDto);
        public Task Delete(int id);

    }
}

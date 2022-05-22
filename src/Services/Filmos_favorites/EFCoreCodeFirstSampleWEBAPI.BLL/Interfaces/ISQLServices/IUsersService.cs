using System.Threading.Tasks;
using System.Collections.Generic;
using EFCoreCodeFirstSampleWEBAPI.BLL.DataTransferObjects;

namespace EFCoreCodeFirstSampleWEBAPI.BLL.Interfaces.ISQLServices
{
    public interface IUsersService
    {
        public Task<IEnumerable<UserDTO>> Get();
        public Task<UserDTO> GetById(int id);
        public Task<UserDTO> Post(UserForCreationDto userdto);
        public Task Put(int id, UserForCreationDto userdto);
        public Task Delete(int id);

    }
}

using EFCoreCodeFirstSampleWEBAPI.BLL.Exceptions.Abstract;

namespace EFCoreCodeFirstSampleWEBAPI.BLL.Exceptions
{
    public sealed class UsersNotFoundException : NotFoundException
    {
        public UsersNotFoundException(int Id)
            : base($"The user with the identifier {Id} was not found.")
        {
        }
    }
}

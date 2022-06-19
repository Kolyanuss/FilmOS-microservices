using Shoping.DAL.Exceptions.Abstract;

namespace Shoping.DAL.Exceptions
{
    public sealed class UsersNotFoundException : NotFoundException
    {
        public UsersNotFoundException(int Id)
            : base($"The user with the identifier {Id} was not found.")
        {
        }
    }
}

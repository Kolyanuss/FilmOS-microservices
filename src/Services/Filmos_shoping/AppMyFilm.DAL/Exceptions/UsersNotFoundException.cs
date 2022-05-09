using AppMyFilm.DAL.Exceptions.Abstract;

namespace AppMyFilm.DAL.Exceptions
{
    public sealed class UsersNotFoundException : NotFoundException
    {
        public UsersNotFoundException(long Id)
            : base($"The user with the identifier {Id} was not found.")
        {
        }
    }
}

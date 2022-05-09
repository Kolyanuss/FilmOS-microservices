using AppMyFilm.DAL.Exceptions.Abstract;

namespace AppMyFilm.DAL.Exceptions
{
    public sealed class FilmsNotFoundException : NotFoundException
    {
        public FilmsNotFoundException(long Id)
            : base($"The film with the identifier {Id} was not found.")
        {
        }
    }
}

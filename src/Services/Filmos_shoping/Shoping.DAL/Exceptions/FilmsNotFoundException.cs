using Shoping.DAL.Exceptions.Abstract;

namespace Shoping.DAL.Exceptions
{
    public sealed class FilmsNotFoundException : NotFoundException
    {
        public FilmsNotFoundException(int Id)
            : base($"The film with the identifier {Id} was not found.")
        {
        }
    }
}

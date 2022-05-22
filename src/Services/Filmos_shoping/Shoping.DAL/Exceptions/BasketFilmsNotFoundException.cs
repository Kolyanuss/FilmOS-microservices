using Shoping.DAL.Exceptions.Abstract;

namespace Shoping.DAL.Exceptions
{
    public sealed class BasketFilmsNotFoundException : NotFoundException
    {
        public BasketFilmsNotFoundException(long Id)
            : base($"The filmsusers with the identifier {Id} was not found.")
        {
        }
        public BasketFilmsNotFoundException(long Id, long Id2)
            : base($"The filmsusers with the identifier ({Id};{Id2}) was not found.")
        {
        }
    }
}

using Shoping.DAL.Interfaces.EntityInterfaces;

namespace Shoping.DAL.Entities.SQLEntities
{
    public class SQLBasketFilms : IClearEntity
    {
        public int id_film { get; set; }
        public int id_user { get; set; }

    }
}

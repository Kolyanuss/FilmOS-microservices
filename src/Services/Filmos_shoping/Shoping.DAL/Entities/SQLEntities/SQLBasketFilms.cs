using Shoping.DAL.Interfaces.EntityInterfaces;

namespace Shoping.DAL.Entities.SQLEntities
{
    public class SQLBasketFilms : IClearEntity
    {
        public long id_film { get; set; }
        public long id_user { get; set; }

        public SQLBasketFilms(long idFilms, long idUser)
        {
            id_film = idFilms;
            id_user = idUser;
        }
    }
}

using Shoping.DAL.Interfaces.EntityInterfaces;

namespace Shoping.DAL.Entities.SQLEntities
{
    public class SQLListFilmsStr : IClearEntity
    {
        public string NameFilm { get; }
        public string UserName { get; }

        public SQLListFilmsStr(string nameFilm, string userName)
        {
            NameFilm = nameFilm;
            UserName = userName;
        }
    }
}

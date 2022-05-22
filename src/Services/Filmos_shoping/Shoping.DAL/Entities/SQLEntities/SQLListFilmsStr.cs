using Shoping.DAL.Interfaces.EntityInterfaces;

namespace Shoping.DAL.Entities.SQLEntities
{
    public class SQLListFilmsStr : IClearEntity
    {
        public string UserName { get; }
        public string NameFilm { get; }

        public SQLListFilmsStr(string userName, string nameFilm)
        {
            UserName = userName;
            NameFilm = nameFilm;
        }
    }
}

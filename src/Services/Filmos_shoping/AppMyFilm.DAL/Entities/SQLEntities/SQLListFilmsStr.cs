using AppMyFilm.DAL.Interfaces.EntityInterfaces;

namespace AppMyFilm.DAL.Entities.SQLEntities
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

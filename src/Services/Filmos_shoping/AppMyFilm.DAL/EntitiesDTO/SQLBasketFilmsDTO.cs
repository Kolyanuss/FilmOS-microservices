using AppMyFilm.DAL.Interfaces.EntityInterfaces;
using SkillManagement.DataAccess.Interfaces;
using System;

namespace AppMyFilm.DAL.Entities.SQLEntities
{
    public class SQLBasketFilmsDTO : IClearEntity
    {
        public long id_film { get; set; }
        public long id_user { get; set; }
    }
}

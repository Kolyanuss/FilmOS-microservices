using Shoping.DAL.Interfaces;
using System;

namespace Shoping.DAL.Entities.SQLEntities
{
    public class SQLFilms : IEntity<int>
    {
        public int Id { get; set; }
        public string name_film { get; set; }
        public DateTime release_data { get; set; }
        public string country { get; set; }
        public int type_price_id { get; set; }
    }
}

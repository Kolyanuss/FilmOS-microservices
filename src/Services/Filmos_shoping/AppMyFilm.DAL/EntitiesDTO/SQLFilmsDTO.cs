﻿using System;

namespace AppMyFilm.DAL.EntitiesDTO
{
    public class SQLFilmsDTO
    {
        public int Id { get; set; }
        public string name_film { get; set; }
        public DateTime release_data { get; set; }
        public string country { get; set; }
        public int type_price_id { get; set; }
    }
}
using AppMyFilm.DAL.Entities.SQLEntities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppMyFilm.WEBAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SQLFilmsDTO, SQLFilms>();
            CreateMap<SQLFilms, SQLFilmsDTO>();
            CreateMap<SQLBasketFilmsDTO, SQLBasketFilms>();
            
        }
    }
}

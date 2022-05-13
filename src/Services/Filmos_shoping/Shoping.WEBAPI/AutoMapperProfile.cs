using AutoMapper;
using Shoping.DAL.Entities.SQLEntities;
using Shoping.DAL.EntitiesDTO;

namespace Shoping.WEBAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SQLFilmsDTO, SQLFilms>().ReverseMap();
            CreateMap<SQLFilmsForAddDTO, SQLFilms>();
            CreateMap<SQLBasketFilmsDTO, SQLBasketFilms>().ReverseMap();
        }
    }
}

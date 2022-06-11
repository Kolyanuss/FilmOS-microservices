using AutoMapper;
using Shoping.DAL.Entities.SQLEntities;
using Shoping.GRPC.Protos;

namespace Shoping.GRPC //.Mapper
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<SQLBasketFilms, BasketModel>().ReverseMap()
                .ForMember(dest => dest.id_film, opt => opt.MapFrom(src => src.IdObject))
                .ForMember(dest => dest.id_user, opt => opt.MapFrom(src => src.IdUser));
        }
    }
}

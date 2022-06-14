using AutoMapper;
using Shoping.DAL.Entities.SQLEntities;
using Shoping.DAL.EntitiesDTO;
using Shoping.GRPC.Protos;

namespace Shoping.GRPC.Maper
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketModel, SQLBasketFilmsDTO>()
                .ForMember(dest => dest.id_film, opt => opt.MapFrom(src => src.IdObject))
                .ForMember(dest => dest.id_user, opt => opt.MapFrom(src => src.IdUser));
            CreateMap<SQLBasketFilms, BasketModel>()
                .ForMember(dest => dest.IdObject, opt => opt.MapFrom(src => src.id_film))
                .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.id_user))
                .ForMember(dest => dest.TypeObject, opt => opt.MapFrom(_ => "Film"))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(_ => 111)) // stub
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(_ => 1)); // stub

            CreateMap<SQLBasketFilmsDTO, SQLBasketFilms>().ReverseMap();
        }
    }
}

using AutoMapper;
using Basket.API.Entities;
using Shoping.GRPC.Protos;

namespace Basket.API.Mapper
{
    public class BasketProfile : Profile
	{
		public BasketProfile()
		{
            CreateMap<ShoppingCartItem, BasketModel>()
                .ForMember(dest => dest.IdObject, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.TypeObject, opt => opt.MapFrom(src => src.ProductName));
            CreateMap<BasketModel, ShoppingCartItem>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.IdObject))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.TypeObject));
        }
	}
}

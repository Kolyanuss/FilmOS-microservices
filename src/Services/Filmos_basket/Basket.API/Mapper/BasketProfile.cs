using AutoMapper;
using Basket.API.Entities;
using Shoping.GRPC.Protos;

namespace Basket.API.Mapper
{
    public class BasketProfile : Profile
	{
		public BasketProfile()
		{
			//CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap(); // add in future
			CreateMap<ShoppingCartItem, BasketModel>().ReverseMap()
				.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.IdObject))
				.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.TypeObject));
		}
	}
}

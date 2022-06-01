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
			CreateMap<BasketFilmModel, ShoppingCartItem>()
				.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.IdFilm))
				.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => "Films"))
				.ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => 1))
				.ForMember(dest => dest.Price, opt => opt.MapFrom(src => 1));

			/*CreateMap<BasketSubscriptionsModel, ShoppingCartItem>()
				.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id_sub))
				.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => "Subscriptions"))
				.ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => 1))
				.ForMember(dest => dest.Price, opt => opt.MapFrom(src => 1));*/
		}
	}
}

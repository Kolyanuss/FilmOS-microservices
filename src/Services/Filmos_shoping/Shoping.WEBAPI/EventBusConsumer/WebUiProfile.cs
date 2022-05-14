using AutoMapper;
using EventBus.Messages.Events;
using Shoping.DAL.EntitiesDTO;

namespace Shoping.WEBAPI.EventBusConsumer
{
    public class WebUiProfile : Profile
    {
        public WebUiProfile()
        {
            // films
            CreateMap<FilmsUpsertDtoEvent, SQLFilmsForAddDTO>() // insert+update map
                .ForMember(dest => dest.name_film, opt => opt.MapFrom(src => src.NameFilm))
                .ForMember(dest => dest.country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.release_data, opt => opt.MapFrom(src => src.CreationDate));

            // users
            /*CreateMap<UsersDtoEvent, UpsertUsersCommand>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id_User));*/
        }
    }
}

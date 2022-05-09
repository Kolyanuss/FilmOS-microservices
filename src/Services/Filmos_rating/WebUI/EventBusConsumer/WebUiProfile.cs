using AutoMapper;
using EventBus.Messages.Events;
using Filmos_Rating_CleanArchitecture.Application.Film.Commands.UpsertFilms;
using Filmos_Rating_CleanArchitecture.Application.User.Commands.UpsertUsers;

namespace Filmos_Rating_CleanArchitecture.WebUI.EventBusConsumer
{
    public class WebUiProfile : Profile
    {
        public WebUiProfile()
        {
            CreateMap<UpsertFilmCommand, FilmsDtoEvent>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id_Film));
            CreateMap<UpsertUsersCommand, UsersDtoEvent>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id_User));
        }
    }
}

using AutoMapper;
using EventBus.Messages.Events;
using Filmos_Rating_CleanArchitecture.Application.Film.Commands.UpsertFilms;
using Filmos_Rating_CleanArchitecture.Application.Film.Commands.DeleteFilms;
using Filmos_Rating_CleanArchitecture.Application.User.Commands.UpsertUsers;

namespace Filmos_Rating_CleanArchitecture.WebUI.EventBusConsumer
{
    public class WebUiProfile : Profile
    {
        public WebUiProfile()
        {
            // films
            CreateMap<FilmsUpsertDtoEvent, UpdateFilmCommand>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest._id_sql_film, opt => opt.MapFrom(src => src.Id_Film));
            CreateMap<FilmsUpsertDtoEvent, InsertFilmCommand>()
                .ForMember(dest => dest._id_sql_film, opt => opt.MapFrom(src => src.Id_Film));
            CreateMap<FilmsDeleteDtoEvent, DeleteFilmsCommand>()
                .ForMember(dest => dest.Id_sql, opt => opt.MapFrom(src => src.Id_Film));

            // users
            CreateMap<UsersDtoEvent, UpsertUsersCommand>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id_User));
        }
    }
}

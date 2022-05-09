using AutoMapper;
using EFCoreCodeFirstSampleWEBAPI.BLL.DataTransferObjects;
using EFCoreCodeFirstSampleWEBAPI.DAL.Models;
using EventBus.Messages.Events;

namespace EFCoreCodeFirstSampleWEBAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Films, FilmsDTO>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.ReleaseData));
            CreateMap<Films, FilmsDetailDTO>();
            CreateMap<FilmsForCreationDto, Films>()
                .ForMember(dest => dest.ReleaseData, opt => opt.MapFrom(src => src.Data));

            CreateMap<User, UserDTO>();
            CreateMap<UserForCreationDto, User>();

            CreateMap<Description, DescriptionDTO>();

            CreateMap<FilmsUsers, FilmsUsersDTO>();
            CreateMap<FilmsUsers, FilmsDetailUsersIdDTO>();
            CreateMap<FilmsUsers, FilmsIdUsersDetailsDTO>();
            CreateMap<FilmsUsers, FilmsUsers_DetailDTO>();
            CreateMap<FilmsUsersDTO, FilmsUsers>();

            CreateMap<FilmsForCreationDto, FilmsDtoEvent>().ReverseMap();
            CreateMap<UserForCreationDto, UsersDtoEvent>().ReverseMap();
        }
    }
}

using AutoMapper;
using Filmos_Rating_CleanArchitecture.Application.Common.Mappings;
using Filmos_Rating_CleanArchitecture.Domain.Entities;

namespace Filmos_Rating_CleanArchitecture.Application.Film.Queries.GetFilmsList
{
    public class FilmsLookupDto : IMapFrom<Films>
    {
        public string? Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Films, FilmsLookupDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id_film))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name_Film));
        }
    }
}

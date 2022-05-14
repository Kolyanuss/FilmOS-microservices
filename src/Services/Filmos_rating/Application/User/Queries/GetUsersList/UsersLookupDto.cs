using AutoMapper;
using Filmos_Rating_CleanArchitecture.Application.Common.Mappings;
using Filmos_Rating_CleanArchitecture.Domain.Entities;

namespace Filmos_Rating_CleanArchitecture.Application.User.Queries.GetUsersList
{
    public class UsersLookupDto : IMapFrom<Users>
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public bool Is_admin { get; set; }
        public int _id_sql_user { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Users, UsersLookupDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id_user))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.User_name));
        }
    }
}

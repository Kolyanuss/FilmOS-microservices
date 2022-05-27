using AutoMapper;
using Filmos_Rating_CleanArchitecture.Application.Common.Mappings;
using Filmos_Rating_CleanArchitecture.Domain.Entities;

namespace Filmos_Rating_CleanArchitecture.Application.Comment.Queries
{
    public class CommentDto : IMapFrom<Comments>
    {
        public string? Id { get; set; }
        public int _id_sql_film { get; set; }
        public int _id_sql_user { get; set; }
        public string Text { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Comments, CommentDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id_comment))
                .ForMember(d => d._id_sql_film, opt => opt.MapFrom(s => s._id_sql_film))
                .ForMember(d => d._id_sql_user, opt => opt.MapFrom(s => s._id_sql_user))
                .ForMember(d => d.Text, opt => opt.MapFrom(s => s.Text));
        }
    }
}

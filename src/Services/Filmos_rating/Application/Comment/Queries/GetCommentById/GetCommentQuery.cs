using AutoMapper;
using Filmos_Rating_CleanArchitecture.Application.Common;
using Filmos_Rating_CleanArchitecture.Application.Common.Exceptions;
using Filmos_Rating_CleanArchitecture.Application.Film.Queries.GetFilmsList;
using Filmos_Rating_CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Filmos_Rating_CleanArchitecture.Application.Comment.Queries.GetCommentById
{
    public class GetCommentQuery : IRequest<CommentDto>
    {
        public string? Id_comment_to_find { get; set; }

        public class GetCommentQueryHandler : IRequestHandler<GetCommentQuery, CommentDto>
        {
            private readonly IMapper _mapper;
            private readonly IMongoCollection<Comments> _collection;

            public GetCommentQueryHandler(IOptions<FilmosDatabaseSettings> dbSettings, IMapper mapper)
            {
                _mapper = mapper;

                var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

                _collection = mongoDatabase.GetCollection<Comments>("Comments");
            }

            public async Task<CommentDto> Handle(GetCommentQuery request, CancellationToken cancellationToken)
            {
                var entity = await _collection.Find(x => x.Id_comment == request.Id_comment_to_find).FirstOrDefaultAsync();
                if (entity == null)
                {
                    throw new NotFoundException(nameof(Films), request.Id_comment_to_find);
                }
                var Dto = _mapper.Map<CommentDto>(entity);

                return Dto;
            }
        }
    }
}

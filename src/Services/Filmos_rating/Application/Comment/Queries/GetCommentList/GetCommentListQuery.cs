using AutoMapper;
using Filmos_Rating_CleanArchitecture.Application.Common;
using Filmos_Rating_CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Filmos_Rating_CleanArchitecture.Application.Comment.Queries.GetCommentList
{
    public class GetCommentListQuery : IRequest<CommentListVm>
    {
        public class GetCommentListQueryHandler : IRequestHandler<GetCommentListQuery, CommentListVm>
        {
            private readonly IMapper _mapper;
            private readonly IMongoCollection<Comments> _collection;

            public GetCommentListQueryHandler(IOptions<FilmosDatabaseSettings> dbSettings, IMapper mapper)
            {
                _mapper = mapper;

                var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

                _collection = mongoDatabase.GetCollection<Comments>("Comments");
            }

            public async Task<CommentListVm> Handle(GetCommentListQuery request, CancellationToken cancellationToken)
            {
                var List = await _collection.Find(_ => true).ToListAsync();
                var ListDto = _mapper.Map<List<CommentDto>>(List);

                var vm = new CommentListVm
                {
                    CommentsList = ListDto
                };

                return vm;
            }
        }

    }
}

using Filmos_Rating_CleanArchitecture.Application.Common;
using Filmos_Rating_CleanArchitecture.Application.Common.Exceptions;
using Filmos_Rating_CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Filmos_Rating_CleanArchitecture.Application.Comment.Commands.DeleteComment
{
    public class DeleteCommentCommand : IRequest
    {
        public string? Id { get; set; }

        public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
        {
            private readonly Comments _typeTable;
            private readonly IMongoCollection<Comments> _collection;

            public DeleteCommentCommandHandler(IOptions<FilmosDatabaseSettings> dbSettings)
            {
                var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

                _collection = mongoDatabase.GetCollection<Comments>("Comments");
            }

            public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
            {
                var entity = await _collection.Find(x => x.Id_comment == request.Id).FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Comments), request.Id);
                }

                var filter = Builders<Comments>.Filter.Eq(x => x.Id_comment, entity.Id_comment);
                await _collection.DeleteOneAsync(filter);
                return Unit.Value;
            }
        }
    }
}

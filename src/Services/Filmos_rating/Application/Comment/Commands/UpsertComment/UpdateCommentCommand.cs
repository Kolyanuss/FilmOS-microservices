using Filmos_Rating_CleanArchitecture.Application.Common;
using Filmos_Rating_CleanArchitecture.Application.Common.Exceptions;
using Filmos_Rating_CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Filmos_Rating_CleanArchitecture.Application.Comment.Commands.UpsertComment
{
    public class UpdateCommentCommand : IRequest<string?>
    {
        public string? Id { get; set; }
        public int _id_sql_film { get; set; }
        public int _id_sql_user { get; set; }
        public string Text { get; set; }

        public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, string?>
        {
            private readonly IMongoCollection<Comments> _collection;

            public UpdateCommentCommandHandler(IOptions<FilmosDatabaseSettings> dbSettings)
            {
                var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

                _collection = mongoDatabase.GetCollection<Comments>("Comments");
            }

            public async Task<string?> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
            {
                if (request.Text == null)
                {
                    throw new MissedValueException(nameof(Comments), nameof(request.Text));
                }
                if (request._id_sql_film == 0)
                {
                    throw new MissedValueException(nameof(Comments), nameof(request._id_sql_film));
                }
                if (request._id_sql_user == 0)
                {
                    throw new MissedValueException(nameof(Comments), nameof(request._id_sql_user));
                }

                var entity = await _collection.Find(x => x.Id_comment == request.Id).FirstOrDefaultAsync();
                
                if (entity == null)
                {
                    throw new NotFoundException(nameof(Comments), request.Id);
                }

                entity.Text = request.Text;
                entity._id_sql_film = request._id_sql_film;
                entity._id_sql_user = request._id_sql_user;

                await _collection.ReplaceOneAsync(x => x.Id_comment == entity.Id_comment, entity);

                return entity.Id_comment;
            }
        }
    }
}

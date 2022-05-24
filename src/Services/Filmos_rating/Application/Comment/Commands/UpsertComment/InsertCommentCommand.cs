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
    public class InsertCommentCommand : IRequest<string?>
    {
        public int _id_sql_film { get; set; }
        public int _id_sql_user { get; set; }
        public string Text { get; set; }

        public class InsertCommentCommandHandler : IRequestHandler<InsertCommentCommand, string?>
        {
            private readonly IMongoCollection<Comments> _collection;

            public InsertCommentCommandHandler(IOptions<FilmosDatabaseSettings> dbSettings)
            {
                var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

                _collection = mongoDatabase.GetCollection<Comments>("Comments");
            }

            public async Task<string?> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
            {
                if (request.Text == null)
                {
                    throw new MissedValueException(nameof(Comments), nameof(request.Text));
                }

                var entity = new Comments();

                entity.Text = request.Text;
                entity._id_sql_film = request._id_sql_film;
                entity._id_sql_user = request._id_sql_user;

                await _collection.InsertOneAsync(entity);

                return entity.Id_comment;
            }
        }
    }
}

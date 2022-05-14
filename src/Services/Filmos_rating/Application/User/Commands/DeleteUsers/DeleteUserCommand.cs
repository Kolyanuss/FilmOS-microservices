using Filmos_Rating_CleanArchitecture.Application.Common;
using Filmos_Rating_CleanArchitecture.Application.Common.Exceptions;
using Filmos_Rating_CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Filmos_Rating_CleanArchitecture.Application.User.Commands.DeleteUsers
{
    public class DeleteUserCommand : IRequest
    {
        public string? Id { get; set; }
        public int? Id_sql { get; set; }

        public class DeleteUsersCommandHandler : IRequestHandler<DeleteUserCommand>
        {
            private readonly IMongoCollection<Users> _collection;

            public DeleteUsersCommandHandler(IOptions<FilmosDatabaseSettings> dbSettings)
            {
                var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

                _collection = mongoDatabase.GetCollection<Users>("Users");
            }

            public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                Users entity = null;
                if (request.Id_sql.HasValue)
                {
                    entity = await _collection.Find(x => x._id_sql_user == request.Id_sql).FirstOrDefaultAsync();
                }
                else
                {
                    entity = await _collection.Find(x => x.Id_user == request.Id).FirstOrDefaultAsync();
                }

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Users), request.Id);
                }

                var filter = Builders<Users>.Filter.Eq(x => x.Id_user, entity.Id_user);
                await _collection.DeleteOneAsync(filter);
                return Unit.Value;
            }
        }
    }
}

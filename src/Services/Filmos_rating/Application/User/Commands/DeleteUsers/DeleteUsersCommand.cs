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
    public class DeleteUsersCommand : IRequest
    {
        public string? Id { get; set; }

        public class DeleteUsersCommandHandler : IRequestHandler<DeleteUsersCommand>
        {
            private readonly IMongoCollection<Users> _collection;

            public DeleteUsersCommandHandler(IOptions<FilmosDatabaseSettings> dbSettings)
            {
                var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

                _collection = mongoDatabase.GetCollection<Users>("Users");
            }

            public async Task<Unit> Handle(DeleteUsersCommand request, CancellationToken cancellationToken)
            {
                var entity = await _collection.Find(x => x.Id_user == request.Id).FirstOrDefaultAsync();
                //var entity = await _collection.FindAsync(x => x.Id_film == request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Users), request.Id);
                }

                var filter = Builders<Users>.Filter.Eq(x => x.Id_user, request.Id);
                await _collection.DeleteOneAsync(filter);
                return Unit.Value;
            }
        }
    }
}

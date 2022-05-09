using Filmos_Rating_CleanArchitecture.Application.Common;
using Filmos_Rating_CleanArchitecture.Application.Common.Exceptions;
using Filmos_Rating_CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Filmos_Rating_CleanArchitecture.Application.User.Commands.UpsertUsers
{
    public class UpsertUsersCommand : IRequest<string?>
    {
        public string? Id { get; set; }
        public string User_Name { get; set; }
        public bool Is_admin { get; set; }

        public class UpsertUsersCommandHandler : IRequestHandler<UpsertUsersCommand, string?>
        {
            private readonly IMongoCollection<Users> _collection;

            public UpsertUsersCommandHandler(IOptions<FilmosDatabaseSettings> dbSettings)
            {
                var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

                _collection = mongoDatabase.GetCollection<Users>("Users");
            }

            public async Task<string?> Handle(UpsertUsersCommand request, CancellationToken cancellationToken)
            {
                if (request.User_Name == null)
                {
                    throw new MissedValueException(nameof(Users), nameof(request.User_Name));
                }

                Users entity;

                if (request.Id == "" || request.Id == null)
                {
                    entity = new Users();
                    entity.User_name = request.User_Name;
                    entity.Is_admin = request.Is_admin;
                    await _collection.InsertOneAsync(entity);
                }
                else
                {
                    entity = await _collection.Find(x => x.Id_user == request.Id).FirstOrDefaultAsync();
                    if (entity == null)
                    {
                        throw new NotFoundException(nameof(Users), request.Id);
                    }
                    entity.User_name = request.User_Name;
                    await _collection.ReplaceOneAsync(x => x.Id_user == request.Id, entity);
                }

                return entity.Id_user;
            }
        }
    }
}

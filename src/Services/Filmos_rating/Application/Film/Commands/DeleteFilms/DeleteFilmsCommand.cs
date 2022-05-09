using Filmos_Rating_CleanArchitecture.Application.Common;
using Filmos_Rating_CleanArchitecture.Application.Common.Exceptions;
using Filmos_Rating_CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Filmos_Rating_CleanArchitecture.Application.Film.Commands.DeleteFilms
{
    public class DeleteFilmsCommand : IRequest
    {
        public string? Id { get; set; }

        public class DeleteFilmsCommandHandler : IRequestHandler<DeleteFilmsCommand>
        {
            private readonly IMongoCollection<Films> _collection;

            public DeleteFilmsCommandHandler(IOptions<FilmosDatabaseSettings> dbSettings)
            {
                var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

                _collection = mongoDatabase.GetCollection<Films>("Films");
            }

            public async Task<Unit> Handle(DeleteFilmsCommand request, CancellationToken cancellationToken)
            {
                var entity = await _collection.Find(x => x.Id_film == request.Id).FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Films), request.Id);
                }

                var filter = Builders<Films>.Filter.Eq(x => x.Id_film, request.Id);
                await _collection.DeleteOneAsync(filter);
                return Unit.Value;
            }
        }
    }
}

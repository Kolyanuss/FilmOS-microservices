using Filmos_Rating_CleanArchitecture.Application.Common;
using Filmos_Rating_CleanArchitecture.Application.Common.Exceptions;
using Filmos_Rating_CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Filmos_Rating_CleanArchitecture.Application.Film.Commands.UpsertFilms
{
    public class UpsertFilmCommand : IRequest<string?>
    {
        public string? Id { get; set; }
        public string Name_Film { get; set; }

        public class UpsertFilmCommandHandler : IRequestHandler<UpsertFilmCommand, string?>
        {
            private readonly IMongoCollection<Films> _collection;

            public UpsertFilmCommandHandler(IOptions<FilmosDatabaseSettings> dbSettings)
            {
                var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

                _collection = mongoDatabase.GetCollection<Films>("Films");
            }

            public async Task<string?> Handle(UpsertFilmCommand request, CancellationToken cancellationToken)
            {
                if (request.Name_Film == null)
                {
                    throw new MissedValueException(nameof(Films), nameof(request.Name_Film));
                }

                Films entity;

                if (request.Id == "" || request.Id == null)
                {
                    entity = new Films();
                    entity.Name_Film = request.Name_Film;
                    await _collection.InsertOneAsync(entity);
                }
                else
                {
                    entity = await _collection.Find(x => x.Id_film == request.Id).FirstOrDefaultAsync();
                    if (entity == null)
                    {
                        throw new NotFoundException(nameof(Films), request.Id);
                    }
                    entity.Name_Film = request.Name_Film;
                    await _collection.ReplaceOneAsync(x => x.Id_film == request.Id, entity);
                }

                return entity.Id_film;
            }
        }
    }
}

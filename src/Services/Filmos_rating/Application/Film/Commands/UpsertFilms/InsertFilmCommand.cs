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
    public class InsertFilmCommand : IRequest<string?>
    {
        public int? _id_sql_film { get; set; }
        public string Name_Film { get; set; }

        public class InsertFilmCommandHandler : IRequestHandler<InsertFilmCommand, string?>
        {
            private readonly IMongoCollection<Films> _collection;

            public InsertFilmCommandHandler(IOptions<FilmosDatabaseSettings> dbSettings)
            {
                var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

                _collection = mongoDatabase.GetCollection<Films>("Films");
            }

            public async Task<string?> Handle(InsertFilmCommand request, CancellationToken cancellationToken)
            {
                if (request.Name_Film == null)
                {
                    throw new MissedValueException(nameof(Films), nameof(request.Name_Film));
                }

                Films entity = new Films();

                entity.Name_Film = request.Name_Film;
                entity._id_sql_film = request._id_sql_film.Value;
                await _collection.InsertOneAsync(entity);

                return entity.Id_film;
            }
        }
    }
}

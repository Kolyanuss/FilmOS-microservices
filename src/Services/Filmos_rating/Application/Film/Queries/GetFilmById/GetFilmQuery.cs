using AutoMapper;
using Filmos_Rating_CleanArchitecture.Application.Common;
using Filmos_Rating_CleanArchitecture.Application.Common.Exceptions;
using Filmos_Rating_CleanArchitecture.Application.Film.Queries.GetFilmsList;
using Filmos_Rating_CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Filmos_Rating_CleanArchitecture.Application.Film.Queries.GetFilmById
{
    public class GetFilmQuery : IRequest<FilmsLookupDto>
    {
        public string? Id { get; set; }

        public class GetFilmQueryHandler : IRequestHandler<GetFilmQuery, FilmsLookupDto>
        {
            private readonly IMapper _mapper;
            private readonly IMongoCollection<Films> _collection;

            public GetFilmQueryHandler(IOptions<FilmosDatabaseSettings> dbSettings, IMapper mapper)
            {
                _mapper = mapper;

                var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

                _collection = mongoDatabase.GetCollection<Films>("Films");
            }

            public async Task<FilmsLookupDto> Handle(GetFilmQuery request, CancellationToken cancellationToken)
            {
                var entity = await _collection.Find(x => x.Id_film == request.Id).FirstOrDefaultAsync();
                if (entity == null)
                {
                    throw new NotFoundException(nameof(Films), request.Id);
                }
                var Dto = _mapper.Map<FilmsLookupDto>(entity);

                return Dto;
            }
        }
    }
}

using AutoMapper;
using Filmos_Rating_CleanArchitecture.Application.Common;
using Filmos_Rating_CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Filmos_Rating_CleanArchitecture.Application.Film.Queries.GetFilmsList
{
    public class GetFilmsListQueryHandler : IRequestHandler<GetFilmsListQuery, FilmsListVm>
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Films> _collection;

        public GetFilmsListQueryHandler(IOptions<FilmosDatabaseSettings> dbSettings, IMapper mapper)
        {
            _mapper = mapper;

            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<Films>("Films");
        }

        public async Task<FilmsListVm> Handle(GetFilmsListQuery request, CancellationToken cancellationToken)
        {
            var List = await _collection.Find(_ => true).ToListAsync();
            var ListDto = _mapper.Map<List<FilmsLookupDto>>(List);

            var vm = new FilmsListVm
            {
                Films = ListDto
            };

            return vm;
        }
    }
}

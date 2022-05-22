using AutoMapper;
using Filmos_Rating_CleanArchitecture.Application.Common;
using Filmos_Rating_CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Filmos_Rating_CleanArchitecture.Application.User.Queries.GetUsersList
{
    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, UsersListVm>
    {
        private readonly IMapper _mapper;
        IMongoCollection<Users> _collection;

        public GetUsersListQueryHandler(IOptions<FilmosDatabaseSettings> dbSettings, IMapper mapper)
        {
            _mapper = mapper;
            
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<Users>("Users");
        }

        public async Task<UsersListVm> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var ListUser = await _collection.Find(_ => true).ToListAsync();
            var ListUserDto = _mapper.Map<List<UsersLookupDto>>(ListUser);

            var vm = new UsersListVm
            {
                Users = ListUserDto
            };

            return vm;
        }
    }
}

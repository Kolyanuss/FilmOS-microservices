using AutoMapper;
using Filmos_Rating_CleanArchitecture.Application.Common;
using Filmos_Rating_CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Filmos_Rating_CleanArchitecture.Application.User.Queries.GetUsersList
{
    public class GetUsersAdminListQuery : IRequest<UsersListVm>
    {
        public class GetUsersAdminListQueryHandler : IRequestHandler<GetUsersAdminListQuery, UsersListVm>
        {
            private readonly IMapper _mapper;
            IMongoCollection<Users> _collection;

            public GetUsersAdminListQueryHandler(IOptions<FilmosDatabaseSettings> dbSettings, IMapper mapper)
            {
                _mapper = mapper;

                var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

                _collection = mongoDatabase.GetCollection<Users>("Users");
            }

            public async Task<UsersListVm> Handle(GetUsersAdminListQuery request, CancellationToken cancellationToken)
            {
                var builder = Builders<Users>.Filter;
                var filter = builder.Eq("Is_admin", true);

                var ListUser = await _collection.Find(filter).ToListAsync();
                var ListUserDto = _mapper.Map<List<UsersLookupDto>>(ListUser);

                var vm = new UsersListVm
                {
                    Users = ListUserDto
                };

                return vm;
            }
        }
    }
}

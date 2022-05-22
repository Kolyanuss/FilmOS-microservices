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
    public class GetUsersListWithLongNameQuery : IRequest<UsersListVm>
    {
        public class GetUsersListWithLongNameQueryHandler : IRequestHandler<GetUsersListWithLongNameQuery, UsersListVm>
        {
            private readonly IMapper _mapper;
            IMongoCollection<Users> _collection;

            public GetUsersListWithLongNameQueryHandler(IOptions<FilmosDatabaseSettings> dbSettings, IMapper mapper)
            {
                _mapper = mapper;

                var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

                _collection = mongoDatabase.GetCollection<Users>("Users");
            }

            public async Task<UsersListVm> Handle(GetUsersListWithLongNameQuery request, CancellationToken cancellationToken)
            {
                /*var builder = Builders<Users>.Filter;
                var filter = builder.("User_name", 5);*/

                var ListUser = await _collection.Find(x => x.User_name.Length > 5).ToListAsync();
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

﻿using Filmos_Rating_CleanArchitecture.Application.Common;
using Filmos_Rating_CleanArchitecture.Application.Common.Exceptions;
using Filmos_Rating_CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Filmos_Rating_CleanArchitecture.Application.User.Commands.UpsertUsers
{
    public class UpdateUserCommand : IRequest<string?>
    {
        public string? Id { get; set; }
        public int _id_sql_user { get; set; }
        public string User_Name { get; set; }
        public bool Is_admin { get; set; }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, string?>
        {
            private readonly IMongoCollection<Users> _collection;

            public UpdateUserCommandHandler(IOptions<FilmosDatabaseSettings> dbSettings)
            {
                var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

                _collection = mongoDatabase.GetCollection<Users>("Users");
            }

            public async Task<string?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                if (request.User_Name == null)
                {
                    throw new MissedValueException(nameof(Users), nameof(request.User_Name));
                }

                Users entity;

                if (request.Id != null && request.Id.Length != 0)
                {
                    entity = await _collection.Find(x => x.Id_user == request.Id).FirstOrDefaultAsync();
                }
                else
                {
                    entity = await _collection.Find(x => x._id_sql_user == request._id_sql_user).FirstOrDefaultAsync();
                }

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Users), request.Id);
                }
                entity.User_name = request.User_Name;
                entity.Is_admin = request.Is_admin;
                entity._id_sql_user = request._id_sql_user;

                await _collection.ReplaceOneAsync(x => x.Id_user == entity.Id_user, entity);

                return entity.Id_user;
            }
        }
    }
}

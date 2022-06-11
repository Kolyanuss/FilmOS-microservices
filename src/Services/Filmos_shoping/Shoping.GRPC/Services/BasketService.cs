using AutoMapper;
using Grpc.Core;
using Shoping.DAL.Entities.SQLEntities;
using Shoping.DAL.Interfaces.SQLInterfaces.ISQLServices;
using Shoping.GRPC.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoping.GRPC.Services
{
    public class BasketService : BasketProto.BasketProtoBase
    {
        private readonly ISQLBasketFilmsService _SqlBasketServise;
        private readonly IMapper _mapper;

        public BasketService(ISQLBasketFilmsService sqlBasketServise)
        {
            _SqlBasketServise = sqlBasketServise ?? throw new ArgumentNullException(nameof(sqlBasketServise));
            var config = new MapperConfiguration(cfg =>
                    cfg.AddProfile<BasketProfile>()
                );
            _mapper = new Mapper(config);
        }

        public override Task<BasketModel> GetTestFirstBasket(GetTestRequest request, ServerCallContext context)
        {
            return Task.FromResult(new BasketModel() { IdObject = 1, IdUser = 1 });
        }

        public override async Task GetAllBasket(GetAllBasketRequest request, IServerStreamWriter<BasketModel> responseStream, ServerCallContext context)
        {
            var sqlBasketList = await _SqlBasketServise.GetAllBasketFilms();
            if (sqlBasketList == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Basket is empty."));
            }

            var basketModelList = _mapper.Map<IEnumerable<BasketModel>>(sqlBasketList);

            foreach (var basket in sqlBasketList)
            {
                await responseStream.WriteAsync(new BasketModel()
                {
                    IdObject = basket.id_film,
                    IdUser = basket.id_user
                });
            }
        }

        public override async Task GetBasketFilmByUserName(GetBasketByNameUserRequest request, IServerStreamWriter<BasketModel> responseStream, ServerCallContext context)
        {
            var IdUser = (await _SqlBasketServise.GetAllIdByUserName(request.UserName)).FirstOrDefault();
            if (IdUser == 0)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"User with UserName={request.UserName} is not found."));
            }

            var sqlBasketList = await _SqlBasketServise.GetBasketByIdUser(IdUser);
            if (sqlBasketList == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Basket for UserName={request.UserName} is empty."));
            }

            foreach (var basket in sqlBasketList)
            {
                await responseStream.WriteAsync(new BasketModel()
                {
                    IdObject = basket.id_film,
                    IdUser = basket.id_user
                });
            }
        }
    }
}

using AutoMapper;
using Grpc.Core;
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

        public BasketService(ISQLBasketFilmsService sqlBasketServise, IMapper mapper)
        {
            _SqlBasketServise = sqlBasketServise ?? throw new ArgumentNullException(nameof(sqlBasketServise));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

            foreach (var basket in basketModelList)
            {
                await responseStream.WriteAsync(basket);
            }
        }

        public override async Task GetBasketFilmByUserName(GetBasketByNameUserRequest request, IServerStreamWriter<BasketFilmModel> responseStream, ServerCallContext context)
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

            var basketModelList = _mapper.Map<IEnumerable<BasketFilmModel>>(sqlBasketList); //ERROR

            foreach (var basket in basketModelList)
            {
                await responseStream.WriteAsync(basket);
            }
        }
    }
}

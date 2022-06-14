using AutoMapper;
using Grpc.Core;
using Shoping.DAL.EntitiesDTO;
using Shoping.DAL.Interfaces.SQLInterfaces.ISQLServices;
using Shoping.GRPC.Maper;
using Shoping.GRPC.Protos;
using System;
using System.Threading.Tasks;

namespace Shoping.GRPC.Services
{
    public class BasketService : BasketProto.BasketProtoBase
    {
        private readonly ISQLBasketFilmsService _SqlBasketFilmServise; // in future: add basket subscription support 
        private readonly IMapper _mapper;

        public BasketService(ISQLBasketFilmsService sqlBasketServise)
        {
            _SqlBasketFilmServise = sqlBasketServise ?? throw new ArgumentNullException(nameof(sqlBasketServise));
            var config = new MapperConfiguration(cfg =>
                    cfg.AddProfile<BasketProfile>()
                );
            _mapper = new Mapper(config);
        }

        public override async Task<BasketModel> GetTestBasket(GetTestRequest request, ServerCallContext context)
        {
            return new BasketModel() { IdObject = 1, IdUser = 1 };
        }

        public override async Task GetAllBasket(GetAllBasketRequest request, IServerStreamWriter<BasketModel> responseStream, ServerCallContext context)
        {
            var sqlBasketList = await _SqlBasketFilmServise.GetAllBasketFilms();
            if (sqlBasketList == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Basket is empty."));
            }

            foreach (var basket in sqlBasketList)
            {
                await responseStream.WriteAsync(_mapper.Map<BasketModel>(basket));
            }
        }

        public override async Task GetBasketByUserId(GetBasketByUserIdRequest request, IServerStreamWriter<BasketModel> responseStream, ServerCallContext context)
        {
            if (request.UserId == 0)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"User with Id={request.UserId} is not found."));
            }

            var sqlBasketList = await _SqlBasketFilmServise.GetBasketByIdUser(request.UserId);
            if (sqlBasketList == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Basket for UserId={request.UserId} is empty."));
            }

            // in future: add basket subscription support
            // find price
            foreach (var basket in sqlBasketList)
            {
                await responseStream.WriteAsync(_mapper.Map<BasketModel>(basket));
            }
        }

        public override async Task<StatusResponse> CreateBasket(CreateBasketRequest request, ServerCallContext context)
        {
            if (request.Basket.TypeObject == "Film" || request.Basket.TypeObject == "Films")
            {
                try
                {
                    var basket = _mapper.Map<SQLBasketFilmsDTO>(request.Basket);
                    await _SqlBasketFilmServise.AddBasketFilm(basket);
                }
                catch (Exception ex)
                {
                    throw new RpcException(new Status(StatusCode.Internal, ex.Message));
                }
                return new StatusResponse { Success = true };
            }

            return new StatusResponse { Success = false };
        }

        public override async Task<StatusResponse> DeleteBasket(DeleteBasketRequest request, ServerCallContext context)
        {
            if (request.Basket.TypeObject == "Film" || request.Basket.TypeObject == "Films")
            {
                try
                {
                    if (request.Basket.IdObject <= 0)
                    {
                        await _SqlBasketFilmServise.DeleteBasketFilm(request.Basket.IdUser);
                    }
                    else await _SqlBasketFilmServise.DeleteBasketFilm(request.Basket.IdObject, request.Basket.IdUser);
                }
                catch (Exception ex)
                {
                    throw new RpcException(new Status(StatusCode.Internal, ex.Message));
                }
                return new StatusResponse { Success = true };
            }

            return new StatusResponse { Success = false };
        }
    }
}

using AutoMapper;
using Grpc.Core;
using Shoping.DAL.EntitiesDTO;
using Shoping.DAL.Interfaces.SQLInterfaces.ISQLServices;
using Shoping.GRPC.Maper;
using Shoping.GRPC.Protos;
using System;
using System.Linq;
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
                await responseStream.WriteAsync(new BasketModel()
                {
                    IdObject = basket.id_film,
                    TypeObject = "Films",
                    IdUser = basket.id_user,
                    Quantity = 1,
                    Price = 100
                });
            }
        }

        public override async Task GetBasketByUserName(GetBasketByNameUserRequest request, IServerStreamWriter<BasketModel> responseStream, ServerCallContext context)
        {
            // find userId by name
            var IdUser = (await _SqlBasketFilmServise.GetAllIdByUserName(request.UserName)).FirstOrDefault();
            if (IdUser == 0)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"User with UserName={request.UserName} is not found."));
            }

            var sqlBasketList = await _SqlBasketFilmServise.GetBasketByIdUser(IdUser);
            if (sqlBasketList == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Basket for UserName={request.UserName} is empty."));
            }

            // in future: add basket subscription support
            // find price
            foreach (var basket in sqlBasketList)
            {
                await responseStream.WriteAsync(new BasketModel()
                {
                    IdObject = basket.id_film,
                    TypeObject = "Films",
                    IdUser = basket.id_user,
                    Quantity = 1,
                    Price = 100
                });
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
                catch
                {
                    return new StatusResponse { Success = false };
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
                catch
                {
                    return new StatusResponse { Success = false };
                }
                return new StatusResponse { Success = true };
            }

            return new StatusResponse { Success = false };
        }
    }
}

using AutoMapper;
using Basket.API.Entities;
using Shoping.GRPC.Protos;
using System;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public class BasketGrscService
    {
        private readonly BasketProto.BasketProtoClient _BasketClient;
        private readonly IMapper _mapper;

        public BasketGrscService(BasketProto.BasketProtoClient basketClient, IMapper mapper)
        {
            _BasketClient = basketClient ?? throw new ArgumentNullException(nameof(basketClient));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ShoppingCart> GetBasketFromSqlByUserName(string UserName)
        {
            var Request = new GetBasketByNameUserRequest { UserName = UserName };
            //var rezult = _BasketClient.GetBasketByUserName(Request);

            var newCart = new ShoppingCart(UserName);
            using (var clientData = _BasketClient.GetBasketFilmByUserName(Request))
            {
                while (await clientData.ResponseStream.MoveNext(new System.Threading.CancellationToken()))
                {
                    var item = clientData.ResponseStream.Current;

                    newCart.Items.Add(_mapper.Map<ShoppingCartItem>(item));
                }
            }
            return newCart;
        }
    }
}

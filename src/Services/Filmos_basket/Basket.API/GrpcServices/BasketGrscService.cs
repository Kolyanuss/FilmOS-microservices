using AutoMapper;
using Basket.API.Entities;
using Grpc.Net.Client;
using Shoping.GRPC.Protos;
using System;
using System.Net.Http;
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

            /*var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var grpcChannel = GrpcChannel.ForAddress("https://localhost:5001",
                new GrpcChannelOptions { HttpHandler = httpHandler });
            _BasketClient = new BasketProto.BasketProtoClient(grpcChannel);*/

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ShoppingCart> GetBasketTest()
        {
            var newCart = new ShoppingCart("Test");

            var rez = _BasketClient.GetTestBasket(new GetTestRequest());
            newCart.Items.Add(_mapper.Map<ShoppingCartItem>(rez));

            return newCart;
        }

        public async Task<ShoppingCart> GetBasketFromSqlByUserName(string UserName)
        {
            var newCart = new ShoppingCart(UserName);

            var Request = new GetBasketByNameUserRequest { UserName = UserName };
            using (var clientData = _BasketClient.GetBasketByUserName(Request))
            {
                while (await clientData.ResponseStream.MoveNext(new System.Threading.CancellationToken()))
                {
                    var item = clientData.ResponseStream.Current;
                    newCart.Items.Add(_mapper.Map<ShoppingCartItem>(item));
                }
            }
            return newCart;
        }

        public async Task<bool> AddCardInBasket(ShoppingCart card)
        {
            foreach (var item in card.Items)
            {
                if(!await AddItemInBasket(item))
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> AddItemInBasket(ShoppingCartItem item)
        {
            var basketModel = _mapper.Map<BasketModel>(item);
            var Request = new CreateBasketRequest { Basket = basketModel };

            return _BasketClient.CreateBasketAsync(Request).ResponseAsync.Result.Success;
        }

        public async Task<bool> DeleteCardInBasket(ShoppingCart card)
        {
            foreach (var item in card.Items)
            {
                if (!await DeleteItemInBasket(item))
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> DeleteItemInBasket(ShoppingCartItem item)
        {
            var basketModel = _mapper.Map<BasketModel>(item);
            var Request = new DeleteBasketRequest { Basket = basketModel };

            return _BasketClient.DeleteBasketAsync(Request).ResponseAsync.Result.Success;
        }
    }
}

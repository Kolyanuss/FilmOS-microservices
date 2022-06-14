using AutoMapper;
using Basket.API.Entities;
using Grpc.Net.Client;
using Shoping.GRPC.Protos;
using System;
using System.Collections.Generic;
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
            var newCart = new ShoppingCart(0);

            var rez = _BasketClient.GetTestBasket(new GetTestRequest());
            newCart.Items.Add(_mapper.Map<ShoppingCartItem>(rez));

            return newCart;
        }

        public async Task<List<ShoppingCart>> GetAllBasket()
        {
            var CardList = new List<ShoppingCart>();
            using (var clientData = _BasketClient.GetAllBasket(new GetAllBasketRequest { }))
            {
                while (await clientData.ResponseStream.MoveNext(new System.Threading.CancellationToken()))
                {
                    var itemModel = clientData.ResponseStream.Current;
                    var item = _mapper.Map<ShoppingCartItem>(itemModel);

                    var findCard = CardList.Find(x => x.UserId == itemModel.IdUser.ToString());
                    if (findCard != null)
                    {
                        findCard.Items.Add(item);
                    }
                    else
                    {
                        var NewCard = new ShoppingCart(itemModel.IdUser);
                        NewCard.Items.Add(item);
                        CardList.Add(NewCard);
                    }
                }
            }
            return CardList;
        }

        public async Task<ShoppingCart> GetBasketFromSqlById(int UserId)
        {
            var newCart = new ShoppingCart(UserId);

            var Request = new GetBasketByUserIdRequest { UserId = UserId };
            using (var clientData = _BasketClient.GetBasketByUserId(Request))
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
                var basketModel = _mapper.Map<BasketModel>(item);
                basketModel.IdUser = int.Parse(card.UserId);
                var Request = new CreateBasketRequest { Basket = basketModel };

                if (_BasketClient.CreateBasketAsync(Request).ResponseAsync.Result.Success == false)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> DeleteCardInBasket(ShoppingCart card)
        {
            var basketModel = new BasketModel() { IdUser = int.Parse(card.UserId) };
            var Request = new DeleteBasketRequest { Basket = basketModel };

            if ((await _BasketClient.DeleteBasketAsync(Request)).Success == false)
            {
                return false;
            }

            return true;
        }
    }
}

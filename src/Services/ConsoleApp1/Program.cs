using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Shoping.GRPC.Protos;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var request = new GetBasketByNameUserRequest { UserName = "Nikolay" };
            var grpcChannel = GrpcChannel.ForAddress("https://localhost:5001", 
                new GrpcChannelOptions { HttpHandler = httpHandler });
            var client = new BasketProto.BasketProtoClient(grpcChannel);

            var response = client.GetTestFirstBasket(new GetTestRequest());
            Console.WriteLine(response);
            Console.ReadLine();

            using (var clientData = client.GetBasketFilmByUserName(request))
            {
                while (await clientData.ResponseStream.MoveNext(new System.Threading.CancellationToken()))
                {
                    var item = clientData.ResponseStream.Current;
                    Console.WriteLine(item);
                }
            }
            Console.ReadLine();
        }
    }
}

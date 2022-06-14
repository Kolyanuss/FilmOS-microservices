using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Basket.API.GrpcServices;
using System.Collections.Generic;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly BasketGrscService _grpc;

        public BasketController(IBasketRepository repository, BasketGrscService grpc)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _grpc = grpc ?? throw new ArgumentNullException(nameof(grpc));
        }

        [HttpGet("{userId}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(int userId)
        {
            var basket = await _repository.GetBasket(userId.ToString());
            if (basket == null)
            {
                basket = await _grpc.GetBasketFromSqlById(userId);
                if (basket != null)
                {
                    await _repository.UpdateBasket(basket);
                }
            }
            return Ok(basket ?? new ShoppingCart(userId));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> EditBasket([FromBody] ShoppingCart basket)
        {
            var RepoStatus = await _repository.UpdateBasket(basket);
            var GrpcStatus = await _grpc.AddCardInBasket(basket);
            if (GrpcStatus && RepoStatus != null)
            {
                return Ok(RepoStatus);
            }
            else return BadRequest("status grpc - " + GrpcStatus + ", status redis - " + RepoStatus);
        }

        [HttpDelete("{userId}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(int userId)
        {
            var id = (userId.ToString());
            var GrpcStatus = await _grpc.DeleteCardInBasket(await _repository.GetBasket(id)); // FIRST GRPC!!!
            await _repository.DeleteBasket(id); // second repo
            if (GrpcStatus)
            {
                return Ok();
            }
            else return BadRequest("status grpc - " + GrpcStatus);
        }


        [HttpGet("GetAllFromSql")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task UpdateBasket_Get()
        {
            foreach (var item in await _grpc.GetAllBasket())
            {
                await _repository.UpdateBasket(item);
            }
        }
    }
}
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
        private List<ShoppingCart> _listToDelete;
        private List<string> _listIdToAdd;

        public BasketController(IBasketRepository repository, BasketGrscService grpc)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _grpc = grpc ?? throw new ArgumentNullException(nameof(grpc));
            UpdateBasket_Get();
            _listToDelete = new List<ShoppingCart>();
            _listIdToAdd = new List<string>();
        }

        protected async Task UpdateBasket_Get()
        {
            foreach (var item in await _grpc.GetAllBasket())
            {
                await _repository.UpdateBasket(item);
            }
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
            var addGrpc = await _grpc.AddCardInBasket(basket);
            var addRepo = await _repository.UpdateBasket(basket);
            if (addGrpc && addRepo != null)
            {
                return Ok(addRepo);
            }
            else return BadRequest("status grpc - " + addGrpc);
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(int userId)
        {
            var id = (userId.ToString());
            await _grpc.DeleteCardInBasket(await _repository.GetBasket(id));
            await _repository.DeleteBasket(id);
            return Ok();
        }
    }
}
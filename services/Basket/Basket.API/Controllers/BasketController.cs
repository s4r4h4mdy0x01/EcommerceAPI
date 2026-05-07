using Basket.Application.Commands;
using Basket.Application.Queries;
using Basket.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{

    public class BasketController : BaseApiController
    {
        private readonly IMediator _mediator;
        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("[action]/{userName}", Name = "GetBasketByUserName")]
        [ProducesResponseType(typeof(ShoppingCartResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBasketByUserName(string userName)
        {
            var result = await _mediator.Send(new GetBasketByUserNameQuery(userName));
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]", Name = "UpdateBasket")]
        [ProducesResponseType(typeof(ShoppingCartResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCartCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        [Route("[action]/{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _mediator.Send(new DeleteBasketByUserNameCommmand(userName));
            return NoContent();


        }
    }
}
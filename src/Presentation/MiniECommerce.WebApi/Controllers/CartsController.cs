using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Application.Features.Commands.NCart.AddCartItem;
using MiniECommerce.Application.Features.Commands.NCart.DeleteCartItem;
using MiniECommerce.Application.Features.Commands.NCart.UpdateCartItem;
using MiniECommerce.Application.Features.Queries.NCart.GetCartItems;

namespace MiniECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class CartsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCartItems([FromQuery] GetCartItemsQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddCartItem(AddCartItemCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateCartItem(UpdateCartItemCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("[action]/{CartItemId}")]
        public async Task<IActionResult> DeleteCartItem([FromRoute] DeleteCartItemCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}

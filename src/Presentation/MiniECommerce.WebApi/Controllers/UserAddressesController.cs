using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Application.Features.Commands.NAppUserAddress.AddUserAddress;
using MiniECommerce.Application.Features.Commands.NAppUserAddress.DeleteUserAddress;
using MiniECommerce.Application.Features.Commands.NAppUserAddress.UpdateUserAddress;
using MiniECommerce.Application.Features.Queries.NAppUserAddress.GetAddressesByUserId;
using MiniECommerce.Application.Features.Queries.NAppUserAddress.GetAllCities;
using MiniECommerce.Application.Features.Queries.NAppUserAddress.GetAllDistrictsByCityId;
using MiniECommerce.Application.Features.Queries.NAppUserAddress.GetAllNeighborhoodsByDistrictId;

namespace MiniECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserAddressesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAddressesByUserId(GetAddressesByUserIdQueryRequest request)
        {
            var response = _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAddress(AddUserAddressCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAddress(UpdateUserAddressCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAddress(DeleteUserAddressCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        // GetAllCitiesQueryRequest sınıfının herhangi bir property'si olmasa dahi HttpGet method'ın da 
        // parametrelerin nereden okunacağını ([FromQuery] gibi) belirtmem gerekiyor!
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCities([FromQuery] GetAllCitiesQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllDistrictsByCityId([FromQuery] GetAllDistrictsByCityIdQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllNeighborhoodsByDistrictId([FromQuery] GetAllNeighborhoodsByDistrictIdQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}

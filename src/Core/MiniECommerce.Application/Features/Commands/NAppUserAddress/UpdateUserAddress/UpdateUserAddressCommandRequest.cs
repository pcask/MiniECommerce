using MediatR;
using MiniECommerce.Application.DTOs.NAppUserAddress;

namespace MiniECommerce.Application.Features.Commands.NAppUserAddress.UpdateUserAddress
{
    public class UpdateUserAddressCommandRequest : IRequest<UpdateUserAddressCommandResponse>
    {
        public AppUserAddressDto BeUpdated { get; set; }
    }
}

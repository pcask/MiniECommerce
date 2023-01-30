using MediatR;
using MiniECommerce.Application.DTOs.NAppUserAddress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Features.Commands.NAppUserAddress.AddUserAddress
{
    public class AddUserAddressCommandRequest : IRequest<AddUserAddressCommandResponse>
    {
        public AppUserAddressDto BeCreated { get; set; }
    }
}

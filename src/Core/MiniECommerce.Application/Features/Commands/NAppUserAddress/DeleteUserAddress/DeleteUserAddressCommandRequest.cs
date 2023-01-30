using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Features.Commands.NAppUserAddress.DeleteUserAddress
{
    public class DeleteUserAddressCommandRequest : IRequest<DeleteUserAddressCommandResponse>
    {
        public string AddressId { get; set; }
    }
}

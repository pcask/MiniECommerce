using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Features.Commands.NCart.DeleteCartItem
{
    public class DeleteCartItemCommandRequest : IRequest<DeleteCartItemCommandResponse>
    {
        public string CartItemId { get; set; }
    }
}

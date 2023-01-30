using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Features.Commands.NCart.UpdateCartItem
{
    public class UpdateCartItemCommandRequest : IRequest<UpdateCartItemCommandResponse>
    {
        public string CartItemId { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
    }
}

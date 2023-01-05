using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Features.Commands.NCart.AddCartItem
{
    public class AddCartItemCommandRequest : IRequest<AddCartItemCommandResponse>
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

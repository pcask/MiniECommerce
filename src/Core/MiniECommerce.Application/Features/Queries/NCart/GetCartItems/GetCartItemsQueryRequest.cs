using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Features.Queries.NCart.GetCartItems
{
    public class GetCartItemsQueryRequest : IRequest<List<GetCartItemsQueryResponse>>
    {

    }
}

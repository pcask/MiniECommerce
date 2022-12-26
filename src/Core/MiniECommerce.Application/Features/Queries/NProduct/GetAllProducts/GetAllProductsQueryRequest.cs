using MediatR;
using MiniECommerce.Application.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Features.Queries.NProduct.GetAllProducts
{
    public class GetAllProductsQueryRequest : IRequest<GetAllProductsQueryResponse>
    {
        //public Pagination Pagination { get; set; }

        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;

        // With Images
        public int Wi { get; set; }

        // Filter brand
        public string? Fb { get; set; }

        // Order By
        public string? Ob { get; set; }
    }
}

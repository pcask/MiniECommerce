using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Features.Queries.NProduct.GetAllProducts
{
    public class GetAllProductsQueryResponse
    {
        public int TotalProductCount { get; set; }
        public object Products { get; set; }
    }
}

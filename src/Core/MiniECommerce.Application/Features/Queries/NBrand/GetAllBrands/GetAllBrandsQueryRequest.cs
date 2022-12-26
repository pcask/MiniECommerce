using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Features.Queries.NBrand.GetAllBrands
{
    public class GetAllBrandsQueryRequest : IRequest<List<GetAllBrandsQueryResponse>>
    {

    }
}

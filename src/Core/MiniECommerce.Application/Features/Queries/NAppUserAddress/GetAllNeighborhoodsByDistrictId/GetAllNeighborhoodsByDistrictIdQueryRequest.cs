using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Features.Queries.NAppUserAddress.GetAllNeighborhoodsByDistrictId
{
    public class GetAllNeighborhoodsByDistrictIdQueryRequest : IRequest<GetAllNeighborhoodsByDistrictIdQueryResponse>
    {
        public int DistrictId { get; set; }
    }
}

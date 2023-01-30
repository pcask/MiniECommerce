using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Features.Queries.NAppUserAddress.GetAllDistrictsByCityId
{
    public class GetAllDistrictsByCityIdQueryRequest : IRequest<GetAllDistrictsByCityIdQueryResponse>
    {
        public int CityId { get; set; }
    }
}

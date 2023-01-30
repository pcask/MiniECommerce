using MediatR;

namespace MiniECommerce.Application.Features.Queries.NBrand.GetBrandByCode
{
    public class GetBrandByProductIdQueryRequest : IRequest<GetBrandByProductIdQueryResponse>
    {
        public string ProductId { get; set; }
    }
}

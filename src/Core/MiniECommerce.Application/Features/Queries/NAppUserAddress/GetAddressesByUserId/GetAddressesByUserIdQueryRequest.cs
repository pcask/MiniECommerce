using MediatR;

namespace MiniECommerce.Application.Features.Queries.NAppUserAddress.GetAddressesByUserId
{
    public class GetAddressesByUserIdQueryRequest : IRequest<GetAddressesByUserIdQueryResponse>
    {
        public string UserId { get; set; }
    }
}

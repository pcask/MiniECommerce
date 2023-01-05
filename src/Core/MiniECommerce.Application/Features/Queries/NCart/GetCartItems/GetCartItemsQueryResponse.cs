using MiniECommerce.Application.DTOs.NCartItem;

namespace MiniECommerce.Application.Features.Queries.NCart.GetCartItems
{
    public class GetCartItemsQueryResponse
    {
        public string CartItemId { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public int Quantity { get; set; }
    }
}

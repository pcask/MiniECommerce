using MiniECommerce.Application.DTOs.NCartItem;

namespace MiniECommerce.Application.Features.Queries.NCart.GetCartItems
{
    public class GetCartItemsQueryResponse
    {
        public string CartItemId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
        public string? ImagePath { get; set; }
        public string? BrandName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

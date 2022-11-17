namespace MiniECommerce.Application.Features.Commands.NProduct.UpdateProduct
{
    public class UpdateProductCommandResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int AmountOfStock { get; set; }
    }
}

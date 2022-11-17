namespace MiniECommerce.Application.Features.Queries.NProduct.GetProductImages
{
    public class GetProductImagesQueryResponse
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

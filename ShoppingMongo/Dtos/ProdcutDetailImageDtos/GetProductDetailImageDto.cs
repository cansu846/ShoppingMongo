using ShoppingMongo.Entities;

namespace ShoppingMongo.Dtos.ProdcutDetailImageDtos
{
    public class GetProductDetailImageDto
    {
        public string ProductId { get; set; }
        public string ImageUrl1 { get; set; }
        public string ImageUrl2 { get; set; }
        public string ImageUrl3 { get; set; }
    }
}

using ShoppingMongo.Entities;

namespace ShoppingMongo.Dtos.ProdcutDetailImageDtos
{
    public class ResultProductDetailImageDto
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public string ImageUrl1 { get; set; }
        public string ImageUrl2 { get; set; }
        public string ImageUrl3 { get; set; }
    }
}

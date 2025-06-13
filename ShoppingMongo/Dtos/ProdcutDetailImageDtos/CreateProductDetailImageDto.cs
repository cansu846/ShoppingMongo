using ShoppingMongo.Entities;

namespace ShoppingMongo.Dtos.ProdcutDetailImageDtos
{
    public class CreateProductDetailImageDto
    {
        public string ProductId { get; set; }
        public string ImageUrl1 { get; set; }
        public IFormFile ImageUrlFromFile1 { get; set; }
        public string ImageUrl2 { get; set; }
        public IFormFile ImageUrlFromFile2 { get; set; }
        public string ImageUrl3 { get; set; }
        public IFormFile ImageUrlFromFile3 { get; set; }

    }
}

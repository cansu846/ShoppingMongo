namespace ShoppingMongo.Dtos.CategoryDtos
{
    public class GetCategoryByIdDto
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IFormFile CategoryImage { get; set; }
        public string CategoryImagePath { get; set; }
        public string CategoryDescription { get; set; }
    }
}

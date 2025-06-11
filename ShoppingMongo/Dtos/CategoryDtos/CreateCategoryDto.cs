namespace ShoppingMongo.Dtos.CategoryDtos
{
    public class CreateCategoryDto
    {
        public string CategoryName { get; set; }
        public IFormFile CategoryImage { get; set; }
        public string CategoryImagePath { get; set; }
        public string CategoryDescription { get; set; }
    }
}

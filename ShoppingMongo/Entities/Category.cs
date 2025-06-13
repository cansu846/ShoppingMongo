using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShoppingMongo.Entities
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImagePath { get; set; }
        //public IFormFile CategoryImage { get; set; }
        public string CategoryDescription { get; set; }
        public List<Product> Products { get; set; }
        public string CategoryImage { get; set; } // sadece hata almamak için

    }
}

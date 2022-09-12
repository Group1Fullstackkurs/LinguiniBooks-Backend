using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using DBDataAccess.Interfaces;

namespace DBDataAccess.Models
{
    public class BookModel : IBookModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        // Strings 
        public string Id { get; set; } = string.Empty;
        public string first_name { get; set; } = string.Empty;
        public string last_name { get; set; } = string.Empty;
        public string book_title { get; set; } = string.Empty;
        public string book_category { get; set; } = string.Empty;
        public string book_language { get; set; } = string.Empty;
        public string book_salesman { get; set; } = string.Empty;

        // Datetime
        public DateTime year_publication { get; set; }
        
        // Ints
        public int book_price { get; set; }
        public int book_amount { get; set; }

        // Bools
        public bool book_new { get; set; }
    }
}
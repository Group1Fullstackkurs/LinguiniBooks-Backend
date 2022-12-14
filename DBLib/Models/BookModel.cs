using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using DBDataAccess.Interfaces;

namespace DBDataAccess.Models
{
    public class BookModel : IBookModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Title { get; set; } = String.Empty;
        public string Category { get; set; } = String.Empty;
        public string Language { get; set; } = String.Empty;
        public string Seller { get; set; } = String.Empty;
        public int PublicationYear { get; set; }

        // Int
        public string Price { get; set; }
        public int Stock { get; set; }

        // Bool
        public bool New { get; set; }
    }
}
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DBDataAccess.Interfaces
{
    public interface IBookModel
    {
        // Strings 
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public string Seller { get; set; }

        // Datetime
        public int PublicationYear { get; set; }

        // Ints
        public string Price { get; set; }
        public int Stock { get; set; }

        // Bools
        public bool New { get; set; }

    }
}

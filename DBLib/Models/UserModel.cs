using DBDataAccess.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DBDataAccess.Models
{
    public class UserModel : IUser
    {
        // Strings.
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Mail { get; set; } = "";
        public string Hash { get; set; } = "";
        public string Salt { get; set; } = "";

        // Bool.
        public bool IsBlockedSelling { get; set; }
        public bool IsBlockedAccount { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSeller { get; set; }
        public bool IsActivatedAccount { get; set; }
        public bool IsActivatedSelling { get; set; }

        // List of type BookModel.
        public List<BookModel> BoughtBooks { get; set; } = new List<BookModel>();
    }
}

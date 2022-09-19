using DBDataAccess.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDataAccess.Interfaces
{
    public interface IUser
    {
        // Strings
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }

        // Bools
        public bool IsBlockedSelling { get; set; }
        public bool IsBlockedAccount { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSeller { get; set; }
        public bool IsActivatedAccount { get; set; }
        public bool IsActivatedSelling { get; set; }

        // List
        public IEnumerable<BookModel> BoughtBooks { get; set; }

    }
}

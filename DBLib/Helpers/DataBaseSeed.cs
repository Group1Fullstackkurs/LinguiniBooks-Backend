using System;
using DBDataAccess.Interfaces;
using DBDataAccess.DBAccess;
using DBDataAccess.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Serializers;

public class DataBaseSeed
{
    public async static Task DataSeed(string connectionString)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("BookStore");

        string text = await System.IO.File.ReadAllTextAsync(@"BooksSeed.json");

        BsonArray bsonArray;

        using (var jsonReader = new JsonReader(text))
        {
            var serializer = new BsonArraySerializer();
            bsonArray = serializer.Deserialize(BsonDeserializationContext.CreateRoot(jsonReader));
        }

        var collection = database.GetCollection<BsonDocument>("Books");

        foreach (BsonValue bsonValue in bsonArray)
        {
            var b = bsonValue.ToBsonDocument();
            await collection.InsertOneAsync(b);

        }
    }

}

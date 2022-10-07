using System;
using DBDataAccess.Interfaces;
using DBDataAccess.DBAccess;
using DBDataAccess.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

public class DataBaseSeed
{
    public async static Task DataSeed(string connectionString)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("BookStore");

        string text = File.ReadAllText(@"BooksSeed.json");

        var document = BsonSerializer.Deserialize<BsonDocument>(text);
        var collection = database.GetCollection<BsonDocument>("Books");
        await collection.InsertOneAsync(document);

    }      
    
}

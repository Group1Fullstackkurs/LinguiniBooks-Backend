using DBDataAccess.DBAccess;
using DBDataAccess.Models;
using LinguiniBooksAPI.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System.ComponentModel.DataAnnotations.Schema;

DBSeed();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();
app.Run();



async void DBSeed()
{
    var connectionString = ConnStrHelper.ReadConnStr();
    var client = new MongoClient(connectionString);
    var database = client.GetDatabase("BookStore");
    var collection = database.GetCollection<BsonDocument>("Books");
    var result = await collection.FindAsync(_ => true);

    if (result.ToList() == null)
    {
        string text = await System.IO.File.ReadAllTextAsync(@"BooksSeed.json");

        BsonArray bsonArray;

        using (var jsonReader = new JsonReader(text))
        {
            var serializer = new BsonArraySerializer();
            bsonArray = serializer.Deserialize(BsonDeserializationContext.CreateRoot(jsonReader));
        }

        foreach (BsonValue bsonValue in bsonArray)
        {
            var b = bsonValue.ToBsonDocument();
            await collection.InsertOneAsync(b);

        }
    }
}

//var connectionString = ConnStrHelper.ReadConnStr();
//var client = new MongoClient(connectionString);
//var database = client.GetDatabase("BookStore");

//string text = await System.IO.File.ReadAllTextAsync(@"BooksSeed.json");

//BsonArray bsonArray;

//using (var jsonReader = new JsonReader(text))
//{
//    var serializer = new BsonArraySerializer();
//    bsonArray = serializer.Deserialize(BsonDeserializationContext.CreateRoot(jsonReader));
//}

//var collection = database.GetCollection<BsonDocument>("Books");

//foreach (BsonValue bsonValue in bsonArray)
//{
//    var b = bsonValue.ToBsonDocument();
//    await collection.InsertOneAsync(b);

//}




//var connectionString = "mongodb+srv://LinguineBookStore:hejhej123123@bookstore.rn6g6tt.mongodb.net/?retryWrites=true&w=majority";

//var client = new MongoClient(connectionString);
//var database = client.GetDatabase("BookStore");

//string text = await System.IO.File.ReadAllTextAsync(@"BooksSeed.json");

//BsonArray bsonArray;

//using (var jsonReader = new JsonReader(text))
//{
//    var serializer = new BsonArraySerializer();
//    bsonArray = serializer.Deserialize(BsonDeserializationContext.CreateRoot(jsonReader));
//}

//var collection = database.GetCollection<BsonDocument>("Books");

//foreach (BsonValue bsonValue in bsonArray)
//{
//    var b = bsonValue.ToBsonDocument();
//    await collection.InsertOneAsync(b);

//}


//Console.WriteLine(ConnStrHelper.ReadConnStr() + " ===================================");
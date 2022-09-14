using DBDataAccess.Models;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace DBDataAccess.DBAccess
{
    public class BooksCrud
    {
        private readonly string connectionString;
        private const string DBName = "BookStore";
        private const string bookCollection = "Books";

        public BooksCrud(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private IMongoCollection<T> Connect<T>(in string collection)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(DBName);
            return db.GetCollection<T>(collection);
        }

        // Create
        public Task CreateBook(BookModel book)
        {
            var collection = Connect<BookModel>(bookCollection);
            return collection.InsertOneAsync(book);
        }

        // Read
        public async Task<List<BookModel>> GetAllBooks()
        {
            var collection = Connect<BookModel>(bookCollection);
            var results = await collection.FindAsync(_ => true);
            //return results.ToList();
            return results.ToList().OrderBy(x => x.FirstName).ToList();
        }

        // Read one (for use in for example delete and put.
        public async Task<BookModel> GetBook(string id)
        {
            var collection = Connect<BookModel>(bookCollection);
            return (await collection.FindAsync(b => b.Id == id)).FirstOrDefault();
        }

        // Update
        public Task UpdateBook(BookModel book)
        {
            var collection = Connect<BookModel>(bookCollection);
            var filter = Builders<BookModel>.Filter.Eq("Id", book.Id);
            return collection.ReplaceOneAsync(filter, book, new ReplaceOptions { IsUpsert = true });
        }

        // update with string id as param
        public Task UpdateBook(string id)
        {
            var collection = Connect<BookModel>(bookCollection);
            var book = collection.FindAsync(b => b.Id == id);
            // TODO: uppdatera boken för vilken parameter som är vald.
            return book;
        }

        // Delete
        public Task DeleteCBook(BookModel book)
        {
            var collection = Connect<BookModel>(bookCollection);
            return collection.DeleteOneAsync(b => b.Id == book.Id);
        }
    }
}

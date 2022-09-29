using DBDataAccess.Models;
using MongoDB.Driver;

namespace DBDataAccess
{
    public class BookRepository : IBookRepository
    {
        private MongoClient _client;
        string connStr = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\fakedbconnstr.txt");
        string DBName = "FakeDb"; // obs ej linguini db!
        string bookCollection = "Books";
        
        public BookRepository()
        {
            _client = new MongoClient(connStr); // connstr? eller var?
        }

        public BookRepository(MongoClient client)
        {
            _client = client;
        }

        //private IMongoCollection<T> Connect<T>(in string collection)
        //{
        //    var client = new MongoClient(connectionString);
        //    var db = client.GetDatabase(DBName);
        //    return db.GetCollection<T>(collection);
        //}

        public IMongoCollection<BookModel> GetAll()
        {
            var db = _client.GetDatabase(DBName);
            return db.GetCollection<BookModel>(bookCollection);
        }

        public async Task<BookModel> GetById(string id)
        {
            var db = _client.GetDatabase(DBName);
            var collection = db.GetCollection<BookModel>(bookCollection);
            return (await collection.FindAsync(b => b.Id == id)).FirstOrDefault();
        }

        public Task Insert(BookModel book)
        {
            var db = _client.GetDatabase(DBName);
            var collection = db.GetCollection<BookModel>(bookCollection);
            book.Id = String.Empty;
            return collection.InsertOneAsync(book);            
        }

        public Task Update(BookModel book)
        {
            var db = _client.GetDatabase(DBName);
            var collection = db.GetCollection<BookModel>(bookCollection);
            var filter = Builders<BookModel>.Filter.Eq("Id", book.Id);
            return collection.ReplaceOneAsync(filter, book, new ReplaceOptions { IsUpsert = true });
        }

        public Task Delete(BookModel book)
        {
            var db = _client.GetDatabase(DBName);
            var collection = db.GetCollection<BookModel>(bookCollection);
            return collection.DeleteOneAsync(b => b.Id == book.Id);
        }
    }
}

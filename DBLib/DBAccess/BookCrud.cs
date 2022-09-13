using DBDataAccess.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DBDataAccess.DBAccess
{
    public class BookCrud : IBookCrud
    {
        private readonly IMongoCollection<BookModel> _books;

        public BookCrud(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("Default"));
            var db = client.GetDatabase("BookStore");
            _books = db.GetCollection<BookModel>("Books");
        }
        
        public Task CreateBook(BookModel book) 
        {
            return _books.InsertOneAsync(book);
        }
        
        public async Task<List<BookModel>> GetAllBooks()
        {
            var results = await _books.FindAsync(_ => true);
            //return results.ToList();
            return results.ToList().OrderBy(x => x.FirstName).ToList();
        }

        // Read one (for use in for example delete and put.
        public async Task<BookModel> GetBook(string id)
        {
            return (await _books.FindAsync(b => b.Id == id)).FirstOrDefault();
        }

        // Update
        public Task UpdateBook(BookModel book)
        {
            var filter = Builders<BookModel>.Filter.Eq("Id", book.Id);
            return _books.ReplaceOneAsync(filter, book, new ReplaceOptions { IsUpsert = true });
        }

        // update with string id as param
        public Task UpdateBook(string id)
        {
            var book = _books.FindAsync(b => b.Id == id);
            // TODO: uppdatera boken för vilken parameter som är vald.
            return book;
        }

        // Delete
        public Task DeleteCBook(BookModel book)
        {
            return _books.DeleteOneAsync(b => b.Id == book.Id);
        }
    }
}

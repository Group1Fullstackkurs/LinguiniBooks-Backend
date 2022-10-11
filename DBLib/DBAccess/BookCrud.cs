    using DBDataAccess.Interfaces;
using DBDataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;


namespace DBDataAccess.DBAccess
{
    public class BooksCrud : IBookCrud
    {
        // Global fields and constants.
        private readonly string connectionString;
        private const string DBName = "BookStore";
        private const string bookCollection = "Books";

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        public BooksCrud(string connectionString)
        {
            this.connectionString = connectionString;

            DBSeed();
        }

        public async void DBSeed()
        {
            var collection = Connect<BookModel>(bookCollection);
            var result = await collection.FindAsync(_ => true);

            if (true)
            {
                await DataBaseSeed.DataSeed(connectionString);
            }
        }

        /// <summary>
        /// Connects to the Mongo database.
        /// </summary>
        /// <typeparam name="T">Type is generic.</typeparam>
        /// <param name="collection">The collection to connect to.</param>
        /// <returns>The collection</returns>
        private IMongoCollection<T> Connect<T>(in string collection)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(DBName);
            return db.GetCollection<T>(collection);
        }

        /// <summary>
        /// CREATE. Creates a book and inserts it to the database.
        /// </summary>
        /// <param name="book">The book to be inserted</param>
        /// <returns>A collection with the new book</returns>
        public Task CreateBook(BookModel book)
        {
            var collection = Connect<BookModel>(bookCollection);
            book.Id = String.Empty;
            return collection.InsertOneAsync(book);
        }

        /// <summary>
        /// READ. Gets all books from the database.
        /// </summary>
        /// <returns>A list of all books, ordered by FirstName</returns>
        public async Task<List<BookModel>> GetAllBooks()
        {
            var collection = Connect<BookModel>(bookCollection);
            var results = await collection.FindAsync(_ => true);
            //return results.ToList();
            return results.ToList().OrderBy(x => x.FirstName).ToList();
        }

        /// <summary>
        /// READ. Gets one book from the database.
        /// </summary>
        /// <param name="id">Id of the book to be found</param>
        /// <returns>The first book that matches the incoming id.</returns>
        public async Task<BookModel> GetBook(string id)
        {
            var collection = Connect<BookModel>(bookCollection);
            return (await collection.FindAsync(b => b.Id == id)).FirstOrDefault();
        }

        /// <summary>
        /// UPDATE. Updates a book object in the database.
        /// </summary>
        /// <param name="book">The book to be updated</param>
        /// <returns>A collection containing the updated book</returns>
        public Task UpdateBook(BookModel book)
        {
            var collection = Connect<BookModel>(bookCollection);
            var filter = Builders<BookModel>.Filter.Eq("Id", book.Id);
            return collection.ReplaceOneAsync(filter, book, new ReplaceOptions { IsUpsert = true });
        }

        /// <summary>
        /// DELETE. Deletes a book.
        /// </summary>
        /// <param name="book">The book to be deleted from database.</param>
        /// <returns>An updated collection without the deleted book object.</returns>
        public Task DeleteCBook(BookModel book)
        {
            var collection = Connect<BookModel>(bookCollection);
            return collection.DeleteOneAsync(b => b.Id == book.Id);
        }
    }
}

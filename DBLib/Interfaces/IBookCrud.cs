using DBDataAccess.Models;

namespace DBDataAccess.Interfaces
{
    public interface IBookCrud
    {
        public Task CreateBook(BookModel book);
        public Task<List<BookModel>> GetAllBooks();
        public Task<BookModel> GetBook(string id);
        public Task UpdateBook(BookModel book);
        public Task DeleteBook(BookModel book);
        public Task DeleteAllBooks();
    }
}
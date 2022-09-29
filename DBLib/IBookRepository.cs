using DBDataAccess.Models;
using MongoDB.Driver;

namespace DBDataAccess
{
    public interface IBookRepository
    {
        IMongoCollection<BookModel> GetAll();
        Task<BookModel> GetById(string id);
        Task Insert(BookModel book);
        Task Update(BookModel book);
        Task Delete(BookModel book);
    }
}

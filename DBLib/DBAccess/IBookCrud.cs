using DBDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDataAccess.DBAccess
{
    public interface IBookCrud
    {
        public Task CreateBook(BookModel book);
        public Task<List<BookModel>> GetAllBooks();
        public Task<BookModel> GetBook(string id);
        public Task UpdateBook(BookModel book);
        public Task DeleteCBook(BookModel book);
    }
}

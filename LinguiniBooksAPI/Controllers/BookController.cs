using Microsoft.AspNetCore.Mvc;
using DBDataAccess.Interfaces;
using DBDataAccess.DBAccess;
using LinguiniBooksAPI.Helpers;
using DBDataAccess.Models;
using DBDataAccess;

namespace LinguiniBooksAPI.Controllers
{
    /// <summary>
    /// Controller for all book-related http requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        /// <summary>
        /// Field needed for database access and crud operations on database.
        /// </summary>
        readonly BooksCrud bookCrud = new(ConnStrHelper.ReadConnStr());

        /// <summary>
        /// READ. Http request for getting all books from database.
        /// </summary>
        /// <returns>A task of type ActionResult of type BookModel</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<IBookModel>>> Get() => await bookCrud.GetAllBooks();

        /// <summary>
        /// READ. Http request for getting one book from database, by specifying the book's id.
        /// </summary>
        /// <param name="id">The id of the book to be found.</param>
        /// <returns>A task of type ActionResult.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BookModel>> GetById(string id)
        {
            var bookToBeFound = await bookCrud.GetBook(id);
            if (bookToBeFound == null)
            {
                return NotFound();
            }
            return bookToBeFound;
        }

        /// <summary>
        /// CREATE. Http request for adding a book to database.
        /// </summary>
        /// <param name="book">The book to be requested.</param>
        /// <returns>A task of type ActionResult.</returns>
        [HttpPost]
        public async Task<ActionResult<IBookModel>> Post(BookModel book)
        {
            await bookCrud.CreateBook(book);
            return Ok();
        }

        /// <summary>
        /// UPDATE. Http request for updating a book that exists in database.
        /// </summary>
        /// <param name="bookToBeUpdated">The book to be updated.</param>
        /// <returns>A task of type ActionResult.</returns>
        [HttpPut]
        public async Task<IActionResult> Put(BookModel bookToBeUpdated)
        {
            var updatedBook = bookCrud.UpdateBook(bookToBeUpdated);
            await updatedBook;
            return Ok();
        }

        /// <summary>
        /// DELETE. Http request for deleting a book from database.
        /// </summary>
        /// <param name="id">The id of the book to be deleted</param>
        /// <returns>A task of type ActionResult</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            var bookToBeDeleted = await bookCrud.GetBook(id);
            await bookCrud.DeleteCBook(bookToBeDeleted);
            
            return Ok();
        }
    }
}

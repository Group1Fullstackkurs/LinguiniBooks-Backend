using Microsoft.AspNetCore.Mvc;
using DBDataAccess.DBAccess;
using DBDataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LinguiniBooksAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private IBookCrud _bookCrud;

        public BookController(IBookCrud booksCrud)
        {
            _bookCrud = booksCrud;
        }

        // GET: BookController
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BookModel>>> Get() => await _bookCrud.GetAllBooks();



        // GET by id
        [HttpGet("{id}")]
        public async Task<ActionResult<BookModel>> GetById(string id)
        {
            var bookToBeFound = await _bookCrud.GetBook(id);
            if (bookToBeFound == null)
            {
                return NotFound();
            }
            return bookToBeFound;
        }

        // CREATE
        [HttpPost]
        public async Task<ActionResult<BookModel>> Post(BookModel book)
        {
            await _bookCrud.CreateBook(book);
            return Ok();
        }

        // UPDATE
        [HttpPut] // todo: vilken/vilka param?
        public Task<IActionResult> Put(BookModel book)
        {
            var x = _bookCrud.UpdateBook(book);
            return (Task<IActionResult>)x;
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            var bookToBeDeleted = await _bookCrud.GetBook(id);
            await _bookCrud.DeleteCBook(bookToBeDeleted);
            
            return Ok(); // Vilken statuskod är korrekt?
        }
    }
}

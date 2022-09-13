using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DBDataAccess.Interfaces;
using DBDataAccess.DBAccess;
using LinguiniBooksAPI.Helpers;
using DBDataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LinguiniBooksAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        readonly BooksCrud bookCrud = new(ConnStrHelper.ReadConnStr());

        // GET: BookController
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<IBookModel>>> Get() => await bookCrud.GetAllBooks();



        // GET by id
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

        // CREATE
        [HttpPost]
        public async Task<ActionResult<IBookModel>> Post(BookModel book)
        {
            await bookCrud.CreateBook(book);
            return Ok();
        }

        // UPDATE
        [HttpPut] // todo: vilken/vilka param?
        public Task<IActionResult> Put(BookModel book)
        {
            var x = bookCrud.UpdateBook(book);
            return (Task<IActionResult>)x;
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            var bookToBeDeleted = await bookCrud.GetBook(id);
            await bookCrud.DeleteCBook(bookToBeDeleted);
            
            return Ok(); // Fixa rätt statuskod.
        }
    }
}

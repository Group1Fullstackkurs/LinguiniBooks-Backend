using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DBDataAccess.Interfaces;
using DBDataAccess.DBAccess;
using LinguiniBooksAPI.Helpers;
using DBDataAccess.Models;

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


        // CREATE
        [HttpPost]
        public async Task<ActionResult<IBookModel>> Post(BookModel book)
        {
            await bookCrud.CreateBook(book);
            return Ok();
        }
    }
}

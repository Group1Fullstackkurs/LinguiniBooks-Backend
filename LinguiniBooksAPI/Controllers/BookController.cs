using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DBDataAccess.Interfaces;
using DBDataAccess.DBAccess;
using LinguiniBooksAPI.Helpers;

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
    }
}

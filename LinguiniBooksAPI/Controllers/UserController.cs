using DBDataAccess.DBAccess;
using DBDataAccess.Interfaces;
using LinguiniBooksAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinguiniBooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly BooksCrud userCrud = new(ConnStrHelper.ReadConnStr());

        // GET: BookController
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<IUser>>> Get() => null;
    }
}

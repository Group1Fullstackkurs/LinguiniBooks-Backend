using DBDataAccess.DBAccess;
using DBDataAccess.Interfaces;
using DBDataAccess.Models;
using LinguiniBooksAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinguiniBooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly UserCrud userCrud = new(ConnStrHelper.ReadConnStr());

        // GET: BookController
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<IUser>>> Get() => await userCrud.GetAllUsers();

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetById(string id)
        {
            var userToBeFound = await userCrud.GetUser(id);
            if (userToBeFound==null)
            {
                return NotFound();
            }
            return userToBeFound;
        }
    }
}

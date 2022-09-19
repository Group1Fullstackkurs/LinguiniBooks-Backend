using DBDataAccess.DBAccess;
using DBDataAccess.Interfaces;
using DBDataAccess.Models;
using LinguiniBooksAPI.Helpers;
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

        [HttpGet("{name}/{pwd}")]
        public async Task<ActionResult<UserModel>> GetByName(string name, string pwd)
        {
            var userToBeFound = await userCrud.GetUserByName(name, pwd);
            if (userToBeFound == null)
            {
                return NotFound();
            }
            return userToBeFound;
        }

        [HttpPut]
        public async Task<ActionResult<IUser>> Update(UserModel userToBeUpdated)
        {
            var updatedUser = userCrud.UpdateUser(
                userToBeUpdated);
            await updatedUser;
            return Ok();
        }

        // CREATE
        [HttpPost]
        public async Task<ActionResult<IUser>> Post(UserModel user)
        {
            await userCrud.CreateUser(user);
            return Ok();
        }
    }
}

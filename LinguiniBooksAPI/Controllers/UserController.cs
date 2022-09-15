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

        #region Work in progress
        [HttpPut("{id}")]
        public async Task<ActionResult<IUser>> Update(UserModel userToBeUpdated)
        {
            var updatedUser = userCrud.UpdateName(userToBeUpdated);
            //updatedUser = userCrud.UpdateOtherProperty1();
            //updatedUser = userCrud.UpdateOtherProperty2();
            return await updatedUser;
        }
        #endregion

        // CREATE
        [HttpPost]
        public async Task<ActionResult<IUser>> Post(UserModel user)
        {
            await userCrud.CreateUser(user);
            return Ok();
        }
    }
}

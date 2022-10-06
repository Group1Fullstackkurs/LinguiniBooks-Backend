using DBDataAccess.DBAccess;
using DBDataAccess.Interfaces;
using DBDataAccess.Models;
using LinguiniBooksAPI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace LinguiniBooksAPI.Controllers
{
    /// <summary>
    /// Controller for all user-related http requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Field needed for database access and crud operations on database.
        /// </summary>
        readonly UserCrud userCrud = new(ConnStrHelper.ReadConnStr());

        /// <summary>
        /// READ. Http request for getting all users from database.
        /// </summary>
        /// <returns>A task of type ActionResult of type UserModel</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<IUser>>> Get() => await userCrud.GetAllUsers();


        /// <summary>
        /// User login.
        /// </summary>
        /// <param name="name">The username of the user wanting to log in.</param>
        /// <param name="pwd">The password of the user wanting to log in.</param>
        /// <returns>A task of type ActionResult.</returns>
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

        /// <summary>
        /// UPDATE. Http request for updating a user that exists in database.
        /// </summary>
        /// <param name="userToBeUpdated">The user to be updated.</param>
        /// <returns>A task of type ActionResult.</returns>
        [HttpPut("{id}/{pwd}")]
        public async Task<ActionResult<IUser>> Update(UserModel userToBeUpdated, string pwd)
        {
            var updatedUser = userCrud.UpdateUser(
                userToBeUpdated, pwd);
            if(!await updatedUser)
            {
                return BadRequest();
            }
            return Ok();
        }

        /// <summary>
        /// Update User Books
        /// </summary>
        /// <param name="id">The active user's id</param>
        /// <param name="pwd">The current password of the user</param>
        /// <param name="book">The chosen book(s)</param>
        /// <returns></returns>
        [HttpPut("BuyBook/{id}/{pwd}")]
        public async Task<ActionResult> BuyBook(string id, string pwd, BookModel[] book)
        {
            var user = userCrud.BuyBook(id, pwd, book);
            if(!await user)
            {
                return BadRequest();
            }
            return Ok();
        }

        /// <summary>
        /// CREATE. Http request for adding a user to database.
        /// </summary>
        /// <param name="user">The user to be requested.</param>
        /// <returns>A task of type ActionResult.</returns>
        [HttpPost]
        public async Task<ActionResult<IUser>> Post(UserModel user)
        {
            await userCrud.CreateUser(user);
            return Ok();
        }
    }
}
using DotNetProjectLibrary.Models;
using DotNetProjectAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetProjectAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService UserService;

        public UserController(UserService userService)
        {
            UserService = userService;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>A list of User objects</returns>
        [HttpGet]
        public ActionResult<List<User>> GetAll() => UserService.GetAll();

        /// <summary>
        /// Get a user by ID
        /// </summary>
        /// <param name="id">The user's ID</param>
        /// <returns>The user with the provided ID</returns>
        /// <exception cref="NotFound">Thrown when the provided ID doesn't exist</exception>
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            User? user = UserService.Get(id);

            if (user is null) return NotFound();

            return user;
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="computer">The User object</param>
        /// <returns>The created User object</returns>
        [HttpPost]
        public IActionResult Create(User user)
        {
            UserService.Add(user);

            return CreatedAtAction(nameof(Get), new { id = user.id }, user);
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id">The user's ID</param>
        /// <returns>A success status</returns>
        /// <exception cref="NotFound">Thrown when the provided ID doesn't exist</exception>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            User? user = UserService.Delete(id);

            if (user is null) return NotFound();

            return Ok();
        }

        /// <summary>
        /// Update a user's password
        /// </summary>
        /// <param name="id">The user's ID</param>
        /// <param name="newPassword">The new password</param>
        /// <returns>A success status</returns>
        /// <exception cref="NotFound">Thrown when the provided ID doesn't exist</exception>
        [HttpPut("{id}/{newPassword}")]
        public IActionResult Update(int id, string newPassword)
        {
            User? newUser = UserService.UpdatePassword(id, newPassword);

            if (newUser is null) return NotFound();

            return Ok();
        }
    }
}

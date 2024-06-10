using DotNetProjectLibrary.Models;
using DotNetProjectAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetProjectAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user_park")]
    public class UserParkController : ControllerBase
    {
        private readonly UserParkService UserParkService;

        public UserParkController(UserParkService userParkService)
        {
            UserParkService = userParkService;
        }

        /// <summary>
        /// Get all associations of users and parks
        /// </summary>
        /// <returns>A list of UserPark objects</returns>
        [HttpGet]
        public ActionResult<List<UserPark>> GetAll() => UserParkService.GetAll();

        /// <summary>
        /// Get parks for a user
        /// </summary>
        /// <param name="id">The user's ID</param>
        /// <returns>A list of UserPark objects</returns>
        [HttpGet("get_by_user/{id}")]
        public ActionResult<List<UserPark>> GetByUser(int id)
        {
            List<UserPark> userParks = UserParkService.GetByUser(id);

            return userParks;
        }

        /// <summary>
        /// Get users for a park
        /// </summary>
        /// <param name="id">The park's ID</param>
        /// <returns>A list of UserPark objects</returns>
        [HttpGet("get_by_park/{id}")]
        public ActionResult<List<UserPark>> GetByPark(int id)
        {
            List<UserPark> parkUsers = UserParkService.GetByPark(id);

            return parkUsers;
        }

        /// <summary>
        /// Create a associtation of user and park
        /// </summary>
        /// <param name="computer">The UserPark object</param>
        /// <returns>The created UserPark object</returns>
        [HttpPost]
        public IActionResult Create(UserPark userPark)
        {
            UserParkService.Add(userPark);

            return Ok(userPark);
        }

        /// <summary>
        /// Delete a association of user and park
        /// </summary>
        /// <param name="userPark">The UserPark object</param>
        /// <returns>A success status</returns>
        /// <exception cref="NotFound">Thrown when the provided ID doesn't exist</exception>
        [HttpDelete]
        public IActionResult Delete(UserPark userPark)
        {
            UserPark? actualUserPark = UserParkService.Delete(userPark);

            if (actualUserPark is null) return NotFound();

            return Ok();
        }
    }
}

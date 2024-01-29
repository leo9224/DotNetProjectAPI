using DotNetProjectLibrary.Models;
using DotNetProjectAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetProjectAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/user_park")]
    public class UserParkController : ControllerBase
    {
        private readonly UserParkService UserParkService;

        public UserParkController(UserParkService userParkService)
        {
            UserParkService = userParkService;
        }

        [HttpGet]
        public ActionResult<List<UserPark>> GetAll() => UserParkService.GetAll();

        [HttpGet("get_by_user/{id}")]
        public ActionResult<List<UserPark>> GetByUser(int id)
        {
            List<UserPark> userParks = UserParkService.GetByUser(id);

            return userParks;
        }

        [HttpGet("get_by_park/{id}")]
        public ActionResult<List<UserPark>> GetByPark(int id)
        {
            List<UserPark> parkUsers = UserParkService.GetByPark(id);

            return parkUsers;
        }

        [HttpPost]
        public IActionResult Create(UserPark userPark)
        {
            UserParkService.Add(userPark);

            return Ok(userPark);
        }

        [HttpDelete]
        public IActionResult Delete(UserPark userPark)
        {
            UserPark? actualUserPark = UserParkService.Delete(userPark);

            if (actualUserPark is null) return NotFound();

            return Ok();
        }
    }
}

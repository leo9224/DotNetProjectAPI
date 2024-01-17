using DotNetProjectAPI.Models;
using DotNetProjectAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetProjectAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService UserService;

        public UserController(UserService userService)
        {
            UserService = userService;
        }

        [HttpGet]
        public ActionResult<List<User>> GetAll() => UserService.GetAll();

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            User? user = UserService.Get(id);

            if (user is null) return NotFound();

            return user;
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            UserService.Add(user);

            return CreatedAtAction(nameof(Get), new { id = user.id }, user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            User? user = UserService.Delete(id);

            if (user is null) return NotFound();

            return Ok();
        }

        [HttpPut("{id}/{newPassword}")]
        public IActionResult Update(int id, string newPassword)
        {
            User? newUser = UserService.UpdatePassword(id, newPassword);

            if (newUser is null) return NotFound();

            return Ok();
        }
    }
}

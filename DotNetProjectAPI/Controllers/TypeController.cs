using DotNetProjectAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Type = DotNetProjectLibrary.Models.Type;

namespace DotNetProjectAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/type")]
    public class TypeController : ControllerBase
    {
        private readonly TypeService TypeService;

        public TypeController(TypeService typeService)
        {
            TypeService = typeService;
        }

        [HttpGet]
        public ActionResult<List<Type>> GetAll() => TypeService.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Type> Get(int id)
        {
            Type? type = TypeService.Get(id);

            if (type is null) return NotFound();

            return type;
        }

        [HttpPost]
        public IActionResult Create(Type type)
        {
            TypeService.Add(type);

            return CreatedAtAction(nameof(Get), new { id = type.id }, type);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Type? type = TypeService.Delete(id);

            if (type is null) return NotFound();

            return Ok();
        }
    }
}

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

        /// <summary>
        /// Get all types
        /// </summary>
        /// <returns>A list of Type objects</returns>
        [HttpGet]
        public ActionResult<List<Type>> GetAll() => TypeService.GetAll();

        /// <summary>
        /// Get a type by ID
        /// </summary>
        /// <param name="id">The type's ID</param>
        /// <returns>The type with the provided ID</returns>
        /// <exception cref="NotFound">Thrown when the provided ID doesn't exist</exception>
        [HttpGet("{id}")]
        public ActionResult<Type> Get(int id)
        {
            Type? type = TypeService.Get(id);

            if (type is null) return NotFound();

            return type;
        }

        /// <summary>
        /// Create a type
        /// </summary>
        /// <param name="computer">The Type object</param>
        /// <returns>The created Type object</returns>
        [HttpPost]
        public IActionResult Create(Type type)
        {
            TypeService.Add(type);

            return CreatedAtAction(nameof(Get), new { id = type.id }, type);
        }

        /// <summary>
        /// Delete a type
        /// </summary>
        /// <param name="id">The type's ID</param>
        /// <returns>A success status</returns>
        /// <exception cref="NotFound">Thrown when the provided ID doesn't exist</exception>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Type? type = TypeService.Delete(id);

            if (type is null) return NotFound();

            return Ok();
        }
    }
}

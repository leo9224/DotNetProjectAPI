using DotNetProjectLibrary.Models;
using DotNetProjectAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetProjectAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/room")]
    public class RoomController : ControllerBase
    {
        private readonly RoomService RoomService;

        public RoomController(RoomService roomService)
        {
            RoomService = roomService;
        }

        /// <summary>
        /// Get all rooms
        /// </summary>
        /// <returns>A list of Room objects</returns>
        [HttpGet]
        public ActionResult<List<Room>> GetAll() => RoomService.GetAll();

        /// <summary>
        /// Get a room by ID
        /// </summary>
        /// <param name="id">The room's ID</param>
        /// <returns>The room with the provided ID</returns>
        /// <exception cref="NotFound">Thrown when the provided ID doesn't exist</exception>
        [HttpGet("{id}")]
        public ActionResult<Room> Get(int id)
        {
            Room? room = RoomService.Get(id);

            if (room is null) return NotFound();

            return room;
        }

        /// <summary>
        /// Get the rooms in a park
        /// </summary>
        /// <param name="id">The park's ID</param>
        /// <returns>The list of the park's rooms</returns>
        /// <exception cref="NotFound">Thrown when the provided ID doesn't exist</exception>
        [HttpGet("get_by_park/{id}")]
        public ActionResult<List<Room>> GetByPark(int id)
        {
            List<Room> rooms = RoomService.GetByPark(id);

            return rooms;
        }

        /// <summary>
        /// Create a room
        /// </summary>
        /// <param name="computer">The Room object</param>
        /// <returns>The created Room object</returns>
        [HttpPost]
        public IActionResult Create(Room room)
        {
            RoomService.Add(room);

            return CreatedAtAction(nameof(Get), new { id = room.id }, room);
        }

        /// <summary>
        /// Update a room
        /// </summary>
        /// <param name="id">The room's ID</param>
        /// <param name="room">The Room object</param>
        /// <returns>A success status</returns>
        /// <exception cref="NotFound">Thrown when the provided ID doesn't exist</exception>
        [HttpPut("{id}")]
        public IActionResult Update(int id, Room room)
        {
            Room? updatedRoom = RoomService.Update(id, room);

            if (room is null) return NotFound();

            return Ok(updatedRoom);
        }

        /// <summary>
        /// Delete a room
        /// </summary>
        /// <param name="id">The room's ID</param>
        /// <returns>A success status</returns>
        /// <exception cref="NotFound">Thrown when the provided ID doesn't exist</exception>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Room? room = RoomService.Delete(id);

            if (room is null) return NotFound();

            return Ok();
        }
    }
}

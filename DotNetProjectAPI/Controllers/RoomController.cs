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

        [HttpGet]
        public ActionResult<List<Room>> GetAll() => RoomService.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Room> Get(int id)
        {
            Room? room = RoomService.Get(id);

            if (room is null) return NotFound();

            return room;
        }

        [HttpGet("get_by_park/{id}")]
        public ActionResult<List<Room>> GetByPark(int id)
        {
            List<Room> rooms = RoomService.GetByPark(id);

            return rooms;
        }

        [HttpPost]
        public IActionResult Create(Room room)
        {
            RoomService.Add(room);

            return CreatedAtAction(nameof(Get), new { id = room.id }, room);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Room room)
        {
            Room? updatedRoom = RoomService.Update(id, room);

            if (room is null) return NotFound();

            return Ok(updatedRoom);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Room? room = RoomService.Delete(id);

            if (room is null) return NotFound();

            return Ok();
        }
    }
}

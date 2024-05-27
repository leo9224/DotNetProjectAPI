using DotNetProjectLibrary.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DotNetProjectAPI.Services
{
    public class RoomService
    {
        private readonly AppDbContext AppDbContext;
        private readonly ILogger<RoomService> Logger;

        public RoomService(AppDbContext appDbContext, ILogger<RoomService> logger)
        {
            AppDbContext = appDbContext;
            Logger = logger;
        }

        public List<Room> GetAll() => AppDbContext.room.ToList();

        public Room? Get(int id) => AppDbContext.room.ToList().Find(room => room.id == id);

        public List<Room> GetByPark(int id)
        {
            List<Room> rooms=AppDbContext.room.Where(room => room.park_id == id).ToList();
            rooms.Sort((room1,room2)=>string.Compare(room1.name,room2.name));

            return rooms;
        }

        public void Add(Room room)
        {
            room.created_at = DateTime.UtcNow;
            room.updated_at = null;
            room.is_enabled = true;

            EntityEntry<Room> addedRoom = AppDbContext.room.Add(room);
            AppDbContext.SaveChanges();

            Logger.LogInformation($"Room created with id {addedRoom.Entity.id}");
        }

        public Room? Update(int id, Room room)
        {
            Room? currentRoom = Get(id);

            if (id != room.id) return null;

            if (currentRoom is null) return null;

            currentRoom.name = room.name;
            currentRoom.updated_at= DateTime.UtcNow;

            EntityEntry<Room> updatedRoom = AppDbContext.room.Update(currentRoom);
            AppDbContext.SaveChanges();

            Logger.LogInformation($"Room with ID {updatedRoom.Entity.id} updated");

            return room;
        }

        public Room? Delete(int id)
        {
            Room? room = Get(id);

            if (room is null) return null;

            room.is_enabled = false;

            EntityEntry<Room> updatedRoom = AppDbContext.room.Update(room);
            AppDbContext.SaveChanges();

            Logger.LogInformation($"Park with ID {updatedRoom.Entity.id} deleted");

            return room;
        }
    }
}

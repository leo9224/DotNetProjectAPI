using DotNetProjectLibrary.Models;

namespace DotNetProjectAPI.Services
{
    public class RoomService
    {
        private readonly AppDbContext AppDbContext;

        public RoomService(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
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

            AppDbContext.room.Add(room);
            AppDbContext.SaveChanges();
        }

        public Room? Delete(int id)
        {
            Room? room = Get(id);

            if (room is null) return null;

            room.is_enabled = false;

            AppDbContext.room.Update(room);
            AppDbContext.SaveChanges();

            return room;
        }
    }
}

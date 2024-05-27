using Microsoft.EntityFrameworkCore.ChangeTracking;
using Type = DotNetProjectLibrary.Models.Type;

namespace DotNetProjectAPI.Services
{
    public class TypeService
    {
        private readonly AppDbContext AppDbContext;
        private readonly ILogger<TypeService> Logger;

        public TypeService(AppDbContext appDbContext, ILogger<TypeService> logger)
        {
            AppDbContext = appDbContext;
            Logger = logger;
        }

        public List<Type> GetAll() => AppDbContext.type.ToList();

        public Type? Get(int id) => AppDbContext.type.ToList().Find(type => type.id == id);

        public void Add(Type type)
        {
            type.class_name = type.class_name.ToLower();
            type.created_at = DateTime.UtcNow;
            type.updated_at = null;
            type.is_enabled = true;

            EntityEntry<Type> addedType = AppDbContext.type.Add(type);
            AppDbContext.SaveChanges();

            Logger.LogInformation($"Type created with id {addedType.Entity.id}");
        }

        public Type? Delete(int id)
        {
            Type? type = Get(id);

            if (type is null) return null;

            EntityEntry<Type> deletedType = AppDbContext.type.Remove(type);
            AppDbContext.SaveChanges();

            Logger.LogInformation($"Park with ID {deletedType.Entity.id} deleted");

            return type;
        }
    }
}

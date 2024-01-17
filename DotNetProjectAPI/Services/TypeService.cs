using Type = DotNetProjectAPI.Models.Type;

namespace DotNetProjectAPI.Services
{
    public class TypeService
    {
        private readonly AppDbContext AppDbContext;

        public TypeService(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public List<Type> GetAll() => AppDbContext.type.ToList();

        public Type? Get(int id) => AppDbContext.type.ToList().Find(type => type.id == id);

        public void Add(Type type)
        {
            type.class_name = type.class_name.ToLower();
            type.created_at = DateTime.UtcNow;
            type.updated_at = null;
            type.is_enabled = true;

            AppDbContext.type.Add(type);
            AppDbContext.SaveChanges();
        }

        public Type? Delete(int id)
        {
            Type? type = Get(id);

            if (type is null) return null;

            AppDbContext.type.Remove(type);
            AppDbContext.SaveChanges();

            return type;
        }
    }
}

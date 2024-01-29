using DotNetProjectLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Type = DotNetProjectLibrary.Models.Type;

namespace DotNetProjectAPI
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> user { get; set; }
        public DbSet<Type> type { get; set; }
        public DbSet<Park> park { get; set; }
        public DbSet<Room> room { get; set; }
        public DbSet<Computer> computer { get; set; }
        public DbSet<UserPark> user_park { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPark>()
                  .HasKey(user_park => new { user_park.user_id, user_park.park_id });
        }
    }
}

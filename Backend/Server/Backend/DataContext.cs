
using Microsoft.EntityFrameworkCore;

namespace Server.Backend
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost; Database=testGraphQL; User Id=sa;Password=qann@_2019");
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey(sc => new {sc.UserId, sc.RoleId});
        }

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
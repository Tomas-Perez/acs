using acs.Model;
using Microsoft.EntityFrameworkCore;

namespace acs.Db
{
    public class UserContext : DbContext
    {
        public UserContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;database=partytalk;user=user;password=password");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity => { entity.HasKey(e => e.Id); });
        }


        public DbSet<User> Users { get; set; }
    }
}
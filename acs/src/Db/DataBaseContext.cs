using acs.Model;
using Microsoft.EntityFrameworkCore;

namespace acs.Db
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base()
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

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(m => m.Members).WithOne();
                entity.HasOne(o => o.Owner).WithOne().HasForeignKey<User>("OwnerId");
            });
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}
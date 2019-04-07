using acs.Model;
using Microsoft.EntityFrameworkCore;

namespace acs.Db
{
    public class GroupContext : DbContext
    {
        public GroupContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;database=partytalk;user=user;password=password");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.Id);
                //TODO relations to user (owner and members)
            });
        }


        public DbSet<Group> Groups { get; set; }
    }
}
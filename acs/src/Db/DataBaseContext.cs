using System;
using System.Collections.Generic;
using System.IO;
using acs.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

namespace acs.Db
{
    public class DatabaseContext : DbContext
    {   
        public DatabaseContext() : base()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var databaseSettings = new DatabaseSettings();
            var config =
                $"server={databaseSettings.Config["server"]};" +
                $"database={databaseSettings.Config["database"]};" +
                $"user={databaseSettings.Config["user"]};" +
                $"password={databaseSettings.Config["password"]}";
            optionsBuilder.UseMySql(config);
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
    
    public class DatabaseSettings
    {
        public Dictionary<string, string> Config { get; set; }

        public DatabaseSettings()
        {
            Config = new Dictionary<string, string>();
            var server = "localhost";
            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1) server = args[1];
            Config.Add("server", server);
            Config.Add("database", "partytalk");
            Config.Add("user", "user");
            Config.Add("password", "password");
        }
    }
}
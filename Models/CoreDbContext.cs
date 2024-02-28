using System;
using Microsoft.EntityFrameworkCore;

namespace NewApp.Models
{

    public class CoreDbContext : DbContext
    {
        public DbSet<Core> Core { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Core>().ToTable("Core");
            modelBuilder.Entity<Core>().HasKey(qt => qt.Id);

            // Other configurations if needed

            base.OnModelCreating(modelBuilder);
        }
    }
}


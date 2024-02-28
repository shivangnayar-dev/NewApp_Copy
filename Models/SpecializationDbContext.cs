using System;
using Microsoft.EntityFrameworkCore;

namespace NewApp.Models
{
    public class SpecializationDbContext : DbContext
    {
        public DbSet<Core> Core { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specialization>().ToTable("Specialization");
            modelBuilder.Entity<Specialization>().HasKey(qt => qt.CoreStreamID);

            // Other configurations if needed

            base.OnModelCreating(modelBuilder);
        }
    }
}


using System;
using Microsoft.EntityFrameworkCore;

namespace NewApp.Models
{
    public class ReportSubAttributeDbContext : DbContext
    {
        public DbSet<ReportSubAttribute> ReportSubAttributes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReportSubAttribute>().ToTable("reportsubattribute");

            base.OnModelCreating(modelBuilder);
        }
    }
}


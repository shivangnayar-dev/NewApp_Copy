using System;
namespace NewApp.Models;
using Microsoft.EntityFrameworkCore;

public class TestCodeDbContext : DbContext
{
    public DbSet<TestCode> TestCodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
     
        modelBuilder.Entity<TestCode>().ToTable("testcode");
        
        

        base.OnModelCreating(modelBuilder);
    }
}

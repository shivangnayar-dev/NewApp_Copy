using Microsoft.EntityFrameworkCore;
using NewApp.Models; // Make sure to adjust the namespace accordingly

public class QualificationTypeDbContext : DbContext
{
    public DbSet<QualificationTypes> QualificationTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QualificationTypes>().ToTable("QualificationTypes");
        modelBuilder.Entity<QualificationTypes>().HasKey(qt => qt.Id);

        // Other configurations if needed

        base.OnModelCreating(modelBuilder);
    }
}


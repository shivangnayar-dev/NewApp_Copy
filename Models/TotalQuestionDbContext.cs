using System;
using Microsoft.EntityFrameworkCore;

namespace NewApp.Models
{
    public class TotalQuestionDbContext : DbContext
    {
        public DbSet<TotalQuestion> TotalQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TotalQuestion>().ToTable("totalquestions");

            base.OnModelCreating(modelBuilder);
        }
    }
}


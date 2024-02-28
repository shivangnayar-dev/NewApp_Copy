using System;
using Microsoft.EntityFrameworkCore;

namespace NewApp.Models
{
    public class QuestionAnsMapDbContext : DbContext
    {
        public DbSet<QuestionAnsMap> QuestionAnsMaps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<QuestionAnsMap>().ToTable("QuestionAnsMap");



            base.OnModelCreating(modelBuilder);
        }
    }
    }


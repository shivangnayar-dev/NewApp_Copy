using Microsoft.EntityFrameworkCore;

namespace NewApp.Models
{
    public class CandidateDbContext : DbContext
    {
        public CandidateDbContext(DbContextOptions<CandidateDbContext> options) : base(options) { }

        public DbSet<CandidateDetails> Candidates { get; set; }
        public DbSet<TestCode> TestCodes { get; set; }
        public DbSet<TotalQuestion> TotalQuestions { get; set; } // Add this line
        public DbSet<ReportSubAttribute> ReportSubAttributes { get; set; }
        public DbSet<QuestionAnsMap> QuestionAnsMaps { get; set; } // Add this line
        public DbSet<QualificationTypes> QualificationTypes { get; set; }
        public DbSet<Core> Core { get; set; }
        public DbSet<Specialization> Specialization { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Specify the primary key for CandidateDetails
            modelBuilder.Entity<CandidateDetails>().ToTable("CandidateDetails");
            modelBuilder.Entity<CandidateDetails>().HasKey(c => c.candidate_id);

            modelBuilder.Entity<QualificationTypes>().ToTable("QualificationTypes");
            modelBuilder.Entity<QualificationTypes>().HasKey(qt => qt.Id);
            modelBuilder.Entity<Core>().ToTable("Core");
            modelBuilder.Entity<Core>().HasKey(qt => qt.Id);
            modelBuilder.Entity<Specialization>().ToTable("Specialization");
            modelBuilder.Entity<Specialization>().HasKey(qt => qt.CoreStreamID);

            // Specify the primary key for TestCode
            modelBuilder.Entity<TestCode>().ToTable("testcode");
            modelBuilder.Entity<TestCode>().HasKey(tc => tc.Code);

            // Specify the primary key for TotalQuestion
            modelBuilder.Entity<TotalQuestion>().ToTable("totalquestions");
            modelBuilder.Entity<TotalQuestion>().HasKey(tq => tq.QuestionId);

            modelBuilder.Entity<QuestionAnsMap>().ToTable("QuestionAnsMap");
            modelBuilder.Entity<QuestionAnsMap>().HasKey(qam => qam.AnswerId);
         
            // Specify the primary key for ReportSubAttribute
            modelBuilder.Entity<ReportSubAttribute>().ToTable("reportsubattribute");
            modelBuilder.Entity<ReportSubAttribute>().HasKey(rsa => rsa.ReportId);

            // Other configurations if needed

            base.OnModelCreating(modelBuilder);
        }
    }
}

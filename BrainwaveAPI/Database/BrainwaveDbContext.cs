using BrainwaveAPI.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BrainwaveAPI.Database
{
    public class BrainwaveDbContext : DbContext
    {
        public BrainwaveDbContext(DbContextOptions<BrainwaveDbContext> options) : base(options) { }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Quiz> Quizzes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable(nameof(Answer));

                entity.HasKey(_ => _.Id);
                entity.Property(_ => _.Text)
                    .HasMaxLength(255)
                    .IsRequired(false);
                entity.Property(_ => _.Image)
                    .IsRequired(false);
                entity.HasOne<Question>()
                    .WithMany()
                    .HasForeignKey(_ => _.QuestionId);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable(nameof(Question));

                entity.HasKey(_ => _.Id);
                entity.Property(_ => _.Text)
                    .HasMaxLength(255)
                    .IsRequired(false);
                entity.Property(_ => _.Image)
                    .IsRequired(false);
                entity.HasOne<Quiz>()
                    .WithMany()
                    .HasForeignKey(_ => _.QuizId);
                entity.HasMany(_ => _.Answers)
                    .WithOne()
                    .HasForeignKey(_ => _.QuestionId);
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.ToTable(nameof(Quiz));

                entity.HasKey(_ => _.Id);
                entity.Property(_ => _.Name)
                    .HasMaxLength(255)
                    .IsRequired(true);
                entity.Property(_ => _.Description)
                    .HasMaxLength(2048)
                    .IsRequired(false);
                entity.HasMany(_ => _.Questions)
                    .WithOne()
                    .HasForeignKey(_ => _.QuizId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

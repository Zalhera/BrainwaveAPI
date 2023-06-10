using BrainwaveAPI.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BrainwaveAPI.Database
{
    public class BrainwaveDbContext : DbContext, IBrainwaveDbContext
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
                entity.Property(_ => _.Id)
                    .ValueGeneratedOnAdd();
                entity.Property(_ => _.Text)
                    .HasMaxLength(255)
                    .IsRequired(false);
                entity.Property(_ => _.Image)
                    .IsRequired(false);
                entity.HasOne(_ => _.Question)
                    .WithMany(_ => _.Answers)
                    .HasForeignKey(_ => _.QuestionId);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable(nameof(Question));

                entity.HasKey(_ => _.Id);
                entity.Property(_ => _.Id)
                    .ValueGeneratedOnAdd();
                entity.Property(_ => _.Text)
                    .HasMaxLength(255)
                    .IsRequired(false);
                entity.Property(_ => _.Image)
                    .IsRequired(false);
                entity.HasOne(_ => _.Quiz)
                    .WithMany(_ => _.Questions)
                    .HasForeignKey(_ => _.QuizId);
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.ToTable(nameof(Quiz));

                entity.HasKey(_ => _.Id);
                entity.Property(_ => _.Id)
                    .ValueGeneratedOnAdd();
                entity.Property(_ => _.Name)
                    .HasMaxLength(255)
                    .IsRequired(true);
                entity.Property(_ => _.Description)
                    .HasMaxLength(2048)
                    .IsRequired(false);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

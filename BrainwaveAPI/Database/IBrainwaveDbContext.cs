using BrainwaveAPI.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BrainwaveAPI.Database
{
    public interface IBrainwaveDbContext
    {
        DbSet<Answer> Answers { get; set; }
        DbSet<Question> Questions { get; set; }
        DbSet<Quiz> Quizzes { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
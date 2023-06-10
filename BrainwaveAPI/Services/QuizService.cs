using BrainwaveAPI.Database;
using BrainwaveAPI.Database.Models;
using Serilog;

namespace BrainwaveAPI.Services
{
    public class QuizService : IQuizService
    {
        private readonly ILogger<QuizService> _logger;
        private readonly IBrainwaveDbContext _context;
        private readonly IDateTimeProvider _dateTimeProvider;

        public QuizService(ILogger<QuizService> logger, IBrainwaveDbContext context, IDateTimeProvider dateTimeProvider)
        {
            _logger = logger;
            _context = context;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Quiz> CreateQuizAsync(string name, string? description)
        {
            var quiz = new Quiz()
            {
                Name = name,
                Description = description,
                Created = _dateTimeProvider.Now,
                Updated = _dateTimeProvider.Now,
                Visible = false,
            };

            await _context.Quizzes.AddAsync(quiz);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Created new quiz with name [{Name}]", quiz.Name);

            return quiz;
        }
    }
}

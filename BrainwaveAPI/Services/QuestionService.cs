using BrainwaveAPI.Database;
using BrainwaveAPI.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BrainwaveAPI.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly ILogger<QuestionService> _logger;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IBrainwaveDbContext _brainwaveDbContext;

        public QuestionService(ILogger<QuestionService> logger, IDateTimeProvider dateTimeProvider, IBrainwaveDbContext brainwaveDbContext)
        {
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
            _brainwaveDbContext = brainwaveDbContext;
        }

        public async Task<Question> CreateQuestionAsync(long quizId, string text)
        {
            if (!await _brainwaveDbContext.Quizzes.AnyAsync(_ => _.Id == quizId))
            {
                throw new Exception("Quiz does not exists. TODO: Replace with specific exc");
            }

            var question = new Question()
            {
                QuizId = quizId,
                Created = _dateTimeProvider.Now,
                Updated = _dateTimeProvider.Now,
                Text = text,
            };

            await _brainwaveDbContext.Questions.AddAsync(question);
            await _brainwaveDbContext.SaveChangesAsync();

            _logger.LogInformation("Created question for quiz [{QuizId}]", quizId);
            return question;
        }
    }
}

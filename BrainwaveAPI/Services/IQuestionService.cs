using BrainwaveAPI.Database.Models;

namespace BrainwaveAPI.Services
{
    public interface IQuestionService
    {
        Task<Question> CreateQuestionAsync(long quizId, string text);
    }
}

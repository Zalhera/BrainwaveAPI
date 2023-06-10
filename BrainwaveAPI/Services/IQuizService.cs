using BrainwaveAPI.Database.Models;

namespace BrainwaveAPI.Services
{
    public interface IQuizService
    {
        Task<Quiz> CreateQuizAsync(string name, string? description);
    }
}

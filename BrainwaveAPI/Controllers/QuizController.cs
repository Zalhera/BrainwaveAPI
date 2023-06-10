using Brainwave.Common.Models;
using BrainwaveAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BrainwaveAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateQuizAsync([FromBody] CreateQuizModel createQuiz)
        {
            return Ok(await _quizService.CreateQuizAsync(createQuiz.Name, createQuiz.Description));
        }
    }
}

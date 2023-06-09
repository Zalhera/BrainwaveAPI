using Brainwave.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace BrainwaveAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class QuizController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateQuizAsync([FromBody] CreateQuizModel createQuiz)
        {
            // TODO: Create the quiz
            return Ok();
        }
    }
}

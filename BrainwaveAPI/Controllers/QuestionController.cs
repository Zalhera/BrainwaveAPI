using Brainwave.Common.Models;
using BrainwaveAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BrainwaveAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost("{quizId}/create")]
        public async Task<IActionResult> CreateQuestionAsync([FromRoute] long quizId, [FromBody] CreateQuestionModel createQuestion)
        {
            return Ok(await _questionService.CreateQuestionAsync(quizId, createQuestion.Text));
        }
    }
}

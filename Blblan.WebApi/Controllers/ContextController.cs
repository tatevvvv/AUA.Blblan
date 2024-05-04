using Blblan.Common.Models;
using Blblan.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blblan.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContextController : ControllerBase
    {
        private readonly IContextService _contextService;

        public ContextController(IContextService contextService)
        {
            _contextService = contextService;
        }

        [HttpPost]
        public IActionResult SendMessage([FromBody] QuestionDto questionDto)
        {
            // perform validation

            var answer =  _contextService.SendMessage(new QuestionModel(questionDto.content, questionDto.contextId));

            return Ok(new AnswerDto("Sorry, we are not trained yet", 0));
        }
    }
}

using Blblan.Common.Models;
using Blblan.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blblan.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContextController : ControllerBase
    {
        private readonly IConversationService _contextService;

        public ContextController(IConversationService contextService)
        {
            _contextService = contextService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(int userId, [FromBody] QuestionDto questionDto)
        {
            // TODO: perform validation
            // TODO: add auto mappers
            var answer = await _contextService.SendMessageAsync(userId, new QuestionModel(questionDto.content, questionDto.contextId)).ConfigureAwait(false);

            return Ok(answer);
        }

        [HttpPost]
        public async Task<IActionResult> CratetNewConversation(int userId)
        {
            // TODO: perform validation
            // TODO: add auto mappers
            var result = await _contextService.CreateNewConversation(userId);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetModelName()
        {
            // TODO: perform validation
            // TODO: add auto mappers
            await _contextService.GetModelName().ConfigureAwait(false);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConversations(int userId)
        {
            // TODO: perform validation
            // TODO: add auto mappers
            var result  = await _contextService.GetConversationListAsync(userId).ConfigureAwait(false);

            return Ok(result);
        }
    }
}

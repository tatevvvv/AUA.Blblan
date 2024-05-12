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
            var answer = await _contextService.SendMessageAsync(userId, new QuestionModel(questionDto.Message, questionDto.ContextId, questionDto.ModelType)).ConfigureAwait(false);

            return Ok(answer);
        }

        [HttpGet]
        public async Task<List<MessageModel>> GetConversationAllMessagesByUserId(int userId, int conversationId)
        {
            return await _contextService.GetAllConversationsByUserId(userId, conversationId);
        }

        [HttpPost]
        public async Task<IActionResult> CrateNewConversation(int userId)
        {
            var result = await _contextService.CreateNewConversation(userId);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConversations(int userId)
        {
            var result  = await _contextService.GetConversationListAsync(userId).ConfigureAwait(false);

            return Ok(result);
        }
    }
}

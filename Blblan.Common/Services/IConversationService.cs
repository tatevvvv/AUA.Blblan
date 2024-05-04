using Blblan.Common.Models;

namespace Blblan.Common.Services
{
    public interface IConversationService
    {
        Task<AnswerModel> SendMessageAsync(int userId, QuestionModel messageModel);

        Task<ConversationModel> CreateNewConversation(int userId);
    }
}

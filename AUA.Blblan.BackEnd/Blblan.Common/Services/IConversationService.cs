using Blblan.Common.Models;

namespace Blblan.Common.Services
{
    public interface IConversationService
    {
        Task<AnswerModel> SendMessageAsync(int userId, QuestionModel messageModel);

        Task<List<MessageModel>> GetAllConversationByUserId(int userId, int conversationId);

        Task<ConversationModel> CreateNewConversation(int userId);

        Task GetModelName();

        Task<List<ConversationModel>> GetConversationListAsync(int userId);
    }
}

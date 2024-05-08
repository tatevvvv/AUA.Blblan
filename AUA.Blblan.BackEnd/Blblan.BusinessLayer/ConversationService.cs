using Blblan.Common.Models;
using Blblan.Common.Services;
using Blblan.Data.Entities;
using Blblan.Data.Repositories;
using PredictionClientApp;

namespace Blblan.BusinessLayer
{
    internal class ConversationService : IConversationService
    {
        private readonly PredictionEngineClient _predictionEngineClient;
        private readonly ConversationRepository _conversationRepository;
        private readonly MessagesRepository _messagesRepository;
        private readonly UserRepository _userRepository;

        public ConversationService(
            PredictionEngineClient predictionEngineClient,
            ConversationRepository conversationRepository,
            MessagesRepository messagesRepository,
            UserRepository userRepository)
        {
            _predictionEngineClient = predictionEngineClient;
            _conversationRepository = conversationRepository;
            _messagesRepository = messagesRepository;
            _userRepository = userRepository;
        }

        public async Task<AnswerModel> SendMessageAsync(int userId, QuestionModel messageModel)
        {
            var conversation = await _conversationRepository.GetByIdAsync(messageModel.contextId).ConfigureAwait(false);

            if (conversation == null)
            {
                throw new Exception($"Conversation not found error for Id : {messageModel.contextId}");
            }

            try
            {
                // collecting context by conversationID
                var allMessages = _messagesRepository.Set().Where(m => m.ConversationId == conversation.Id).ToList();

                var response = await _predictionEngineClient.MakePredictionAsync(messageModel).ConfigureAwait(false);
                var result = new AnswerModel(response, messageModel.contextId);

                // after getting the response from prediction engine store it in db.

                conversation.Messages.Add(new Message
                {
                    Question = messageModel.content,
                    Answer = response,
                    ConversationId = messageModel.contextId,
                    Conversation = conversation,
                    Timestamp = DateTime.UtcNow,
                });

                return result;
            }
            catch (Exception e)
            {
                return new AnswerModel(e.Message, messageModel.contextId);
            }
        }

        public async Task<ConversationModel> CreateNewConversation(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId).ConfigureAwait(false);

            if (user == null)
            {
                throw new Exception($"User not found for {userId}");
            }

            var conversation = new Conversation
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
            };

            await _conversationRepository.AddAsync(conversation).ConfigureAwait(false);

            return new ConversationModel(conversation.Id, conversation.Name);
        }

        public async Task GetModelName()
        {
            await _predictionEngineClient.GetModelName().ConfigureAwait(false);
        }
    }
}

using System.Net;
using Blblan.Common.Models;
using Blblan.Common.Services;
using Blblan.Data.Entities;
using Blblan.Data.Repositories;
using Microsoft.EntityFrameworkCore;
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
                var result = await _predictionEngineClient.MakePredictionAsync(messageModel, userId).ConfigureAwait(false);
                
                var resultModel = new AnswerModel(result.response, messageModel.contextId);
                
                if (result.statusCode == HttpStatusCode.OK)
                {
                    await _messagesRepository.AddAsync(new Message()
                    {
                        Question = messageModel.content,
                        Answer = result.response,
                        ConversationId = messageModel.contextId,
                        Timestamp = DateTime.UtcNow,
                        Conversation = conversation,
                        UserId = userId
                    }).ConfigureAwait(false);
                    
                    return resultModel;
                }
                else
                {
                    throw new Exception($"Warning on Status Code : {result.statusCode}");
                }
            }
            catch (Exception e)
            {
                return new AnswerModel(e.Message, messageModel.contextId);
            }
        }

        public async Task<List<MessageModel>> GetAllConversationByUserId(int userId, int conversationId)
        {
            List<MessageModel> result = [];
            
            var messages = await _messagesRepository.GetAllAsync().ConfigureAwait(false);
            var filteredMessages =  messages.Where(x => x.UserId == userId && x.ConversationId == conversationId)
                .OrderBy(x => x.Timestamp).ToList();

            foreach (var item in filteredMessages)
            {
                result.Add(new MessageModel()
                {
                    Answer = item.Answer,
                    Question = item.Question,
                    Timestamp = item.Timestamp
                });
            }
            return result;
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

        public async Task<List<ConversationModel>> GetConversationListAsync(int userId)
        {
            var convs = _conversationRepository.AsNoTracking().Where(c => c.UserId == userId);
            var userconv = await convs.Where(c => c.UserId == userId).ToListAsync().ConfigureAwait(false);

            return userconv.Select(c => new ConversationModel(c.Id, c.Name)).ToList();
        }
    }
}

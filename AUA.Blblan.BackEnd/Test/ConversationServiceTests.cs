using Blblan.Common.Models;
using Blblan.Data.Entities;
using Blblan.Data.Repositories;
using NSubstitute;
using PredictionClientApp;

namespace Blblan.BusinessLayer.Tests
{
    [TestFixture]
    public class ConversationServiceTests
    {
        private ConversationService _conversationService;
        private PredictionEngineClient _predictionEngineClient;
        private ConversationRepository _conversationRepository;
        private MessagesRepository _messagesRepository;
        private UserRepository _userRepository;

        [SetUp]
        public void Setup()
        {
            _predictionEngineClient = Substitute.For<PredictionEngineClient>();
            _conversationRepository = Substitute.For<ConversationRepository>();
            _messagesRepository = Substitute.For<MessagesRepository>();
            _userRepository = Substitute.For<UserRepository>();

            _conversationService = new ConversationService(
                _predictionEngineClient,
                _conversationRepository,
                _messagesRepository,
                _userRepository);
        }

        [Test]
        public async Task SendMessageAsync_ConversationNotFound_ThrowsException()
        {
            // Arrange
            int userId = 1;
            var messageModel = new QuestionModel("", 123, 1);
            _conversationRepository.GetByIdAsync(messageModel.ContextId).Returns(Task.FromResult<Conversation>(null));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _conversationService.SendMessageAsync(userId, messageModel));
        }

        [Test]
        public async Task SendMessageAsync_Successful_ReturnsAnswerModel()
        {
            // Arrange
            var messageModel = new QuestionModel("", 123, 2);
            var conversation = new Conversation();
            _conversationRepository.GetByIdAsync(messageModel.ContextId).Returns(Task.FromResult(conversation));
            await _messagesRepository.Received().AddAsync(Arg.Any<Message>()).ConfigureAwait(false);
        }
    }
}

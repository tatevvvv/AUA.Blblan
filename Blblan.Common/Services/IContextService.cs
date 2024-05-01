using Blblan.Common.Models;

namespace Blblan.Common.Services
{
    public interface IContextService
    {
        public AnswerModel SendMessage(QuestionModel messageModel);
    }
}

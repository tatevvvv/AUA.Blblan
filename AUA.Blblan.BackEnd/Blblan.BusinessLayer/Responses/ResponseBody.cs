using Blblan.BusinessLayer.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blblan.BusinessLayer.Responses
{
    public class Usage
    {
        public int PromptTokens { get; set; }
        public int CompletionTokens { get; set; }
        public int TotalTokens { get; set; }
    }

    public class MessageChoice
    {
        public Message Message { get; set; }
        public object Logprobs { get; set; }
        public string FinishReason { get; set; }
        public int Index { get; set; }
    }

    public class ChatCompletionResponse
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public int Created { get; set; }
        public string Model { get; set; }
        public Usage Usage { get; set; }
        public List<MessageChoice> Choices { get; set; }
    }
}

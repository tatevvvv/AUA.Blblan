namespace Blblan.BusinessLayer.Requests
{
    public class RequestBody
    {
        public List<Message> Messages { get; set; }
        public double Temperature { get; set; } = 0.7;
        public int MaxTokens { get; set; } = 10;
        public bool Stream { get; set; }
    }
}

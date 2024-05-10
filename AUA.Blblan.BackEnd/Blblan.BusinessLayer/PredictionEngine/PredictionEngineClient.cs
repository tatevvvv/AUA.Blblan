using System.Net;
using Blblan.BusinessLayer.Requests;
using Blblan.Common.Models;
using Newtonsoft.Json;
using System.Text;

namespace PredictionClientApp
{
    public class PredictionEngineClient : IDisposable
    {
        private readonly HttpClient _httpClient;

        private readonly string baseUrl = "http://localhost:8000";

        public PredictionEngineClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
            
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<(HttpStatusCode statusCode, string response)> MakePredictionAsync(QuestionModel model, int userId)
        {
            try
            {
                var requestModel = new RequestBody
                {
                    userID = userId,
                    conversationID = model.contextId,
                    messageText = model.content
                };

                string jsonContent = JsonConvert.SerializeObject(requestModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("/process_message", content);

                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                
                return (response.StatusCode, result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error making prediction: {e.Message}");
                return new ValueTuple<HttpStatusCode, string>();
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}

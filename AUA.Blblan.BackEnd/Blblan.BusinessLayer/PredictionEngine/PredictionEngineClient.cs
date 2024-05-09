﻿using Blblan.BusinessLayer.Requests;
using Blblan.Common.Models;
using Newtonsoft.Json;
using System.Text;

namespace PredictionClientApp
{
    public class PredictionEngineClient : IDisposable
    {
        private readonly HttpClient _httpClient;

        private readonly string baseUrl = "http://localhost:1234/v1";

        public PredictionEngineClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
            // Optionally set up headers or other settings
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<string> MakePredictionAsync(QuestionModel model)
        {
            try
            {
                var requestModel = new RequestBody
                {
                    Messages = new List<Message>
                    {
                        new Message
                        {
                            Content = "Hello",
                        }
                    }
                };

                string jsonContent = JsonConvert.SerializeObject(requestModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync("/chat/completions", content);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Read the response content
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                // Handle exceptions related to the request
                Console.WriteLine($"Error making prediction: {e.Message}");
                return null;
            }
        }

        public async Task GetModelName()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("/models");

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                // Handle exceptions related to the request
                Console.WriteLine($"Error making prediction: {e.Message}");
            }
        }

        // Implement IDisposable to dispose of the HttpClient
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}

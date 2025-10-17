// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Standard.AI.PeerLLM.Models.Configurations;

namespace Standard.AI.PeerLLM.Brokers.PeerLLMs
{
    internal partial class PeerLLMBroker : IPeerLLMBroker
    {
        private readonly PeerLLMConfiguration peerLLMConfiguration;
        private readonly HttpClient httpClient;

        public PeerLLMBroker(PeerLLMConfiguration peerLLMConfiguration)
        {
            this.peerLLMConfiguration = peerLLMConfiguration;
            this.httpClient = SetupHttpClient();
        }

        private async ValueTask<TResult> PostJsonAsync<TRequest, TResult>(
            string relativeUrl,
            TRequest content,
            CancellationToken cancellationToken = default)
        {
            var json = JsonSerializer.Serialize(content);

            using var request = new HttpRequestMessage(HttpMethod.Post, relativeUrl)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            using var response = await httpClient.SendAsync(
                request,
                completionOption: HttpCompletionOption.ResponseContentRead,
                cancellationToken);

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResult>(responseBody)
                ?? throw new InvalidOperationException(
                    $"Failed to deserialize {typeof(TResult).Name} from response body: {responseBody}");
        }

        private HttpClient SetupHttpClient()
        {
            var httpClient = new HttpClient()
            {
                BaseAddress =
                    new Uri(uriString: this.peerLLMConfiguration.ApiUrl),
            };

            if (!string.IsNullOrWhiteSpace(this.peerLLMConfiguration.ApiKey))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    scheme: "Bearer",
                    parameter: this.peerLLMConfiguration.ApiKey);
            }

            return httpClient;
        }
    }
}

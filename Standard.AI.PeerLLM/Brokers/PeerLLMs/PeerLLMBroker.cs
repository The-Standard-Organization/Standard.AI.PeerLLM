// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
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

        private async IAsyncEnumerable<string> PostJsonStreamAsync<TRequest>(
            string relativeUrl,
            TRequest content,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var json = JsonSerializer.Serialize(content);

            using var request = new HttpRequestMessage(HttpMethod.Post, relativeUrl)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            using var response = await httpClient.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

                throw new HttpRequestException(
                    $"Unexpected status code {(int)response.StatusCode} ({response.StatusCode}). " +
                    $"Response body: {body}",
                    inner: null,
                    statusCode: response.StatusCode);
            }

            await using var responseStream =
                await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            await foreach (var token in JsonSerializer
                .DeserializeAsyncEnumerable<string>(responseStream, jsonOptions, cancellationToken)
                .ConfigureAwait(false))
            {
                if (token is not null)
                    yield return token;
            }
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
                httpClient.DefaultRequestHeaders.Add(
                    name: "X-API-Key",
                    value: this.peerLLMConfiguration.ApiKey);
            }

            return httpClient;
        }
    }
}

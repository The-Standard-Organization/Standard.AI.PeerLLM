// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RESTFulSense.Clients;
using Standard.AI.PeerLLM.Models.Configurations;

namespace Standard.AI.PeerLLM.Brokers.PeerLLMs
{
    internal partial class PeerLLMBroker : IPeerLLMBroker
    {
        private readonly PeerLLMConfiguration peerLLMConfiguration;
        private readonly IRESTFulApiFactoryClient apiClient;
        private readonly HttpClient httpClient;

        public PeerLLMBroker(PeerLLMConfiguration peerLLMConfiguration)
        {
            this.peerLLMConfiguration = peerLLMConfiguration;
            this.httpClient = SetupHttpClient();
            this.apiClient = SetupApiClient();
        }

        private async ValueTask<TResult> PostAsync<TRequest, TResult>(string relativeUrl, TRequest content)
        {
            return await this.apiClient.PostContentAsync<TRequest, TResult>(
                relativeUrl,
                content,
                mediaType: "application/json",
                ignoreDefaultValues: true);
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

            if (!string.IsNullOrWhiteSpace(this.peerLLMConfiguration.OrganizationId))
            {
                httpClient.DefaultRequestHeaders.Add(
                    name: "PeerLLM-Organization",
                    value: this.peerLLMConfiguration.OrganizationId);
            }

            return httpClient;
        }

        private IRESTFulApiFactoryClient SetupApiClient() =>
            new RESTFulApiFactoryClient(this.httpClient);
    }
}

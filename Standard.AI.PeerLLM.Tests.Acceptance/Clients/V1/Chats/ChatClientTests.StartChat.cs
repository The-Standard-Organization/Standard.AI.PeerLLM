// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Standard.AI.PeerLLM.Models.Foundations.Chats;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Standard.AI.PeerLLM.Tests.Acceptance.Clients.V1.Chats
{
    public partial class ChatClientTests : IDisposable
    {
        [Fact]
        public async Task ShouldStartChat()
        {
            // given
            ChatSessionConfig chatSessionConfig = new ChatSessionConfig
            {
                ModelName = GetRandomString(),
            };

            Guid conversationId = Guid.NewGuid();
            Guid expectedConversationId = conversationId;
            var expectedBody = JsonSerializer.Serialize(chatSessionConfig);

            this.wireMockServer.Given(
                Request.Create()
                .UsingPost()
                    .WithPath("/api/chats/start")
                    .WithHeader("Authorization", $"Bearer {this.apiKey}")
                    .WithHeader("Content-Type", "application/json; charset=utf-8")
                    .WithBody(expectedBody))
                .RespondWith(
                    Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(conversationId));

            // when
            Guid actualConversationId =
                await this.peerLLMClient.V1.Chats.StartChatAsync(chatSessionConfig);

            // then
            actualConversationId.Should().Be(expectedConversationId);
        }
    }
}

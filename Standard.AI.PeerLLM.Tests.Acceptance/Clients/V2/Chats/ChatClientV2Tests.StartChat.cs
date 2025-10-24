// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Standard.AI.PeerLLM.Models.Foundations.Chats;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Standard.AI.PeerLLM.Tests.Acceptance.Clients.V2.Chats
{
    public partial class ChatClientV2Tests : IDisposable
    {
        [Fact]
        public async Task ShouldStartChatAsync()
        {
            // given
            ChatSessionConfigV2 chatSessionConfig = new ChatSessionConfigV2
            {
                ModelName = GetRandomString(),
                Role = GetRandomString(),
                RoleContent = GetRandomString(),
                TargetMachines = new List<string> { GetRandomString(), GetRandomString() },
                FallBack = true,
                AntiPrompts = new List<string> { GetRandomString(), GetRandomString() }
            };

            Guid conversationId = Guid.NewGuid();
            Guid expectedConversationId = conversationId;
            var expectedBody = JsonSerializer.Serialize(chatSessionConfig);

            this.wireMockServer.Given(
                Request.Create()
                .UsingPost()
                    .WithPath("/api/v2/chats/start")
                    .WithHeader("X-API-Key", this.apiKey)
                    .WithHeader("Content-Type", "application/json; charset=utf-8")
                    .WithBody(expectedBody))
                .RespondWith(
                    Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(conversationId));

            // when
            Guid actualConversationId =
                await this.peerLLMClient.V2.Chats.StartChatAsync(chatSessionConfig);

            // then
            actualConversationId.Should().Be(expectedConversationId);
        }
    }
}

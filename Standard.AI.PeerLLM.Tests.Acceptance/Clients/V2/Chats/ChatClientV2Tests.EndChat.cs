// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Standard.AI.PeerLLM.Tests.Acceptance.Clients.V2.Chats
{
    public partial class ChatClientV2Tests : IDisposable
    {
        [Fact]
        public async Task ShouldEndChat()
        {
            // given
            Guid conversationId = Guid.NewGuid();
            string expectedResponse = "Conversation ended successfully";
            string expectedBody = JsonSerializer.Serialize(conversationId);

            this.wireMockServer.Given(
                Request.Create()
                .UsingPost()
                    .WithPath("/api/v2/chats/end")
                    .WithHeader("Authorization", $"Bearer {this.apiKey}")
                    .WithHeader("Content-Type", "application/json; charset=utf-8")
                    .WithBody(expectedBody)
                    )
                .RespondWith(
                    Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new { message = expectedResponse }));

            // when
            string actualResponse =
                await this.peerLLMClient.V2.Chats.EndChatAsync(conversationId);

            // then
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }
    }
}

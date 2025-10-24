// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Standard.AI.PeerLLM.Tests.Acceptance.Clients.V1.Chats
{
    public partial class ChatClientV1Tests : IDisposable
    {
        [Fact]
        public async Task ShouldStreamChatAsync()
        {
            // given
            Guid conversationId = Guid.NewGuid();
            string text = GetRandomString();
            Guid expectedConversationId = conversationId;
            var expectedBody = JsonSerializer.Serialize(new { ConversationId = conversationId, Text = text });
            CancellationToken cancellationToken = CancellationToken.None;
            var expectedList = await ToListAsync(GetAsyncEnumerableOfRandomStrings(), cancellationToken);

            this.wireMockServer.Given(
                Request.Create()
                .UsingPost()
                    .WithPath("/api/chats/stream")
                    .WithHeader("X-API-Key", this.apiKey)
                    .WithHeader("Content-Type", "application/json; charset=utf-8")
                    .WithBody(expectedBody))
                .RespondWith(
                    Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(expectedList));

            // when
            IAsyncEnumerable<string> actualResponse =
                this.peerLLMClient.V1.Chats.StreamChatAsync(conversationId, text);

            var actualList = await ToListAsync(actualResponse, cancellationToken);

            // then
            actualList.Should().Equal(expectedList);
        }
    }
}

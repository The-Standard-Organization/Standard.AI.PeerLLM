// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Standard.AI.PeerLLM.Models.Foundations.Chats;

namespace Standard.AI.PeerLLM.Tests.Integration.Clients.V2.Chats
{
    public partial class ChatClientV1Tests
    {
        [Fact]
        public async Task ShouldStartStreamAndEndAsync()
        {
            // given
            ChatSessionConfigV2 chatSessionConfig = new ChatSessionConfigV2
            {
                ModelName = "mistral-7b-instruct-v0.1.Q8_0"
            };

            List<string> tokens = new List<string>();

            // when
            Guid conversationId = await this.peerLLMClient.V2.Chats.StartChatAsync(chatSessionConfig);

            IAsyncEnumerable<string> responseStream = this.peerLLMClient.V2.Chats.StreamChatAsync(
                conversationId, text: "Hello, how are you?");

            await foreach (string token in responseStream)
            {
                tokens.Add(token);
            }

            string result = string.Concat(tokens);
            string message = await this.peerLLMClient.V2.Chats.EndChatAsync(conversationId);

            // then
            conversationId.Should().NotBeEmpty();
            tokens.Count.Should().BeGreaterThan(0);
        }
    }
}

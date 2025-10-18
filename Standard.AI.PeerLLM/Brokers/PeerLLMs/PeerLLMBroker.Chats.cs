// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Standard.AI.PeerLLM.Models.Foundations.Chats;

namespace Standard.AI.PeerLLM.Brokers.PeerLLMs
{
    internal partial class PeerLLMBroker : IPeerLLMBroker
    {
        public async ValueTask<Guid> StartChatAsync(
            ChatSessionConfig chatSessionConfig,
            string relativeUrl,
            CancellationToken cancellationToken = default)
        {
            return await PostJsonAsync<ChatSessionConfig, Guid>(
                relativeUrl,
                content: chatSessionConfig,
                cancellationToken);
        }

        public IAsyncEnumerable<string> StreamChatAsync(
            Guid conversationId,
            string text,
            string relativeUrl,
            CancellationToken cancellationToken = default)
        {
            return PostJsonStreamAsync(
                relativeUrl,
                content: new { ConversationId = conversationId, Text = text },
                cancellationToken);
        }

        public async ValueTask<string> EndChatAsync(
            Guid conversationId,
            string relativeUrl,
            CancellationToken cancellationToken = default)
        {
            dynamic response = await PostJsonAsync<Guid, dynamic>(
                relativeUrl,
                content: conversationId,
                cancellationToken);

            return response.GetProperty("message").GetString();
        }
    }
}

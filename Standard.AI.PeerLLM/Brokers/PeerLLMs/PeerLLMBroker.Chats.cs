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
            CancellationToken cancellationToken = default)
        {
            return await PostJsonAsync<ChatSessionConfig, Guid>(
                relativeUrl: "chats/start",
                content: chatSessionConfig,
                cancellationToken);
        }

        public IAsyncEnumerable<string> StreamChatAsync(
            Guid conversationId,
            string text,
            CancellationToken cancellationToken = default)
        {
            return PostJsonStreamAsync(
                relativeUrl: "chats/stream",
                content: new { ConversationId = conversationId, Text = text },
                cancellationToken);
        }
    }
}

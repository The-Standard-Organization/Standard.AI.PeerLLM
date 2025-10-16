// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Standard.AI.PeerLLM.Models.Foundations.Chats;

namespace Standard.AI.PeerLLM.Brokers.PeerLLMs
{
    internal partial class PeerLLMBroker : IPeerLLMBroker
    {
        public async ValueTask<Guid> StartChatSessionAsync(ChatSessionConfig chatSessionConfig)
        {
            return await PostAsync<ChatSessionConfig, Guid>(
                relativeUrl: "chats/start",
                content: chatSessionConfig);
        }

        public IAsyncEnumerable<string> SubmitChatAsync(Guid conversationId, string text) =>
            throw new NotImplementedException();

        public async ValueTask<string> EndChatSessionAsync(Guid conversationId) =>
            throw new NotImplementedException();
    }
}

// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Standard.AI.PeerLLM.Models.Foundations.Chats;

namespace Standard.AI.PeerLLM.Brokers.PeerLLMs
{
    internal partial class PeerLLMBroker : IPeerLLMBroker
    {
        public async ValueTask<Guid> StartChatSessionAsync(ChatSessionConfig chatSessionConfig) =>
            await PostJsonAsync<ChatSessionConfig, Guid>(relativeUrl: "chats/start", content: chatSessionConfig);
    }
}

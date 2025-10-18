// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using Standard.AI.PeerLLM.Models.Foundations.Chats;

namespace Standard.AI.PeerLLM.Brokers.PeerLLMs
{
    internal partial class PeerLLMBroker : IPeerLLMBroker
    {
        public async ValueTask<Guid> StartChatV2Async(
            ChatSessionConfigV2 chatSessionConfig,
            string relativeUrl,
            CancellationToken cancellationToken = default)
        {
            return await PostJsonAsync<ChatSessionConfigV2, Guid>(
                relativeUrl,
                content: chatSessionConfig,
                cancellationToken);
        }
    }
}

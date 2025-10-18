// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using Standard.AI.PeerLLM.Models.Foundations.Chats;

namespace Standard.AI.PeerLLM.Brokers.PeerLLMs
{
    internal partial interface IPeerLLMBroker
    {
        ValueTask<Guid> StartChatV2Async(
            ChatSessionConfigV2 chatSessionConfig,
            string relativeUrl,
            CancellationToken cancellationToken = default);
    }
}

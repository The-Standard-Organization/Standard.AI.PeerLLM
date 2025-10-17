// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using Standard.AI.PeerLLM.Brokers.PeerLLMs;
using Standard.AI.PeerLLM.Models.Foundations.Chats;

namespace Standard.AI.PeerLLM.Services.Foundations.Chats
{
    internal partial class ChatService : IChatService
    {
        private readonly IPeerLLMBroker peerLLMBroker;

        public ChatService(IPeerLLMBroker peerLLMBroker) =>
            this.peerLLMBroker = peerLLMBroker;

        public ValueTask<Guid> StartChatAsync(
            ChatSessionConfig chatSessionConfig,
            CancellationToken cancellationToken = default) =>
                this.peerLLMBroker.StartChatAsync(chatSessionConfig, cancellationToken);
    }
}

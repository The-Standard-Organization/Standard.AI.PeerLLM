// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
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
        TryCatch(async () =>
        {
            ValidateOnStartChat(chatSessionConfig);

            return await this.peerLLMBroker.StartChatAsync(chatSessionConfig, cancellationToken);
        });

        public IAsyncEnumerable<string> StreamChatAsync(
            Guid conversationId,
            string text,
            CancellationToken cancellationToken = default) =>
        TryCatch(() =>
        {
            ValidateOnStreamChat(conversationId, text);

            return this.peerLLMBroker.StreamChatAsync(conversationId, text, cancellationToken);
        });

        public ValueTask<string> EndChatAsync(Guid conversationId, CancellationToken cancellationToken = default) =>
        TryCatch(async () =>
        {
            ValidateOnEndChat(conversationId);

            return await this.peerLLMBroker.EndChatAsync(conversationId, cancellationToken);
        });
    }
}

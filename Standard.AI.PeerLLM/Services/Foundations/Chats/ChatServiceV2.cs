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
    internal partial class ChatServiceV2 : IChatServiceV2
    {
        private readonly IPeerLLMBroker peerLLMBroker;
        public string StartChatRelativeUrl { get; private set; } = "api/v2/chats/start";
        public string StreamChatRelativeUrl { get; private set; } = "api/v2/chats/stream";
        public string EndChatRelativeUrl { get; private set; } = "api/v2/chats/end";

        public ChatServiceV2(IPeerLLMBroker peerLLMBroker) =>
            this.peerLLMBroker = peerLLMBroker;

        public ValueTask<Guid> StartChatAsync(
            ChatSessionConfigV2 chatSessionConfig,
            CancellationToken cancellationToken = default) =>
        TryCatch(async () =>
        {
            ValidateOnStartChat(chatSessionConfig);

            return await this.peerLLMBroker.StartChatV2Async(
                chatSessionConfig,
                relativeUrl: StartChatRelativeUrl,
                cancellationToken);
        });

        public IAsyncEnumerable<string> StreamChatAsync(
            Guid conversationId,
            string text,
            CancellationToken cancellationToken = default) =>
        TryCatch(() =>
        {
            ValidateOnStreamChat(conversationId, text);

            return this.peerLLMBroker.StreamChatAsync(
                conversationId,
                text,
                relativeUrl: StreamChatRelativeUrl,
                cancellationToken);
        });

        public ValueTask<string> EndChatAsync(Guid conversationId, CancellationToken cancellationToken = default) =>
        TryCatch(async () =>
        {
            ValidateOnEndChat(conversationId);

            return await this.peerLLMBroker.EndChatAsync(
                conversationId,
                relativeUrl: EndChatRelativeUrl,
                cancellationToken);
        });
    }
}

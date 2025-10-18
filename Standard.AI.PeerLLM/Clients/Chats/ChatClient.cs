// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Standard.AI.PeerLLM.Models.Foundations.Chats;
using Standard.AI.PeerLLM.Services.Foundations.Chats;

namespace Standard.AI.PeerLLM.Clients.Chats
{
    internal class ChatClient : IChatClient
    {
        private readonly IChatService chatService;

        public ChatClient(IChatService chatService) =>
            this.chatService = chatService;

        public ValueTask<string> EndChatAsync(
            Guid conversationId,
            CancellationToken cancellationToken = default) =>
                throw new NotImplementedException();

        public ValueTask<Guid> StartChatAsync(
            ChatSessionConfig chatSessionConfig,
            CancellationToken cancellationToken = default) =>
                throw new NotImplementedException();

        public IAsyncEnumerable<string> StreamChatAsync(
            Guid conversationId,
            string text,
            CancellationToken cancellationToken = default) =>
                throw new NotImplementedException();
    }
}

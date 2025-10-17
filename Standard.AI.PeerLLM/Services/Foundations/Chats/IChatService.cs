// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Standard.AI.PeerLLM.Models.Foundations.Chats;

namespace Standard.AI.PeerLLM.Services.Foundations.Chats
{
    internal interface IChatService
    {
        ValueTask<Guid> StartChatAsync(
            ChatSessionConfig chatSessionConfig,
            CancellationToken cancellationToken = default);

        IAsyncEnumerable<string> StreamChatAsync(
            Guid conversationId,
            string text,
            CancellationToken cancellationToken = default);

        ValueTask<string> EndChatAsync(
            Guid conversationId,
            CancellationToken cancellationToken = default);
    }
}

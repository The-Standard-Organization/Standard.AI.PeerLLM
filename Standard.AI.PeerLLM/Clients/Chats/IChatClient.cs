// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using Standard.AI.PeerLLM.Models.Foundations.Chats;

namespace Standard.AI.PeerLLM.Clients.Chats
{
    /// <summary>
    /// Defines the contract for interacting with the PeerLLM chat session API.
    /// Provides methods to start a chat session, stream responses, and end a chat.
    /// </summary>
    public interface IChatClient
    {
        /// <summary>
        /// Starts a new chat session with PeerLLM.
        /// </summary>
        /// <param name="chatSessionConfig">
        /// Configuration settings for the session, including model name, role,
        /// system prompt, target machines, fallbacks, and anti-prompts.
        /// </param>
        /// <param name="cancellationToken">
        /// A token that can be used to cancel the asynchronous operation.
        /// </param>
        /// <returns>
        /// A <see cref="Guid"/> representing the conversation ID assigned
        /// by PeerLLM for this session.
        /// </returns>
        /// <remarks>
        /// This method corresponds to the <c>POST /chats/start</c> endpoint.
        /// </remarks>
        /// <exception cref="ChatClientValidationException" />
        /// <exception cref="ChatClientDependencyException" />
        /// <exception cref="ChatClientServiceException" />
        ValueTask<Guid> StartChatAsync(
            ChatSessionConfig chatSessionConfig,
            CancellationToken cancellationToken = default);
    }
}

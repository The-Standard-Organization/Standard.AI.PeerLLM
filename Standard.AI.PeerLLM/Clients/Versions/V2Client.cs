// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Standard.AI.PeerLLM.Clients.Chats;

namespace Standard.AI.PeerLLM.Clients.Versions
{
    /// <summary>
    /// Defines the contract for interacting with the PeerLLM V2 APIs.
    /// </summary>
    internal class V2Client : IV2Client
    {
        /// <summary>
        /// Gets the chat client used to interact with PeerLLM chat endpoints.
        /// </summary>
        public IChatClientV2 Chats { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="V2Client"/> class with the specified chat client.
        /// </summary>
        /// <param name="chatClient">The chat client implementation used for chat interactions.</param>
        public V2Client(IChatClientV2 chatClient) =>
            Chats = chatClient;
    }
}

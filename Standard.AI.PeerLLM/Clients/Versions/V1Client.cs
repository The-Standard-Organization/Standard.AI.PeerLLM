// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

namespace Standard.AI.PeerLLM.Clients.Chats
{
    /// <summary>
    /// Defines the contract for interacting with the PeerLLM V1 APIs.
    /// </summary>
    internal class V1Client : IV1Client
    {
        /// <summary>
        /// Gets the chat client used to interact with PeerLLM chat endpoints.
        /// </summary>
        public IChatClient Chats { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="V1Client"/> class with the specified chat client.
        /// </summary>
        /// <param name="chatClient">The chat client implementation used for chat interactions.</param>
        public V1Client(IChatClient chatClient) =>
            this.Chats = chatClient;
    }
}

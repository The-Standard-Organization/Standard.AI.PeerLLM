// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Standard.AI.PeerLLM.Clients.Chats;

namespace Standard.AI.PeerLLM.Clients.Versions
{
    /// <summary>
    /// Defines the contract for interacting with the PeerLLM V1 APIs.
    /// </summary>
    public interface IV1Client
    {
        IChatClient Chats { get; }
    }
}

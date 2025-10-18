// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

namespace Standard.AI.PeerLLM.Clients.Chats
{
    /// <summary>
    /// Defines the contract for interacting with the PeerLLM V2 APIs.
    /// </summary>
    public interface IV2Client
    {
        IChatClientV2 Chats { get; }
    }
}

// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Standard.AI.PeerLLM.Clients.Chats;

namespace Standard.AI.OpenAI.Clients.OpenAIs
{
    public interface IPeerLLMClient
    {
        IV1Client V1 { get; }
        IV2Client V2 { get; }
    }
}

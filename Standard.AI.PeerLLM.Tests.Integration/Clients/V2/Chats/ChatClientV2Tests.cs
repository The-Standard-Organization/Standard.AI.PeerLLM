// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Standard.AI.PeerLLM.Clients;
using Standard.AI.PeerLLM.Models.Configurations;

namespace Standard.AI.PeerLLM.Tests.Integration.Clients.V2.Chats
{
    public partial class ChatClientV1Tests
    {
        private readonly IPeerLLMClient peerLLMClient;

        public ChatClientV1Tests()
        {
            var peerLLMConfiguration = new PeerLLMConfiguration();
            this.peerLLMClient = new PeerLLMClient(peerLLMConfiguration);
        }
    }
}

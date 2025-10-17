// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Moq;
using Standard.AI.PeerLLM.Brokers.PeerLLMs;
using Standard.AI.PeerLLM.Models.Foundations.Chats;
using Standard.AI.PeerLLM.Services.Foundations.Chats;
using Tynamix.ObjectFiller;

namespace Standard.AI.PeerLLM.Tests.Unit.Services.Foundations.Chats
{
    public partial class ChatServiceTests
    {
        private readonly Mock<IPeerLLMBroker> peerLLMBrokerMock;
        private readonly ChatService chatService;

        public ChatServiceTests()
        {
            this.peerLLMBrokerMock = new Mock<IPeerLLMBroker>();

            this.chatService = new ChatService(
                peerLLMBroker: this.peerLLMBrokerMock.Object);
        }

        private static ChatSessionConfig CreateRandomChatSessionConfig() =>
            CreateChatSessionConfigFiller().Create();

        private static Filler<ChatSessionConfig> CreateChatSessionConfigFiller()
        {
            var filler = new Filler<ChatSessionConfig>();
            filler.Setup();

            return filler;
        }
    }
}

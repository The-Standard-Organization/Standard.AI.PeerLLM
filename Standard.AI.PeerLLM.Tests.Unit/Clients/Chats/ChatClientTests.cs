// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Moq;
using Standard.AI.PeerLLM.Clients.Chats;
using Standard.AI.PeerLLM.Models.Foundations.Chats;
using Standard.AI.PeerLLM.Services.Foundations.Chats;
using Tynamix.ObjectFiller;

namespace Standard.AI.PeerLLM.Tests.Unit.Clients.Chats
{
    public partial class ChatClientTests
    {
        private readonly Mock<IChatService> chatServiceMock;
        private readonly ChatClient chatClient;

        public ChatClientTests()
        {
            this.chatServiceMock = new Mock<IChatService>();
            this.chatClient = new ChatClient(chatService: this.chatServiceMock.Object);
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

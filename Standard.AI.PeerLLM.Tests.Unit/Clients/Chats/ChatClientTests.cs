// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Moq;
using Standard.AI.PeerLLM.Clients.Chats;
using Standard.AI.PeerLLM.Models.Foundations.Chats;
using Standard.AI.PeerLLM.Models.Foundations.Chats.Exceptions;
using Standard.AI.PeerLLM.Services.Foundations.Chats;
using Tynamix.ObjectFiller;
using Xeptions;

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

        public static TheoryData<Xeption> ValidationExceptions()
        {
            string randomMessage = GetRandomString();
            string exceptionMessage = randomMessage;
            var someException = new Xeption(exceptionMessage);

            someException.AddData(
                key: "conversationId",
                values: "Id is required");

            return new TheoryData<Xeption>
            {
                new ChatValidationException(
                    message: "Chat validation error occured, fix errors and try again",
                    innerException: someException),

                new ChatDependencyValidationException(
                    message: "Chat dependency validation error occurred, fix errors and try again",
                    innerException: someException)
            };
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static string GetRandomString() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

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

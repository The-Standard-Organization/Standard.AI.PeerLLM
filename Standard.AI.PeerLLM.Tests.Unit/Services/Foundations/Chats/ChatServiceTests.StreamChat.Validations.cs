// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Standard.AI.PeerLLM.Models.Foundations.Chats.Exceptions;

namespace Standard.AI.PeerLLM.Tests.Unit.Services.Foundations.Chats
{
    public partial class ChatServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnStreamChatIfArgumentsIsInvalidAsync(string invalidText)
        {
            // given
            Guid inputConversationId = Guid.Empty;
            string inputText = invalidText;
            CancellationToken cancellationToken = CancellationToken.None;

            var invalidArgumentsChatException =
                new InvalidArgumentsChatException(
                    message: "Invalid chat arguments. Please correct the errors and try again.");

            invalidArgumentsChatException.AddData(
                key: "conversationId",
                values: "Id is required");

            invalidArgumentsChatException.AddData(
                key: "text",
                values: "Text is required");

            var expectedChatValidationException =
                new ChatValidationException(
                    message: "Chat validation error occurred, fix errors and try again.",
                    innerException: invalidArgumentsChatException);

            // when
            Task StreamChatTask() => EnumerateAsync(
                source: this.chatService.StreamChatAsync(inputConversationId, inputText, cancellationToken),
                cancellationToken);

            ChatValidationException actualChatValidationException =
                await Assert.ThrowsAsync<ChatValidationException>(StreamChatTask);

            // then
            actualChatValidationException.Should()
                .BeEquivalentTo(expectedChatValidationException);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.StreamChatAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }
    }
}
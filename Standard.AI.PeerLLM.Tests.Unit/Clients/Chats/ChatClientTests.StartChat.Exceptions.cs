// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Standard.AI.PeerLLM.Models.Foundations.Chats;
using Standard.AI.PeerLLM.Models.Foundations.Chats.Exceptions;
using Xeptions;

namespace Standard.AI.PeerLLM.Tests.Unit.Clients.Chats
{
    public partial class ChatClientTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowValidationExceptionOnStartChatIfValidationErrorOccurredAsync(
            Xeption validationException)
        {
            // given
            ChatSessionConfig someChatSessionConfig = CreateRandomChatSessionConfig();
            CancellationToken cancellationToken = CancellationToken.None;

            var expectedChatClientValidationException =
                new ChatClientValidationException(
                    message: "Chat client validation error occurred, fix errors and try again.",
                    innerException: validationException,
                    data: validationException.Data);

            this.chatServiceMock.Setup(service =>
                service.StartChatAsync(It.IsAny<ChatSessionConfig>(), It.IsAny<CancellationToken>()))
                    .ThrowsAsync(validationException);

            // when
            ValueTask<Guid> startChatTask =
                this.chatClient.StartChatAsync(someChatSessionConfig, cancellationToken);

            ChatClientValidationException actualChatClientValidationException =
                await Assert.ThrowsAsync<ChatClientValidationException>(
                    startChatTask.AsTask);

            // then
            actualChatClientValidationException.Should()
                .BeEquivalentTo(expectedChatClientValidationException);

            this.chatServiceMock.Verify(service =>
                service.StartChatAsync(It.IsAny<ChatSessionConfig>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.chatServiceMock.VerifyNoOtherCalls();
        }
    }
}

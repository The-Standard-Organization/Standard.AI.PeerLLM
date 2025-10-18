// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Standard.AI.PeerLLM.Models.Clients.Chats.Exceptions;
using Xeptions;

namespace Standard.AI.PeerLLM.Tests.Unit.Clients.Chats
{
    public partial class ChatClientTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowValidationExceptionOnEndChatIfValidationErrorOccurredAsync(
            Xeption validationException)
        {
            // given
            Guid someConversationId = Guid.NewGuid();
            CancellationToken cancellationToken = CancellationToken.None;

            var expectedChatClientValidationException =
                new ChatClientValidationException(
                    message: "Chat client validation error occurred, fix errors and try again.",
                    innerException: validationException.InnerException as Xeption,
                    data: validationException.InnerException.Data);

            this.chatServiceMock.Setup(service =>
                service.EndChatAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                    .ThrowsAsync(validationException);

            // when
            ValueTask<string> endChatTask =
                this.chatClient.EndChatAsync(someConversationId, cancellationToken);

            ChatClientValidationException actualChatClientDependencyException =
                await Assert.ThrowsAsync<ChatClientValidationException>(
                    endChatTask.AsTask);

            // then
            actualChatClientDependencyException.Should()
                .BeEquivalentTo(expectedChatClientValidationException);

            this.chatServiceMock.Verify(service =>
                service.EndChatAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.chatServiceMock.VerifyNoOtherCalls();
        }
    }
}

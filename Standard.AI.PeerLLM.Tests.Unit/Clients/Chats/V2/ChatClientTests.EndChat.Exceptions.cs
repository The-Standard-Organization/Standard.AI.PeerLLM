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

namespace Standard.AI.PeerLLM.Tests.Unit.Clients.Chats.V2
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
                this.chatClientV2.EndChatAsync(someConversationId, cancellationToken);

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

        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnEndChatIfDependencyErrorOccurredAsync(
            Xeption dependencyException)
        {
            // given
            Guid someConversationId = Guid.NewGuid();
            CancellationToken cancellationToken = CancellationToken.None;

            var expectedChatClientDependencyException =
                new ChatClientDependencyException(
                    message: "Chat client dependency error occurred, contact support.",
                    innerException: dependencyException.InnerException as Xeption,
                    data: dependencyException.InnerException.Data);

            this.chatServiceMock.Setup(service =>
                service.EndChatAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<string> endChatTask =
                this.chatClientV2.EndChatAsync(someConversationId, cancellationToken);

            ChatClientDependencyException actualChatClientDependencyException =
                await Assert.ThrowsAsync<ChatClientDependencyException>(
                    endChatTask.AsTask);

            // then
            actualChatClientDependencyException.Should()
                .BeEquivalentTo(expectedChatClientDependencyException);

            this.chatServiceMock.Verify(service =>
                service.EndChatAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.chatServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnEndChatIfServiceErrorOccurredAsync()
        {
            // given
            Guid someConversationId = Guid.NewGuid();
            CancellationToken cancellationToken = CancellationToken.None;
            var serviceException = new Exception();

            var failedChatClientServiceException =
                new FailedChatClientServiceException(
                    message: "Failed chat client service error occurred, contact support.",
                    innerException: serviceException,
                    data: serviceException.Data);

            var expectedChatClientServiceException =
                new ChatClientServiceException(
                    message: "Chat client service error occurred, contact support.",
                    innerException: failedChatClientServiceException as Xeption,
                    data: failedChatClientServiceException.Data);

            this.chatServiceMock.Setup(service =>
                service.EndChatAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<string> endChatTask =
                this.chatClientV2.EndChatAsync(someConversationId, cancellationToken);

            ChatClientServiceException actualChatClientServiceException =
                await Assert.ThrowsAsync<ChatClientServiceException>(
                    endChatTask.AsTask);

            // then
            actualChatClientServiceException.Should()
                .BeEquivalentTo(expectedChatClientServiceException);

            this.chatServiceMock.Verify(service =>
                service.EndChatAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.chatServiceMock.VerifyNoOtherCalls();
        }
    }
}

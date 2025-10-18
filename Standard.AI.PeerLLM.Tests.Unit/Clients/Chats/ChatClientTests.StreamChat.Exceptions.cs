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
        public async Task ShouldThrowValidationExceptionOnStreamChatIfValidationErrorOccurredAsync(
            Xeption validationException)
        {
            // given
            Guid someConversationId = Guid.NewGuid();
            string someText = GetRandomString();
            CancellationToken cancellationToken = CancellationToken.None;

            var expectedChatClientValidationException =
                new ChatClientValidationException(
                    message: "Chat client validation error occurred, fix errors and try again.",
                    innerException: validationException.InnerException as Xeption,
                    data: validationException.InnerException.Data);

            this.chatServiceMock.Setup(broker =>
                broker.StreamChatAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                    .Throws(validationException);

            // when
            Task StreamChatTask() => EnumerateAsync(
                source: this.chatClient.StreamChatAsync(someConversationId, someText, cancellationToken),
                cancellationToken);

            ChatClientValidationException actualChatClientDependencyException =
                await Assert.ThrowsAsync<ChatClientValidationException>(StreamChatTask);

            // then
            actualChatClientDependencyException.Should()
                .BeEquivalentTo(expectedChatClientValidationException);

            this.chatServiceMock.Verify(broker =>
                broker.StreamChatAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.chatServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnStreamChatIfDependencyErrorOccurredAsync(
            Xeption dependencyException)
        {
            // given
            Guid someConversationId = Guid.NewGuid();
            string someText = GetRandomString();
            CancellationToken cancellationToken = CancellationToken.None;

            var expectedChatClientDependencyException =
                new ChatClientDependencyException(
                    message: "Chat client dependency error occurred, contact support.",
                    innerException: dependencyException.InnerException as Xeption,
                    data: dependencyException.InnerException.Data);

            this.chatServiceMock.Setup(broker =>
                broker.StreamChatAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                    .Throws(dependencyException);

            // when
            Task StreamChatTask() => EnumerateAsync(
                source: this.chatClient.StreamChatAsync(someConversationId, someText, cancellationToken),
                cancellationToken);

            ChatClientDependencyException actualChatClientDependencyException =
                await Assert.ThrowsAsync<ChatClientDependencyException>(StreamChatTask);

            // then
            actualChatClientDependencyException.Should()
                .BeEquivalentTo(expectedChatClientDependencyException);

            this.chatServiceMock.Verify(broker =>
                broker.StreamChatAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.chatServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnStreamChatIfServiceErrorOccurredAsync()
        {
            // given
            Guid someConversationId = Guid.NewGuid();
            string someText = GetRandomString();
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
                service.StreamChatAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                    .Throws(serviceException);

            // when
            Task StreamChatTask() => EnumerateAsync(
                source: this.chatClient.StreamChatAsync(someConversationId, someText, cancellationToken),
                cancellationToken);

            ChatClientServiceException actualChatClientServiceException =
                await Assert.ThrowsAsync<ChatClientServiceException>(StreamChatTask);

            // then
            actualChatClientServiceException.Should()
                .BeEquivalentTo(expectedChatClientServiceException);

            this.chatServiceMock.Verify(service =>
                service.StreamChatAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.chatServiceMock.VerifyNoOtherCalls();
        }
    }
}

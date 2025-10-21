// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Standard.AI.PeerLLM.Models.Foundations.Chats.Exceptions;

namespace Standard.AI.PeerLLM.Tests.Unit.Services.Foundations.Chats.V1
{
    public partial class ChatServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnEndChatIfBadRequestErrorOccurredAsync()
        {
            // given
            Guid someConversationId = Guid.NewGuid();
            string someText = GetRandomString();
            CancellationToken cancellationToken = CancellationToken.None;

            var httpRequestException =
                new HttpRequestException(
                    message: "Selected host unavailable",
                    inner: null,
                    statusCode: System.Net.HttpStatusCode.BadRequest);

            var hostNotFoundChatException =
                new HostNotFoundChatException(
                    message: "Host unavailable",
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

            var expectedChatDependencyValidationException =
                new ChatDependencyValidationException(
                    message: "Chat dependency validation error occurred, fix errors and try again.",
                    innerException: hostNotFoundChatException);

            this.peerLLMBrokerMock.Setup(broker =>
                broker.EndChatAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                    .Throws(httpRequestException);

            // when
            ValueTask<string> endChatTask = this.chatService.EndChatAsync(someConversationId, cancellationToken);

            ChatDependencyValidationException actualChatDependencyValidationException =
                await Assert.ThrowsAsync<ChatDependencyValidationException>(endChatTask.AsTask);

            // then
            actualChatDependencyValidationException.Should()
                .BeEquivalentTo(expectedChatDependencyValidationException);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.EndChatAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnEndChatIfNotFoundErrorOccurredAsync()
        {
            // given
            Guid someConversationId = Guid.NewGuid();
            string someText = GetRandomString();
            CancellationToken cancellationToken = CancellationToken.None;

            var httpRequestException =
                new HttpRequestException(
                    message: "Conversation not found",
                    inner: null,
                    statusCode: System.Net.HttpStatusCode.NotFound);

            var conversationNotFoundChatException =
                new ConversationNotFoundChatException(
                    message: "Conversation not found",
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

            var expectedChatDependencyValidationException =
                new ChatDependencyValidationException(
                    message: "Chat dependency validation error occurred, fix errors and try again.",
                    innerException: conversationNotFoundChatException);

            this.peerLLMBrokerMock.Setup(broker =>
                broker.EndChatAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                    .Throws(httpRequestException);

            // when
            ValueTask<string> endChatTask = this.chatService.EndChatAsync(someConversationId, cancellationToken);

            ChatDependencyValidationException actualChatDependencyValidationException =
                await Assert.ThrowsAsync<ChatDependencyValidationException>(endChatTask.AsTask);

            // then
            actualChatDependencyValidationException.Should()
                .BeEquivalentTo(expectedChatDependencyValidationException);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.EndChatAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnEndChatIfTooManyRequestsOccurredAsync()
        {
            // given
            Guid someConversationId = Guid.NewGuid();
            string someText = GetRandomString();
            CancellationToken cancellationToken = CancellationToken.None;

            var httpRequestException =
                new HttpRequestException(
                    message: "Too Many Requests",
                    inner: null,
                    statusCode: System.Net.HttpStatusCode.TooManyRequests);

            var tooManyRequestsChatException =
                new TooManyRequestsChatException(
                    message: httpRequestException.Message,
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

            var expectedChatDependencyValidationException =
                new ChatDependencyValidationException(
                    message: "Chat dependency validation error occurred, fix errors and try again.",
                    innerException: tooManyRequestsChatException);

            this.peerLLMBrokerMock.Setup(broker =>
                broker.EndChatAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                    .Throws(httpRequestException);

            // when
            ValueTask<string> endChatTask = this.chatService.EndChatAsync(someConversationId, cancellationToken);

            ChatDependencyValidationException actualChatDependencyValidationException =
                await Assert.ThrowsAsync<ChatDependencyValidationException>(endChatTask.AsTask);

            // then
            actualChatDependencyValidationException.Should()
                .BeEquivalentTo(expectedChatDependencyValidationException);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.EndChatAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnEndChatIfForbiddenOccurredAsync()
        {
            // given
            Guid someConversationId = Guid.NewGuid();
            string someText = GetRandomString();
            CancellationToken cancellationToken = CancellationToken.None;

            var httpRequestException =
                new HttpRequestException(
                    message: "Forbidden",
                    inner: null,
                    statusCode: System.Net.HttpStatusCode.Forbidden);

            var forbiddenChatException =
                new ForbiddenChatException(
                    message: httpRequestException.Message,
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

            var expectedChatDependencyValidationException =
                new ChatDependencyValidationException(
                    message: "Chat dependency validation error occurred, fix errors and try again.",
                    innerException: forbiddenChatException);

            this.peerLLMBrokerMock.Setup(broker =>
                broker.EndChatAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                    .Throws(httpRequestException);

            // when
            ValueTask<string> endChatTask = this.chatService.EndChatAsync(someConversationId, cancellationToken);

            ChatDependencyValidationException actualChatDependencyValidationException =
                await Assert.ThrowsAsync<ChatDependencyValidationException>(endChatTask.AsTask);

            // then
            actualChatDependencyValidationException.Should()
                .BeEquivalentTo(expectedChatDependencyValidationException);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.EndChatAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnEndChatIfExternalErrorOccurredAsync()
        {
            // given
            Guid someConversationId = Guid.NewGuid();
            string someText = GetRandomString();
            CancellationToken cancellationToken = CancellationToken.None;

            var httpRequestException =
                new HttpRequestException(
                    message: "Conversation not found",
                    inner: null,
                    statusCode: System.Net.HttpStatusCode.MethodNotAllowed);

            var externalChatException =
                new ExternalChatException(
                    message: "External validation error",
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

            var expectedChatDependencyValidationException =
                new ChatDependencyValidationException(
                    message: "Chat dependency validation error occurred, fix errors and try again.",
                    innerException: externalChatException);

            this.peerLLMBrokerMock.Setup(broker =>
                broker.EndChatAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                    .Throws(httpRequestException);

            // when
            ValueTask<string> endChatTask = this.chatService.EndChatAsync(someConversationId, cancellationToken);

            ChatDependencyValidationException actualChatDependencyValidationException =
                await Assert.ThrowsAsync<ChatDependencyValidationException>(endChatTask.AsTask);

            // then
            actualChatDependencyValidationException.Should()
                .BeEquivalentTo(expectedChatDependencyValidationException);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.EndChatAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnEndChatIfErrorOccurredAsync()
        {
            // given
            Guid someConversationId = Guid.NewGuid();
            string someText = GetRandomString();
            CancellationToken cancellationToken = CancellationToken.None;
            var serviceException = new Exception();

            var failedChatServiceException =
                new FailedChatServiceException(
                    message: "Failed chat service exception occurred, please contact support for assistance.",
                    innerException: serviceException,
                    data: serviceException.Data);

            var expectedChatServiceException =
                new ChatServiceException(
                    message: "Chat service error occurred, please contact support.",
                    innerException: failedChatServiceException);

            this.peerLLMBrokerMock.Setup(broker =>
                broker.EndChatAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                    .Throws(serviceException);

            // when
            ValueTask<string> endChatTask = this.chatService.EndChatAsync(someConversationId, cancellationToken);

            ChatServiceException actualChatServiceException =
                await Assert.ThrowsAsync<ChatServiceException>(endChatTask.AsTask);

            // then
            actualChatServiceException.Should()
                .BeEquivalentTo(expectedChatServiceException);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.EndChatAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }
    }
}
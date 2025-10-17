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

namespace Standard.AI.PeerLLM.Tests.Unit.Services.Foundations.Chats
{
    public partial class ChatServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnStreamChatIfBadRequestErrorOccurredAsync()
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
                broker.StreamChatAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                    .Throws(httpRequestException);

            // when
            Task StreamChatTask() => EnumerateAsync(
                source: this.chatService.StreamChatAsync(someConversationId, someText, cancellationToken),
                cancellationToken);

            ChatDependencyValidationException actualChatDependencyValidationException =
                await Assert.ThrowsAsync<ChatDependencyValidationException>(StreamChatTask);

            // then
            actualChatDependencyValidationException.Should()
                .BeEquivalentTo(expectedChatDependencyValidationException);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.StreamChatAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnStreamChatIfNotFoundErrorOccurredAsync()
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
                broker.StreamChatAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                    .Throws(httpRequestException);

            // when
            Task StreamChatTask() => EnumerateAsync(
                source: this.chatService.StreamChatAsync(someConversationId, someText, cancellationToken),
                cancellationToken);

            ChatDependencyValidationException actualChatDependencyValidationException =
                await Assert.ThrowsAsync<ChatDependencyValidationException>(StreamChatTask);

            // then
            actualChatDependencyValidationException.Should()
                .BeEquivalentTo(expectedChatDependencyValidationException);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.StreamChatAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }
    }
}
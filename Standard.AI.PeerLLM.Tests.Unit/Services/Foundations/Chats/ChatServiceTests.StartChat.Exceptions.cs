// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Standard.AI.PeerLLM.Models.Foundations.Chats;
using Standard.AI.PeerLLM.Models.Foundations.Chats.Exceptions;

namespace Standard.AI.PeerLLM.Tests.Unit.Services.Foundations.Chats
{
    public partial class ChatServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnStartChatIfBadRequestErrorOccurredAsync()
        {
            // given
            ChatSessionConfig someChatSessionConfig = CreateRandomChatSessionConfig();
            CancellationToken cancellationToken = CancellationToken.None;

            var badRequestException =
                new HttpRequestException(
                    message: "Bad Request",
                    inner: null,
                    statusCode: System.Net.HttpStatusCode.BadRequest);

            var hostNotFoundChatException =
                new HostNotFoundChatException(
                    message: "No hosts available for this model",
                    innerException: badRequestException,
                    data: badRequestException.Data);

            var expectedChatDependencyValidationException =
                new ChatDependencyValidationException(
                    message: "Chat dependency validation error occurred, fix errors and try again.",
                    innerException: hostNotFoundChatException);

            this.peerLLMBrokerMock.Setup(broker =>
                broker.StartChatAsync(It.IsAny<ChatSessionConfig>(), It.IsAny<CancellationToken>()))
                    .ThrowsAsync(badRequestException);

            // when
            ValueTask<Guid> startChatTask =
                this.chatService.StartChatAsync(someChatSessionConfig, cancellationToken);

            ChatDependencyValidationException actualChatDependencyValidationException =
                await Assert.ThrowsAsync<ChatDependencyValidationException>(
                    startChatTask.AsTask);

            // then
            actualChatDependencyValidationException.Should()
                .BeEquivalentTo(expectedChatDependencyValidationException);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.StartChatAsync(It.IsAny<ChatSessionConfig>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnStartChatIfServiceErrorOccurredAsync()
        {
            // given
            ChatSessionConfig someChatSessionConfig = CreateRandomChatSessionConfig();
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
                broker.StartChatAsync(It.IsAny<ChatSessionConfig>(), It.IsAny<CancellationToken>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<Guid> startChatTask =
                this.chatService.StartChatAsync(someChatSessionConfig, cancellationToken);

            ChatServiceException actualChatServiceException =
                await Assert.ThrowsAsync<ChatServiceException>(
                    startChatTask.AsTask);

            // then
            actualChatServiceException.Should()
                .BeEquivalentTo(expectedChatServiceException);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.StartChatAsync(It.IsAny<ChatSessionConfig>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }
    }
}
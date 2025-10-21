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

namespace Standard.AI.PeerLLM.Tests.Unit.Services.Foundations.Chats.V2
{
    public partial class ChatServiceV2Tests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnStartChatIfBadRequestErrorOccurredAsync()
        {
            // given
            ChatSessionConfigV2 someChatSessionConfig = CreateRandomChatSessionConfig();
            CancellationToken cancellationToken = CancellationToken.None;

            var badRequestException =
                new HttpRequestException(
                    message: "Bad Request",
                    inner: null,
                    statusCode: System.Net.HttpStatusCode.BadRequest);

            var hostNotFoundChatException =
                new HostNotFoundChatException(
                    message: badRequestException.Message,
                    innerException: badRequestException,
                    data: badRequestException.Data);

            var expectedChatDependencyValidationException =
                new ChatDependencyValidationException(
                    message: "Chat dependency validation error occurred, fix errors and try again.",
                    innerException: hostNotFoundChatException);

            this.peerLLMBrokerMock.Setup(broker =>
                broker.StartChatV2Async(
                    It.IsAny<ChatSessionConfigV2>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                        .ThrowsAsync(badRequestException);

            // when
            ValueTask<Guid> startChatTask =
                this.chatServiceV2.StartChatAsync(someChatSessionConfig, cancellationToken);

            ChatDependencyValidationException actualChatDependencyValidationException =
                await Assert.ThrowsAsync<ChatDependencyValidationException>(
                    startChatTask.AsTask);

            // then
            actualChatDependencyValidationException.Should()
                .BeEquivalentTo(expectedChatDependencyValidationException);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.StartChatV2Async(
                    It.IsAny<ChatSessionConfigV2>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()),
                        Times.Once);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnStartChatIfTooManyRequestsOccurredAsync()
        {
            // given
            ChatSessionConfigV2 someChatSessionConfig = CreateRandomChatSessionConfig();
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
                broker.StartChatV2Async(
                    It.IsAny<ChatSessionConfigV2>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                        .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Guid> startChatTask =
                this.chatServiceV2.StartChatAsync(someChatSessionConfig, cancellationToken);

            ChatDependencyValidationException actualChatDependencyValidationException =
                await Assert.ThrowsAsync<ChatDependencyValidationException>(
                    startChatTask.AsTask);

            // then
            actualChatDependencyValidationException.Should()
                .BeEquivalentTo(expectedChatDependencyValidationException);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.StartChatV2Async(
                    It.IsAny<ChatSessionConfigV2>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()),
                        Times.Once);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnStartChatIfServiceErrorOccurredAsync()
        {
            // given
            ChatSessionConfigV2 someChatSessionConfig = CreateRandomChatSessionConfig();
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
                broker.StartChatV2Async(
                    It.IsAny<ChatSessionConfigV2>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                        .ThrowsAsync(serviceException);

            // when
            ValueTask<Guid> startChatTask =
                this.chatServiceV2.StartChatAsync(someChatSessionConfig, cancellationToken);

            ChatServiceException actualChatServiceException =
                await Assert.ThrowsAsync<ChatServiceException>(
                    startChatTask.AsTask);

            // then
            actualChatServiceException.Should()
                .BeEquivalentTo(expectedChatServiceException);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.StartChatV2Async(
                    It.IsAny<ChatSessionConfigV2>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()),
                        Times.Once);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }
    }
}
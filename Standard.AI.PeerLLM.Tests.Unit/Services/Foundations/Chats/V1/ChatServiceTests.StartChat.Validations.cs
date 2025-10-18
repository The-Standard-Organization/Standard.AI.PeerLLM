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

namespace Standard.AI.PeerLLM.Tests.Unit.Services.Foundations.Chats.V1
{
    public partial class ChatServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnStartChatIfChatSessionConfigIsNullAsync()
        {
            // given
            string relativeUrl = chatService.StartChatRelativeUrl;
            ChatSessionConfig nullChatSessionConfig = null;
            CancellationToken cancellationToken = CancellationToken.None;

            var nullChatSessionConfigException =
                new NullChatSessionConfigException(message: "ChatSessionConfig is null.");

            var expectedChatValidationException =
                new ChatValidationException(
                    message: "Chat validation error occurred, fix errors and try again.",
                    innerException: nullChatSessionConfigException);

            // when
            ValueTask<Guid> startChatTask =
                this.chatService.StartChatAsync(nullChatSessionConfig, cancellationToken);

            ChatValidationException actualChatValidationException =
                await Assert.ThrowsAsync<ChatValidationException>(
                    startChatTask.AsTask);

            // then
            actualChatValidationException.Should()
                .BeEquivalentTo(expectedChatValidationException);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.StartChatAsync(nullChatSessionConfig, relativeUrl, cancellationToken),
                    Times.Never);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnStartChatIfModelIsInvalidAsync(string invalidText)
        {
            // given
            string relativeUrl = chatService.StartChatRelativeUrl;

            ChatSessionConfig invalidChatSessionConfig = new ChatSessionConfig
            {
                ModelName = invalidText,
            };

            CancellationToken cancellationToken = CancellationToken.None;

            var invalidChatSessionConfigException =
                new InvalidChatSessionConfigException(
                    message: "Invalid chat session config. Please correct the errors and try again.");

            invalidChatSessionConfigException.AddData(
                key: nameof(ChatSessionConfig.ModelName),
                values: "Text is required");

            var expectedChatValidationException =
                new ChatValidationException(
                    message: "Chat validation error occurred, fix errors and try again.",
                    innerException: invalidChatSessionConfigException);

            // when
            ValueTask<Guid> startChatTask = this.chatService.StartChatAsync(invalidChatSessionConfig);

            ChatValidationException actualChatValidationException =
                await Assert.ThrowsAsync<ChatValidationException>(
                    startChatTask.AsTask);

            // then
            actualChatValidationException.Should()
                .BeEquivalentTo(expectedChatValidationException);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.StartChatAsync(invalidChatSessionConfig, relativeUrl, cancellationToken),
                    Times.Never);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }
    }
}
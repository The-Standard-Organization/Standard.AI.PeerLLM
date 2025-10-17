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

namespace Standard.AI.PeerLLM.Tests.Unit.Services.Foundations.Chats
{
    public partial class ChatServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnStartChatIfChatSessionConfigIsNullAsync()
        {
            // given
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
                broker.StartChatAsync(nullChatSessionConfig, cancellationToken),
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
            ChatSessionConfig invalidChatSessionConfig = new ChatSessionConfig
            {
                ModelName = invalidText,
            };

            CancellationToken cancellationToken = CancellationToken.None;

            var invalidChatSessionConfigException =
                new InvalidChatSessionConfigException(message: "Chat session config is invalid.");

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
                broker.StartChatAsync(invalidChatSessionConfig, cancellationToken),
                    Times.Never);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }
    }
}
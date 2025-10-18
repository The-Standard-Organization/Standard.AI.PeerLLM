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

namespace Standard.AI.PeerLLM.Tests.Unit.Services.Foundations.Chats.V2
{
    public partial class ChatServiceV2Tests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnStartChatIfChatSessionConfigIsNullAsync()
        {
            // given
            string relativeUrl = chatServiceV2.StartChatRelativeUrl;
            ChatSessionConfigV2 nullChatSessionConfig = null;
            CancellationToken cancellationToken = CancellationToken.None;

            var nullChatSessionConfigException =
                new NullChatSessionConfigException(message: "ChatSessionConfig is null.");

            var expectedChatValidationException =
                new ChatValidationException(
                    message: "Chat validation error occurred, fix errors and try again.",
                    innerException: nullChatSessionConfigException);

            // when
            ValueTask<Guid> startChatTask =
                this.chatServiceV2.StartChatAsync(nullChatSessionConfig, cancellationToken);

            ChatValidationException actualChatValidationException =
                await Assert.ThrowsAsync<ChatValidationException>(
                    startChatTask.AsTask);

            // then
            actualChatValidationException.Should()
                .BeEquivalentTo(expectedChatValidationException);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.StartChatV2Async(nullChatSessionConfig, relativeUrl, cancellationToken),
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
            string relativeUrl = chatServiceV2.StartChatRelativeUrl;

            ChatSessionConfigV2 invalidChatSessionConfig = new ChatSessionConfigV2
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
            ValueTask<Guid> startChatTask = this.chatServiceV2.StartChatAsync(invalidChatSessionConfig);

            ChatValidationException actualChatValidationException =
                await Assert.ThrowsAsync<ChatValidationException>(
                    startChatTask.AsTask);

            // then
            actualChatValidationException.Should()
                .BeEquivalentTo(expectedChatValidationException);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.StartChatV2Async(invalidChatSessionConfig, relativeUrl, cancellationToken),
                    Times.Never);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }
    }
}
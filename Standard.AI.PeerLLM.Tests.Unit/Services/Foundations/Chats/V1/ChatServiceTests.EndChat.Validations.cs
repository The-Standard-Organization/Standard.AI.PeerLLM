// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
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
        public async Task ShouldThrowValidationExceptionOnEndChatIfArgumentsIsInvalidAsync()
        {
            // given
            string relativeUrl = chatService.EndChatRelativeUrl;
            Guid inputConversationId = Guid.Empty;
            CancellationToken cancellationToken = CancellationToken.None;

            var invalidArgumentsChatException =
                new InvalidArgumentsChatException(
                    message: "Invalid chat arguments. Please correct the errors and try again.");

            invalidArgumentsChatException.AddData(
                key: "conversationId",
                values: "Id is required");

            var expectedChatValidationException =
                new ChatValidationException(
                    message: "Chat validation error occurred, fix errors and try again.",
                    innerException: invalidArgumentsChatException);

            // when
            ValueTask<string> endChatTask = this.chatService.EndChatAsync(inputConversationId, cancellationToken);

            ChatValidationException actualChatValidationException =
                await Assert.ThrowsAsync<ChatValidationException>(endChatTask.AsTask);

            // then
            actualChatValidationException.Should()
                .BeEquivalentTo(expectedChatValidationException);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.EndChatAsync(inputConversationId, relativeUrl, cancellationToken),
                    Times.Never);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }
    }
}
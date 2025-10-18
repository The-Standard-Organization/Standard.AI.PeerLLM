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
using Xeptions;

namespace Standard.AI.PeerLLM.Tests.Unit.Clients.Chats
{
    public partial class ChatClientTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowValidationExceptionOnStartChatIfValidationErrorOccurredAsync(
            Xeption validationException)
        {
            // given
            ChatSessionConfig someChatSessionConfig = CreateRandomChatSessionConfig();
            CancellationToken cancellationToken = CancellationToken.None;

            var expectedChatClientDependencyException =
                new ChatClientDependencyException(
                    message: "Chat client validation error occurred, fix errors and try again.",
                    innerException: validationException.InnerException as Xeption,
                    data: validationException.InnerException.Data);

            this.chatServiceMock.Setup(service =>
                service.StartChatAsync(It.IsAny<ChatSessionConfig>(), It.IsAny<CancellationToken>()))
                    .ThrowsAsync(validationException);

            // when
            ValueTask<Guid> startChatTask =
                this.chatClient.StartChatAsync(someChatSessionConfig, cancellationToken);

            ChatClientDependencyException actualChatClientDependencyException =
                await Assert.ThrowsAsync<ChatClientDependencyException>(
                    startChatTask.AsTask);

            // then
            actualChatClientDependencyException.Should()
                .BeEquivalentTo(expectedChatClientDependencyException);

            this.chatServiceMock.Verify(service =>
                service.StartChatAsync(It.IsAny<ChatSessionConfig>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.chatServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnStartChatIfDependencyErrorOccurredAsync(
            Xeption dependencyException)
        {
            // given
            ChatSessionConfig someChatSessionConfig = CreateRandomChatSessionConfig();
            CancellationToken cancellationToken = CancellationToken.None;

            var expectedChatClientDependencyException =
                new ChatClientDependencyException(
                    message: "Chat client dependency error occurred, contact support.",
                    innerException: dependencyException.InnerException as Xeption,
                    data: dependencyException.InnerException.Data);

            this.chatServiceMock.Setup(service =>
                service.StartChatAsync(It.IsAny<ChatSessionConfig>(), It.IsAny<CancellationToken>()))
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<Guid> startChatTask =
                this.chatClient.StartChatAsync(someChatSessionConfig, cancellationToken);

            ChatClientDependencyException actualChatClientDependencyException =
                await Assert.ThrowsAsync<ChatClientDependencyException>(
                    startChatTask.AsTask);

            // then
            actualChatClientDependencyException.Should()
                .BeEquivalentTo(expectedChatClientDependencyException);

            this.chatServiceMock.Verify(service =>
                service.StartChatAsync(It.IsAny<ChatSessionConfig>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.chatServiceMock.VerifyNoOtherCalls();
        }
    }
}

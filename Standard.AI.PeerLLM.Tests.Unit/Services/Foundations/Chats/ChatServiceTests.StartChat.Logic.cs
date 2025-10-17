// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Standard.AI.PeerLLM.Models.Foundations.Chats;

namespace Standard.AI.PeerLLM.Tests.Unit.Services.Foundations.Chats
{
    public partial class ChatServiceTests
    {
        [Fact]
        public async Task ShouldStartChatAsync()
        {
            // given
            ChatSessionConfig chatSessionConfig = CreateRandomChatSessionConfig();
            CancellationToken cancellationToken = CancellationToken.None;
            Guid expectedConversationId = Guid.NewGuid();

            this.peerLLMBrokerMock.Setup(broker =>
                broker.StartChatAsync(chatSessionConfig, cancellationToken))
                    .ReturnsAsync(expectedConversationId);

            // when
            Guid actualConversationId =
                await this.chatService.StartChatAsync(
                    chatSessionConfig);

            // then
            actualConversationId.Should().Be(expectedConversationId);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.StartChatAsync(chatSessionConfig, cancellationToken),
                    Times.Once);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }
    }
}

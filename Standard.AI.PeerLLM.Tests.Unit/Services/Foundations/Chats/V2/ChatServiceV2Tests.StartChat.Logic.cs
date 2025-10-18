// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Standard.AI.PeerLLM.Models.Foundations.Chats;

namespace Standard.AI.PeerLLM.Tests.Unit.Services.Foundations.Chats.V2
{
    public partial class ChatServiceV2Tests
    {
        [Fact]
        public async Task ShouldStartChatAsync()
        {
            // given
            string relativeUrl = chatServiceV2.StartChatRelativeUrl;
            ChatSessionConfigV2 chatSessionConfig = CreateRandomChatSessionConfig();
            CancellationToken cancellationToken = CancellationToken.None;
            Guid expectedConversationId = Guid.NewGuid();

            this.peerLLMBrokerMock.Setup(broker =>
                broker.StartChatV2Async(chatSessionConfig, relativeUrl, cancellationToken))
                    .ReturnsAsync(expectedConversationId);

            // when
            Guid actualConversationId =
                await this.chatServiceV2.StartChatAsync(
                    chatSessionConfig);

            // then
            actualConversationId.Should().Be(expectedConversationId);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.StartChatV2Async(chatSessionConfig, relativeUrl, cancellationToken),
                    Times.Once);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }
    }
}

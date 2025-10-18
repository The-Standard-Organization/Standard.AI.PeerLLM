// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;

namespace Standard.AI.PeerLLM.Tests.Unit.Services.Foundations.Chats.V1
{
    public partial class ChatServiceTests
    {
        [Fact]
        public async Task ShouldEndChatAsync()
        {
            // given
            string relativeUrl = chatService.EndChatRelativeUrl;
            Guid inputConversationId = Guid.NewGuid();
            CancellationToken cancellationToken = CancellationToken.None;
            string outputText = GetRandomString();
            string expectedText = outputText;

            this.peerLLMBrokerMock.Setup(broker =>
                broker.EndChatAsync(inputConversationId, relativeUrl, cancellationToken))
                    .ReturnsAsync(outputText);

            // when
            string actualText = await this.chatService.EndChatAsync(inputConversationId, cancellationToken);

            // then
            actualText.Should().BeEquivalentTo(expectedText);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.EndChatAsync(inputConversationId, relativeUrl, cancellationToken),
                    Times.Once);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }
    }
}

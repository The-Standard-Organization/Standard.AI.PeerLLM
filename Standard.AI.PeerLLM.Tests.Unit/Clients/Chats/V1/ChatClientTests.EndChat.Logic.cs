// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;

namespace Standard.AI.PeerLLM.Tests.Unit.Clients.Chats.V1
{
    public partial class ChatClientTests
    {
        [Fact]
        public async Task ShouldEndChatAsync()
        {
            // given
            Guid inputConversationId = Guid.NewGuid();
            CancellationToken cancellationToken = CancellationToken.None;
            string outputText = GetRandomString();
            string expectedText = outputText;

            this.chatServiceMock.Setup(service =>
                service.EndChatAsync(inputConversationId, cancellationToken))
                    .ReturnsAsync(outputText);

            // when
            string actualText =
                await this.chatClient.EndChatAsync(
                    inputConversationId);

            // then
            actualText.Should().Be(expectedText);

            this.chatServiceMock.Verify(service =>
                service.EndChatAsync(inputConversationId, cancellationToken),
                    Times.Once);

            this.chatServiceMock.VerifyNoOtherCalls();
        }
    }
}

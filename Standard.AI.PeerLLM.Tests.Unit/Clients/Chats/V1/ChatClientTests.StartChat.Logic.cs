// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Standard.AI.PeerLLM.Models.Foundations.Chats;

namespace Standard.AI.PeerLLM.Tests.Unit.Clients.Chats.V1
{
    public partial class ChatClientTests
    {
        [Fact]
        public async Task ShouldStartChatAsync()
        {
            // given
            ChatSessionConfig chatSessionConfig = CreateRandomChatSessionConfig();
            CancellationToken cancellationToken = CancellationToken.None;
            Guid expectedConversationId = Guid.NewGuid();

            this.chatServiceMock.Setup(service =>
                service.StartChatAsync(chatSessionConfig, cancellationToken))
                    .ReturnsAsync(expectedConversationId);

            // when
            Guid actualConversationId =
                await this.chatClient.StartChatAsync(
                    chatSessionConfig);

            // then
            actualConversationId.Should().Be(expectedConversationId);

            this.chatServiceMock.Verify(service =>
                service.StartChatAsync(chatSessionConfig, cancellationToken),
                    Times.Once);

            this.chatServiceMock.VerifyNoOtherCalls();
        }
    }
}

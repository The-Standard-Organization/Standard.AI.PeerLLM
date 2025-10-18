// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;

namespace Standard.AI.PeerLLM.Tests.Unit.Services.Foundations.Chats.V1
{
    public partial class ChatServiceTests
    {
        [Fact]
        public async Task ShouldStreamChatAsync()
        {
            // given
            string relativeUrl = chatService.StreamChatRelativeUrl;
            Guid inputConversationId = Guid.NewGuid();
            string inputText = GetRandomString();
            CancellationToken cancellationToken = CancellationToken.None;
            var expectedItems = await ToListAsync(GetAsyncEnumerableOfRandomStrings(), cancellationToken);

            this.peerLLMBrokerMock.Setup(broker =>
                broker.StreamChatAsync(inputConversationId, inputText, relativeUrl, cancellationToken))
                    .Returns(ToAsyncStream(expectedItems));

            // when
            IAsyncEnumerable<string> actualResponse =
                this.chatService.StreamChatAsync(inputConversationId, inputText, cancellationToken);

            var actualList = await ToListAsync(actualResponse, cancellationToken);

            // then
            actualList.Should().Equal(expectedItems);

            this.peerLLMBrokerMock.Verify(broker =>
                broker.StreamChatAsync(inputConversationId, inputText, relativeUrl, cancellationToken),
                    Times.Once);

            this.peerLLMBrokerMock.VerifyNoOtherCalls();
        }
    }
}

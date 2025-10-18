// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;

namespace Standard.AI.PeerLLM.Tests.Unit.Clients.Chats.V2
{
    public partial class ChatClientTests
    {
        [Fact]
        public async Task ShouldStreamChatAsync()
        {
            // given
            Guid inputConversationId = Guid.NewGuid();
            string inputText = GetRandomString();
            CancellationToken cancellationToken = CancellationToken.None;
            var expectedItems = await ToListAsync(GetAsyncEnumerableOfRandomStrings(), cancellationToken);

            this.chatServiceMock.Setup(service =>
                service.StreamChatAsync(inputConversationId, inputText, cancellationToken))
                    .Returns(ToAsyncStream(expectedItems));

            // when
            IAsyncEnumerable<string> actualResponse =
                this.chatClientV2.StreamChatAsync(inputConversationId, inputText, cancellationToken);

            var actualList = await ToListAsync(actualResponse, cancellationToken);

            // then
            actualList.Should().Equal(expectedItems);

            this.chatServiceMock.Verify(service =>
                service.StreamChatAsync(inputConversationId, inputText, cancellationToken),
                    Times.Once);

            this.chatServiceMock.VerifyNoOtherCalls();
        }
    }
}

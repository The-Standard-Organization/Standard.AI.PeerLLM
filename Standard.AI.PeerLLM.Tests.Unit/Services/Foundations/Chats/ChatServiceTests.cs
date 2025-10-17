// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Standard.AI.PeerLLM.Brokers.PeerLLMs;
using Standard.AI.PeerLLM.Models.Foundations.Chats;
using Standard.AI.PeerLLM.Services.Foundations.Chats;
using Tynamix.ObjectFiller;

namespace Standard.AI.PeerLLM.Tests.Unit.Services.Foundations.Chats
{
    public partial class ChatServiceTests
    {
        private readonly Mock<IPeerLLMBroker> peerLLMBrokerMock;
        private readonly ChatService chatService;

        public ChatServiceTests()
        {
            this.peerLLMBrokerMock = new Mock<IPeerLLMBroker>();

            this.chatService = new ChatService(
                peerLLMBroker: this.peerLLMBrokerMock.Object);
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static string GetRandomString() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static ChatSessionConfig CreateRandomChatSessionConfig() =>
            CreateChatSessionConfigFiller().Create();

        private static Filler<ChatSessionConfig> CreateChatSessionConfigFiller()
        {
            var filler = new Filler<ChatSessionConfig>();
            filler.Setup();

            return filler;
        }

        public static async IAsyncEnumerable<string> GetAsyncEnumerableOfRandomStrings(
            int count = 10,
            int wordsPerItem = 1,
            [EnumeratorCancellation] System.Threading.CancellationToken cancellationToken = default)
        {
            var generator = new MnemonicString(wordsPerItem);

            for (int i = 0; i < count; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return generator.GetValue();
                await Task.Yield();
            }
        }

        private static async Task<List<string>> ToListAsync(
            IAsyncEnumerable<string> source,
            CancellationToken cancellationToken = default)
        {
            var list = new List<string>();
            await foreach (var item in source.WithCancellation(cancellationToken))
                list.Add(item);
            return list;
        }

        private static async IAsyncEnumerable<string> ToAsyncStream(
            IEnumerable<string> items,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            foreach (var item in items)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return item;
                await Task.Yield();
            }
        }

        private static async Task EnumerateAsync<T>(
            IAsyncEnumerable<T> source,
            CancellationToken cancellationToken = default)
        {
            await foreach (var _ in source.WithCancellation(cancellationToken))
            { }
        }
    }
}

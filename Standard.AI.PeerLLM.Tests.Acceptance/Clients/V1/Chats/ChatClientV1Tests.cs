// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Clients.OpenAIs;
using Standard.AI.PeerLLM.Models.Configurations;
using Tynamix.ObjectFiller;
using WireMock.Server;

namespace Standard.AI.PeerLLM.Tests.Acceptance.Clients.V1.Chats
{
    public partial class ChatClientV1Tests : IDisposable
    {
        private readonly string apiKey;
        private readonly WireMockServer wireMockServer;
        private readonly IPeerLLMClient peerLLMClient;

        public ChatClientV1Tests()
        {
            this.apiKey = GetRandomString();
            this.wireMockServer = WireMockServer.Start();

            var peerLLMConfiguration = new PeerLLMConfiguration
            {
                ApiUrl = this.wireMockServer.Url,
                ApiKey = this.apiKey,
            };

            this.peerLLMClient = new PeerLLMClient(peerLLMConfiguration);
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        public void Dispose() => this.wireMockServer.Stop();

        private static async Task<List<string>> ToListAsync(
            IAsyncEnumerable<string> source,
            CancellationToken cancellationToken = default)
        {
            var list = new List<string>();
            await foreach (var item in source.WithCancellation(cancellationToken))
                list.Add(item);
            return list;
        }

        public static async IAsyncEnumerable<string> GetAsyncEnumerableOfRandomStrings(
            int count = 10,
            int wordsPerItem = 1,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var generator = new MnemonicString(wordsPerItem);

            for (int i = 0; i < count; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return generator.GetValue();
                await Task.Yield();
            }
        }
    }
}

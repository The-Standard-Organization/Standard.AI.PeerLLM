// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Microsoft.Extensions.DependencyInjection;
using Standard.AI.PeerLLM.Brokers.PeerLLMs;
using Standard.AI.PeerLLM.Clients.Chats;
using Standard.AI.PeerLLM.Clients.Versions;
using Standard.AI.PeerLLM.Models.Configurations;
using Standard.AI.PeerLLM.Services.Foundations.Chats;

namespace Standard.AI.PeerLLM.Clients
{
    public class PeerLLMClient : IPeerLLMClient
    {
        public PeerLLMClient(PeerLLMConfiguration peerLLMConfiguration)
        {
            IServiceProvider serviceProvider = RegisterServices(peerLLMConfiguration);
            InitializeClients(serviceProvider);
        }

        public IV1Client V1 { get; private set; }
        public IV2Client V2 { get; private set; }

        private void InitializeClients(IServiceProvider serviceProvider)
        {
            V1 = serviceProvider.GetRequiredService<IV1Client>();
            V2 = serviceProvider.GetRequiredService<IV2Client>();
        }

        private static IServiceProvider RegisterServices(PeerLLMConfiguration peerLLMConfiguration)
        {
            var serviceCollection = new ServiceCollection()
                .AddTransient<IPeerLLMBroker, PeerLLMBroker>()
                .AddTransient<IChatService, ChatService>()
                .AddTransient<IChatServiceV2, ChatServiceV2>()
                .AddTransient<IChatClient, ChatClient>()
                .AddTransient<IChatClientV2, ChatClientV2>()
                .AddSingleton(peerLLMConfiguration);

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}

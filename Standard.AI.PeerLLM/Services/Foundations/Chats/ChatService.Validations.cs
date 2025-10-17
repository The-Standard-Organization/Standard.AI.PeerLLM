// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Standard.AI.PeerLLM.Models.Foundations.Chats;
using Standard.AI.PeerLLM.Models.Foundations.Chats.Exceptions;

namespace Standard.AI.PeerLLM.Services.Foundations.Chats
{
    internal partial class ChatService : IChatService
    {
        private static void ValidateOnStartChat(ChatSessionConfig chatSessionConfig)
        {
            ValidateChatSessionConfigNotNull(chatSessionConfig);
        }

        private static void ValidateChatSessionConfigNotNull(ChatSessionConfig chatSessionConfig)
        {
            if (chatSessionConfig is null)
            {
                throw new NullChatSessionConfigException(message: "ChatSessionConfig is null.");
            }
        }
    }
}

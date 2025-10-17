// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Standard.AI.PeerLLM.Models.Foundations.Chats;
using Standard.AI.PeerLLM.Models.Foundations.Chats.Exceptions;
using Xeptions;

namespace Standard.AI.PeerLLM.Services.Foundations.Chats
{
    internal partial class ChatService : IChatService
    {
        private static void ValidateOnStartChat(ChatSessionConfig chatSessionConfig)
        {
            ValidateChatSessionConfigNotNull(chatSessionConfig);

            Validate(
                exceptionFactory: () => new InvalidChatSessionConfigException(
                    message: "Invalid chat session config. Please correct the errors and try again."),

                (Rule: IsInvalid(chatSessionConfig.ModelName), Parameter: nameof(ChatSessionConfig.ModelName)));
        }

        private static void ValidateChatSessionConfigNotNull(ChatSessionConfig chatSessionConfig)
        {
            if (chatSessionConfig is null)
            {
                throw new NullChatSessionConfigException(message: "ChatSessionConfig is null.");
            }
        }

        private static dynamic IsInvalid(string? text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static void Validate<T>(
            Func<T> exceptionFactory,
            params (dynamic Rule, string Parameter)[] validations)
            where T : Xeption
        {
            T invalidDataException = exceptionFactory();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidDataException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidDataException.ThrowIfContainsErrors();
        }
    }
}

// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Standard.AI.PeerLLM.Models.Foundations.Chats.Exceptions;
using Xeptions;

namespace Standard.AI.PeerLLM.Services.Foundations.Chats
{
    internal partial class ChatService : IChatService
    {
        private delegate ValueTask<Guid> ReturningGuidFunction();

        private async ValueTask<Guid> TryCatch(ReturningGuidFunction returningGuidFunction)
        {
            try
            {
                return await returningGuidFunction();
            }
            catch (NullChatSessionConfigException nullChatSessionConfigException)
            {
                throw await CreateValidationExceptionAsync(nullChatSessionConfigException);
            }
            catch (InvalidChatSessionConfigException invalidChatSessionConfigException)
            {
                throw await CreateValidationExceptionAsync(invalidChatSessionConfigException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var hostNotFoundException = new HostNotFoundChatException(
                    message: "No hosts available for this model",
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

                throw CreateDependencyValidationException(hostNotFoundException);
            }
        }

        private async ValueTask<ChatValidationException> CreateValidationExceptionAsync(Xeption exception)
        {
            var chatValidationException =
                new ChatValidationException(
                    message: "Chat validation error occurred, fix errors and try again.",
                    innerException: exception);

            return chatValidationException;
        }

        private ChatDependencyValidationException CreateDependencyValidationException(Xeption exception)
        {
            var chatDependencyValidationException =
                new ChatDependencyValidationException(
                    message: "Chat dependency validation error occurred, fix errors and try again.",
                    innerException: exception);

            return chatDependencyValidationException;
        }
    }
}

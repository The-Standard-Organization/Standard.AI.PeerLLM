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
            catch (Exception exception)
            {
                var failedChatServiceException =
                    new FailedChatServiceException(
                        message: "Failed chat service exception occurred, please contact support for assistance.",
                        innerException: exception,
                        data: exception.Data);

                throw CreateServiceException(failedChatServiceException);
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

        private ChatServiceException CreateServiceException(Xeption exception)
        {
            var chatServiceException =
                new ChatServiceException(
                    message: "Chat service error occurred, please contact support.",
                    innerException: exception);

            return chatServiceException;
        }
    }
}

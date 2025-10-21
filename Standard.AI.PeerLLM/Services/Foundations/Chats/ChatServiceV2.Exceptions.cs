// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Standard.AI.PeerLLM.Models.Foundations.Chats.Exceptions;
using Xeptions;

namespace Standard.AI.PeerLLM.Services.Foundations.Chats
{
    internal partial class ChatServiceV2 : IChatServiceV2
    {
        private delegate ValueTask<Guid> ReturningGuidFunction();
        private delegate ValueTask<string> ReturningStringFunction();
        private delegate IAsyncEnumerable<string> ReturninStringEnumerableFunction();

        private async ValueTask<Guid> TryCatch(ReturningGuidFunction returningGuidFunction)
        {
            try
            {
                return await returningGuidFunction();
            }
            catch (NullChatSessionConfigException nullChatSessionConfigException)
            {
                throw CreateValidationException(nullChatSessionConfigException);
            }
            catch (InvalidChatSessionConfigException invalidChatSessionConfigException)
            {
                throw CreateValidationException(invalidChatSessionConfigException);
            }
            catch (HttpRequestException httpRequestException)
                            when (httpRequestException.StatusCode == HttpStatusCode.BadRequest)
            {
                var hostNotFoundException = new HostNotFoundChatException(
                    message: httpRequestException.Message,
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

                throw CreateDependencyValidationException(hostNotFoundException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.NotFound)
            {
                var conversationNotFoundChatException = new ConversationNotFoundChatException(
                    message: httpRequestException.Message,
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

                throw CreateDependencyValidationException(conversationNotFoundChatException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.TooManyRequests)
            {
                var tooManyRequestsChatException = new TooManyRequestsChatException(
                    message: httpRequestException.Message,
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

                throw CreateDependencyValidationException(tooManyRequestsChatException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.Forbidden)
            {
                var forbiddenChatException = new ForbiddenChatException(
                    message: httpRequestException.Message,
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

                throw CreateDependencyValidationException(forbiddenChatException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.Unauthorized)
            {
                var unauthorizedChatException = new UnauthorizedChatException(
                    message: httpRequestException.Message,
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

                throw CreateDependencyValidationException(unauthorizedChatException);
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

        private IAsyncEnumerable<string> TryCatch(
            ReturninStringEnumerableFunction returninStringEnumerableFunction)
        {
            try
            {
                return returninStringEnumerableFunction();
            }
            catch (InvalidArgumentsChatException invalidArgumentsChatException)
            {
                throw CreateValidationException(invalidArgumentsChatException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.BadRequest)
            {
                var hostNotFoundException = new HostNotFoundChatException(
                    message: httpRequestException.Message,
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

                throw CreateDependencyValidationException(hostNotFoundException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.NotFound)
            {
                var conversationNotFoundChatException = new ConversationNotFoundChatException(
                    message: httpRequestException.Message,
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

                throw CreateDependencyValidationException(conversationNotFoundChatException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.TooManyRequests)
            {
                var tooManyRequestsChatException = new TooManyRequestsChatException(
                    message: httpRequestException.Message,
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

                throw CreateDependencyValidationException(tooManyRequestsChatException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.Forbidden)
            {
                var forbiddenChatException = new ForbiddenChatException(
                    message: httpRequestException.Message,
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

                throw CreateDependencyValidationException(forbiddenChatException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.Unauthorized)
            {
                var unauthorizedChatException = new UnauthorizedChatException(
                    message: httpRequestException.Message,
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

                throw CreateDependencyValidationException(unauthorizedChatException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var externalChatException = new ExternalChatException(
                    message: "External validation error",
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

                throw CreateDependencyValidationException(externalChatException);
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

        private async ValueTask<string> TryCatch(ReturningStringFunction returningStringFunction)
        {
            try
            {
                return await returningStringFunction();
            }
            catch (InvalidArgumentsChatException invalidArgumentsChatException)
            {
                throw CreateValidationException(invalidArgumentsChatException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.BadRequest)
            {
                var hostNotFoundException = new HostNotFoundChatException(
                    message: httpRequestException.Message,
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

                throw CreateDependencyValidationException(hostNotFoundException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.NotFound)
            {
                var conversationNotFoundChatException = new ConversationNotFoundChatException(
                    message: httpRequestException.Message,
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

                throw CreateDependencyValidationException(conversationNotFoundChatException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.TooManyRequests)
            {
                var tooManyRequestsChatException = new TooManyRequestsChatException(
                    message: httpRequestException.Message,
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

                throw CreateDependencyValidationException(tooManyRequestsChatException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.Forbidden)
            {
                var forbiddenChatException = new ForbiddenChatException(
                    message: httpRequestException.Message,
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

                throw CreateDependencyValidationException(forbiddenChatException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.Unauthorized)
            {
                var unauthorizedChatException = new UnauthorizedChatException(
                    message: httpRequestException.Message,
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

                throw CreateDependencyValidationException(unauthorizedChatException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var externalChatException = new ExternalChatException(
                    message: "External validation error",
                    innerException: httpRequestException,
                    data: httpRequestException.Data);

                throw CreateDependencyValidationException(externalChatException);
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

        private ChatValidationException CreateValidationException(Xeption exception)
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

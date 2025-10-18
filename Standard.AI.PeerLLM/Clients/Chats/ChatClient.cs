// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Standard.AI.PeerLLM.Models.Clients.Chats.Exceptions;
using Standard.AI.PeerLLM.Models.Foundations.Chats;
using Standard.AI.PeerLLM.Models.Foundations.Chats.Exceptions;
using Standard.AI.PeerLLM.Services.Foundations.Chats;
using Xeptions;

namespace Standard.AI.PeerLLM.Clients.Chats
{
    internal class ChatClient : IChatClient
    {
        private readonly IChatService chatService;

        public ChatClient(IChatService chatService) =>
            this.chatService = chatService;

        public async ValueTask<Guid> StartChatAsync(
            ChatSessionConfig chatSessionConfig,
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.chatService.StartChatAsync(chatSessionConfig, cancellationToken);
            }
            catch (ChatValidationException chatValidationException)
            {
                throw CreateChatClientValidationException(
                    chatValidationException.InnerException as Xeption);
            }
            catch (ChatDependencyValidationException chatDependencyValidationException)
            {
                throw CreateChatClientValidationException(
                    chatDependencyValidationException.InnerException as Xeption);
            }
            catch (ChatDependencyException chatDependencyException)
            {
                throw CreateChatClientDependencyException(
                    chatDependencyException.InnerException as Xeption);
            }
            catch (ChatServiceException chatServiceException)
            {
                throw CreateChatClientDependencyException(
                    chatServiceException.InnerException as Xeption);
            }
            catch (Exception exception)
            {
                var failedChatClientServiceException =
                    new FailedChatClientServiceException(
                        message: "Failed chat client service error occurred, contact support.",
                        innerException: exception,
                        data: exception.Data);

                throw CreateChatClientServiceException(failedChatClientServiceException);
            }
        }

        public IAsyncEnumerable<string> StreamChatAsync(
            Guid conversationId,
            string text,
            CancellationToken cancellationToken = default)
        {
            try
            {
                return this.chatService.StreamChatAsync(
                    conversationId,
                    text,
                    cancellationToken);
            }
            catch (ChatValidationException chatValidationException)
            {
                throw CreateChatClientValidationException(
                    chatValidationException.InnerException as Xeption);
            }
            catch (ChatDependencyValidationException chatDependencyValidationException)
            {
                throw CreateChatClientValidationException(
                    chatDependencyValidationException.InnerException as Xeption);
            }
            catch (ChatDependencyException chatDependencyException)
            {
                throw CreateChatClientDependencyException(
                    chatDependencyException.InnerException as Xeption);
            }
            catch (ChatServiceException chatServiceException)
            {
                throw CreateChatClientDependencyException(
                    chatServiceException.InnerException as Xeption);
            }
        }

        private static ChatClientValidationException CreateChatClientValidationException(
            Xeption innerException)
        {
            return new ChatClientValidationException(
                message: "Chat client validation error occurred, fix errors and try again.",
                innerException,
                data: innerException.Data);
        }

        private static ChatClientDependencyException CreateChatClientDependencyException(
            Xeption innerException)
        {
            return new ChatClientDependencyException(
                message: "Chat client dependency error occurred, contact support.",
                innerException,
                data: innerException.Data);
        }

        private static ChatClientServiceException CreateChatClientServiceException(
            Xeption innerException)
        {
            return new ChatClientServiceException(
                message: "Chat client service error occurred, contact support.",
                innerException,
                data: innerException.Data);
        }
    }
}

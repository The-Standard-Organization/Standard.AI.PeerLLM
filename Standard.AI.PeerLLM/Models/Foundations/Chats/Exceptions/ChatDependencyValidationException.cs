// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.PeerLLM.Models.Foundations.Chats.Exceptions
{
    public class ChatDependencyValidationException : Xeption
    {
        public ChatDependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
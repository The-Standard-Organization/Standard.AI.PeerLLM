// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.PeerLLM.Models.Foundations.Chats.Exceptions
{
    public class ChatDependencyException : Xeption
    {
        public ChatDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.PeerLLM.Models.Foundations.Chats.Exceptions
{
    internal class InvalidChatSessionConfigException : Xeption
    {
        public InvalidChatSessionConfigException(string message)
            : base(message)
        { }
    }
}
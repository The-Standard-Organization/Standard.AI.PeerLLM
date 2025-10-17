// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.PeerLLM.Models.Foundations.Chats.Exceptions
{
    internal class InvalidArgumentsChatException : Xeption
    {
        public InvalidArgumentsChatException(string message)
            : base(message)
        { }
    }
}
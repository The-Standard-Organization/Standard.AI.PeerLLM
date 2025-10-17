// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.PeerLLM.Models.Foundations.Chats.Exceptions
{
    public class NullChatSessionConfigException : Xeption
    {
        public NullChatSessionConfigException(string message)
            : base(message)
        { }
    }
}
// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.PeerLLM.Models.Foundations.Chats.Exceptions
{
    internal class ChatServiceException : Xeption
    {
        public ChatServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
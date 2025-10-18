// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace Standard.AI.PeerLLM.Models.Clients.Chats.Exceptions
{
    public class FailedChatClientServiceException : Xeption
    {
        public FailedChatClientServiceException(string message, Exception innerException, IDictionary data)
            : base(message, innerException, data)
        { }
    }
}
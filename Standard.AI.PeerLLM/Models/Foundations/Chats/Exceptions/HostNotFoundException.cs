// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace Standard.AI.PeerLLM.Models.Foundations.Chats.Exceptions
{
    public class HostNotFoundException : Xeption
    {
        public HostNotFoundException(string message, Exception innerException, IDictionary data)
            : base(message, innerException, data)
        { }
    }
}
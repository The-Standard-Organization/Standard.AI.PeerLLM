// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Collections;
using Xeptions;

namespace Standard.AI.PeerLLM.Models.Clients.Chats.Exceptions
{
    public class ChatClientDependencyException : Xeption
    {
        public ChatClientDependencyException(string message, Xeption innerException, IDictionary data)
            : base(message, innerException, data)
        { }
    }
}
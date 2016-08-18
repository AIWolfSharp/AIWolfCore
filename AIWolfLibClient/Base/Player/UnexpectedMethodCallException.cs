using AIWolf.Common;
using System;
using System.Runtime.Serialization;

namespace AIWolf.Client.Base.Player
{
    class UnexpectedMethodCallException : AIWolfRuntimeException
    {
        public UnexpectedMethodCallException()
        {
        }

        public UnexpectedMethodCallException(string message) : base(message)
        {
        }

        public UnexpectedMethodCallException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
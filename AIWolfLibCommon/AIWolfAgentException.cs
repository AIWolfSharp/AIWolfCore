﻿//
// AIWolfAgentException.cs
//
// Copyright (c) 2016 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

using System;
using AIWolf.Common.Data;

namespace AIWolf.Common
{
    /// <summary>
    /// Exception that occurs during execution of AIWolf agent.
    /// </summary>
    public class AIWolfAgentException : AIWolfRuntimeException
    {
        private Agent agent;
        private Exception exception;
        private string method;

        /// <summary>
        /// Initializes a new instance of the AIWolfAgentException class.
        /// </summary>
        public AIWolfAgentException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the AIWolfAgentException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public AIWolfAgentException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AIWolfAgentException class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception,
        /// or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public AIWolfAgentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AIWolfAgentException class with a agent and a method in which this exception occurs,
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="agent">The agent in which this exception occurred.</param>
        /// <param name="method">The name of the method in which this exception occurred.</param>
        /// <param name="innerException">The exception that is the cause of the current exception,
        /// or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public AIWolfAgentException(Agent agent, string method, Exception innerException)
        {
            this.agent = agent;
            this.method = method;
            exception = innerException;
        }
    }
}
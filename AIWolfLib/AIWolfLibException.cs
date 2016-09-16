//
// AIWolfLibException.cs
//
// Copyright (c) 2016 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

using System;

namespace AIWolf.Lib
{
    /// <summary>
    /// Exception that occurs in AIWolf library.
    /// </summary>
    class AIWolfLibException : Exception
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public AIWolfLibException()
        {
        }

        /// <summary>
        /// Initializes a new instance of this class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public AIWolfLibException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of this class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception,
        /// or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public AIWolfLibException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

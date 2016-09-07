//
// Error.cs
//
// Copyright (c) 2016 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

using System;
using System.Diagnostics;

namespace AIWolf.Lib
{
    /// <summary>
    /// Error handling class.
    /// </summary>
    public static class Error
    {
        /// <summary>
        /// Writes an error message.
        /// </summary>
        /// <param name="message">Error message.</param>
        public static void Warning(string message)
        {
            Console.Error.WriteLine("(WARNING) " + message);
        }

        /// <summary>
        /// Writes an error message, then throws AIWolfRuntimeException on debug.
        /// </summary>
        /// <param name="message">Error message.</param>
        public static void RuntimeError(params string[] messages)
        {
            string message = "";
            if (messages.Length > 0)
            {
                message = messages[0];
            }
            ThrowRuntimeException(message);
            foreach (var m in messages)
            {
                Console.Error.WriteLine("(ERROR) " + m);
            }
        }

        [Conditional("DEBUG")]
        static void ThrowRuntimeException(string message)
        {
            throw new AIWolfException(message);
        }

        /// <summary>
        /// Writes an error message, then throws TimeoutException on debug.
        /// </summary>
        /// <param name="message">Error message.</param>
        public static void TimeoutError(params string[] messages)
        {
            string message = "";
            if (messages.Length > 0)
            {
                message = messages[0];
            }
            ThrowTimeoutException(message);
            foreach (var m in messages)
            {
                Console.Error.WriteLine("(TIMEOUT) " + m);
            }
        }

        [Conditional("DEBUG")]
        static void ThrowTimeoutException(string message)
        {
            throw new TimeoutException(message);
        }
    }
}

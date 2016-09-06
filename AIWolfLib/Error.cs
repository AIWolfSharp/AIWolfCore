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
        /// Writes an error message, then throws exception on debug.
        /// </summary>
        /// <param name="message">Error message.</param>
        public static void RuntimeError(params string[] messages)
        {
            string message = "";
            if (messages.Length > 0)
            {
                message = messages[0];
            }
            ThrowException(message);
            foreach (var m in messages)
            {
                Console.Error.WriteLine(m);
            }
        }

        [Conditional("DEBUG")]
        static void ThrowException(string message)
        {
            throw new AIWolfRuntimeException(message);
        }
    }
}

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
using System.IO;
using System.Runtime.CompilerServices;

namespace AIWolf.Lib
{
    /// <summary>
    /// Error handling class.
    /// </summary>
    public static class Error
    {
        /// <summary>
        /// Writes a warning message.
        /// </summary>
        /// <param name="message">Warning message.</param>
        public static void Warning(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            Console.Error.WriteLine(memberName + " : " + message + " at line " + lineNumber + " in " + Path.GetFileName(filePath));
        }

        /// <summary>
        /// Writes an error message, then throws AIWolfRuntimeException on debug.
        /// </summary>
        /// <param name="message">Error message.</param>
        public static void RuntimeError(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            ThrowRuntimeException(message);
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
        public static void TimeoutError(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            ThrowTimeoutException(message);
        }

        [Conditional("DEBUG")]
        static void ThrowTimeoutException(string message)
        {
            throw new TimeoutException(message);
        }
    }
}

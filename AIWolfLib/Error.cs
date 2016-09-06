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
        public static void RuntimeError(string message)
        {
            Console.Error.WriteLine(message);
            ThrowException(message);
        }

        [Conditional("DEBUG")]
        static void ThrowException(string message)
        {
            throw new AIWolfRuntimeException(message);
        }
    }
}

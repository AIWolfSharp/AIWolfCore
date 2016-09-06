using System;
using System.Diagnostics;

namespace AIWolf.Lib
{
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
        public static void ThrowException(string message)
        {
            throw new AIWolfRuntimeException(message);
        }
    }
}

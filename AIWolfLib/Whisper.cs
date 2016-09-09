//
// Whisper.cs
//
// Copyright (c) 2016 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

using Newtonsoft.Json;

namespace AIWolf.Lib
{
    public class Whisper : Talk
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="idx">The index of this talk/whisper.</param>
        /// <param name="day">The day of this talk/whisper.</param>
        public Whisper(int idx, int day) : base(idx, day)
        {
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="idx">The index of this talk/whisper.</param>
        /// <param name="day">The day of this talk/whisper.</param>
        /// <param name="agent">The agent who talked/whispered.</param>
        /// <param name="text">The text of this talk/whisper.</param>
        public Whisper(int idx, int day, Agent agent, string text) : base(idx, day, agent, text)
        {
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="idx">The index of this talk/whisper.</param>
        /// <param name="day">The day of this talk/whisper.</param>
        /// <param name="agent">The index of agent who talked/whispered.</param>
        /// <param name="text">The text of this talk/whisper.</param>
        [JsonConstructor]
        public Whisper(int idx, int day, int agent, string text) : base(idx, day, agent, text)
        {
        }
    }
}

//
// Talk.cs
//
// Copyright (c) 2016 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

using Newtonsoft.Json;

namespace AIWolf.Lib
{
    /// <summary>
    /// Talk class.
    /// </summary>
    public class Talk : Utterance
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="idx">The index of this talk.</param>
        /// <param name="day">The day of this talk.</param>
        internal Talk(int idx, int day) : base(idx, day)
        {
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="idx">The index of this talk.</param>
        /// <param name="day">The day of this talk.</param>
        /// <param name="agent">The agent who talked.</param>
        /// <param name="text">The text of this talk.</param>
        Talk(int idx, int day, Agent agent, string text) : base(idx, day, agent, text)
        {
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="idx">The index of this talk.</param>
        /// <param name="day">The day of this talk.</param>
        /// <param name="agent">The index of agent who talked.</param>
        /// <param name="text">The text of this talk.</param>
        [JsonConstructor]
        Talk(int idx, int day, int agent, string text) : base(idx, day, agent, text)
        {
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("Talk: Day{0:D2}[{1:D3}]\t{2}\t{3}\t{4}", Day, Idx, Agent, Text, Contents);
        }
    }
}

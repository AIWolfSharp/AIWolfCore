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
#if JHELP
    /// <summary>
    /// 囁きクラス
    /// </summary>
#else
    /// <summary>
    /// Whisper class.
    /// </summary>
#endif
    public class Whisper : Utterance
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="idx">The index of this whisper.</param>
        /// <param name="day">The day of this whisper.</param>
        internal Whisper(int idx, int day) : base(idx, day)
        {
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="idx">The index of this whisper.</param>
        /// <param name="day">The day of this whisper.</param>
        /// <param name="turn">The turn of this whisper.</param>
        /// <param name="agent">The agent who whispered.</param>
        /// <param name="text">The text of this whisper.</param>
        Whisper(int idx, int day, int turn, Agent agent, string text) : base(idx, day, turn, agent, text)
        {
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="idx">The index of this whisper.</param>
        /// <param name="day">The day of this whisper.</param>
        /// <param name="turn">The turn of this whisper.</param>
        /// <param name="agent">The index of agent who whispered.</param>
        /// <param name="text">The text of this whisper.</param>
        [JsonConstructor]
        Whisper(int idx, int day, int turn, int agent, string text) : base(idx, day, turn, agent, text)
        {
        }

#if JHELP
        /// <summary>
        /// このオブジェクトを表す文字列を返す
        /// </summary>
        /// <returns>このオブジェクトを表す文字列</returns>
#else
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
#endif
        public override string ToString()
        {
            return string.Format("Whisper: Day{0:D2} {1:D2}[{2:D3}]\t{3}\t{4}", Day, Turn, Idx, Agent, Text);
        }
    }
}

//
// Talk.cs
//
// Copyright (c) 2016 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace AIWolf.Lib
{
    /// <summary>
    /// Talk/whisper.
    /// </summary>
    [DataContract]
    public class Talk
    {
        /// <summary>
        /// There is nothing to talk/whisper.
        /// </summary>
        public static string Over { get; } = "Over";

        /// <summary>
        /// Skip this turn though there is something to talk/whisper.
        /// </summary>
        public static string Skip { get; } = "Skip";

        /// <summary>
        /// The index number of this talk/whisper.
        /// </summary>
        /// <value>The index number of this talk/whisper.</value>
        [DataMember(Name = "idx")]
        public int Idx { get; }

        /// <summary>
        /// The day of this talk/whisper.
        /// </summary>
        /// <value>The day of this talk/whisper.</value>
        [DataMember(Name = "day")]
        public int Day { get; }

        /// <summary>
        /// The agent who talked/whispered.
        /// </summary>
        /// <value>The agent who talked/whispered.</value>
        public Agent Agent { get; }

        /// <summary>
        /// The index number of the agent who talked/whispered.
        /// </summary>
        /// <value>The index number of the agent who talked/whispered.</value>
        [DataMember(Name = "agent")]
        public int _Agent { get; }

        /// <summary>
        /// The contents of this talk/whisper.
        /// </summary>
        /// <value>The contents of this talk/whisper.</value>
        [DataMember(Name = "content")]
        public string Content { get; }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="idx">The index of this talk/whisper.</param>
        /// <param name="day">The day of this talk/whisper.</param>
        /// <param name="agent">The agent who talked/whispered.</param>
        /// <param name="content">The contents of this talk/whisper.</param>
        public Talk(int idx, int day, Agent agent, string content)
        {
            if (idx < 0)
            {
                throw new AIWolfRuntimeException(GetType() + ": Invalid idx " + idx + ".");
            }
            if (day < 0)
            {
                throw new AIWolfRuntimeException(GetType() + ": Invalid day " + day + ".");
            }
            if (agent == null)
            {
                throw new AIWolfRuntimeException(GetType() + ": Agent is null.");
            }
            if (content == null)
            {
                throw new AIWolfRuntimeException(GetType() + ": Content is null.");
            }
            if (content.Length == 0)
            {
                throw new AIWolfRuntimeException(GetType() + ": Content is empty.");
            }
            Idx = idx;
            Day = day;
            Agent = agent;
            _Agent = Agent.AgentIdx;
            Content = content;
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="idx">The index of this talk/whisper.</param>
        /// <param name="day">The day of this talk/whisper.</param>
        /// <param name="agent">The agent who talked/whispered.</param>
        /// <param name="content">The contents of this talk/whisper.</param>
        [JsonConstructor]
        public Talk(int idx, int day, int agent, string content)
        {
            if (idx < 0)
            {
                throw new AIWolfRuntimeException(GetType() + ": Invalid idx " + idx + ".");
            }
            if (day < 0)
            {
                throw new AIWolfRuntimeException(GetType() + ": Invalid day " + day + ".");
            }
            if (agent < 1)
            {
                throw new AIWolfRuntimeException(GetType() + ": Invalid agent index " + agent + ".");
            }
            if (content == null)
            {
                throw new AIWolfRuntimeException(GetType() + ": Content is null.");
            }
            if (content.Length == 0)
            {
                throw new AIWolfRuntimeException(GetType() + ": Content is empty.");
            }
            Idx = idx;
            Day = day;
            _Agent = agent;
            Agent = Agent.GetAgent(_Agent);
            Content = content;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return String.Format("Day{0:D2}[{1:D3}]\t{2}\t{3}", Day, Idx, Agent, Content);
        }
    }
}

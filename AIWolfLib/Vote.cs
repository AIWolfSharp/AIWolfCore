//
// Vote.cs
//
// Copyright (c) 2016 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace AIWolf.Lib
{
    /// <summary>
    /// Information of vote for execution.
    /// </summary>
    [DataContract]
    public class Vote
    {
        /// <summary>
        /// The day of this vote.
        /// </summary>
        [DataMember(Name = "day")]
        public int Day { get; }

        /// <summary>
        /// The agent who voted.
        /// </summary>
        public Agent Agent { get; }

        /// <summary>
        /// The index number of the agent who voted.
        /// </summary>
        [DataMember(Name = "agent")]
        public int _Agent { get; }

        /// <summary>
        /// The voted agent.
        /// </summary>
        public Agent Target { get; }

        /// <summary>
        /// The index number of the voted agent.
        /// </summary>
        [DataMember(Name = "target")]
        public int _Target { get; }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="day">The day of this vote.</param>
        /// <param name="agent">The index of agent who voted.</param>
        /// <param name="target">The index of voted agent.</param>
        [JsonConstructor]
        public Vote(int day, int agent, int target)
        {
            Day = day;
            if (Day < 0)
            {
                Error.RuntimeError(GetType() + "(): Invalid day " + Day + ".", "Force it to be 0.");
                Day = 0;
            }

            Agent = Agent.GetAgent(agent);
            if (Agent == null)
            {
                Error.RuntimeError(GetType() + "(): Agent must not be null.", "Force it to be Agent[00].");
                Agent = Agent.GetAgent(0);
            }
            _Agent = Agent.AgentIdx;

            Target = Agent.GetAgent(target);
            if (Target == null)
            {
                Error.RuntimeError(GetType() + "(): Target must not be null.", "Force it to be Agent[00].");
                Target = Agent.GetAgent(0);
            }
            _Target = Target.AgentIdx;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Agent + "voted" + Target + "@" + Day;
        }
    }
}

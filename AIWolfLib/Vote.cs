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
        /// <value>The day of this vote.</value>
        [DataMember(Name = "day")]
        public int Day { get; }

        /// <summary>
        /// The agent who voted.
        /// </summary>
        /// <value>The agent who voted.</value>
        public Agent Agent { get; }

        /// <summary>
        /// The index number of the agent who voted.
        /// </summary>
        /// <value>The index number of the agent who voted.</value>
        [DataMember(Name = "agent")]
        public int _Agent { get; }

        /// <summary>
        /// The voted agent.
        /// </summary>
        /// <value>The voted agent.</value>
        /// <remarks></remarks>
        public Agent Target { get; }

        /// <summary>
        /// The index number of the voted agent.
        /// </summary>
        /// <value>The index number of the voted agent.</value>
        [DataMember(Name = "target")]
        public int _Target { get; }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="day">The day of this vote.</param>
        /// <param name="agent">The agent who voted.</param>
        /// <param name="target">The voted agent.</param>
        public Vote(int day, Agent agent, Agent target)
        {
            if (day < 0)
            {
                throw new AIWolfRuntimeException(GetType() + ": Invalid day " + day + ".");
            }
            if (agent == null)
            {
                throw new AIWolfRuntimeException(GetType() + ": Agent is null.");
            }
            if (target == null)
            {
                throw new AIWolfRuntimeException(GetType() + ": Target is null.");
            }
            Day = day;
            Agent = agent;
            _Agent = Agent.AgentIdx;
            Target = target;
            _Target = Target.AgentIdx;
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="day">The day of this vote.</param>
        /// <param name="agent">The index of agent who voted.</param>
        /// <param name="target">The index of voted agent.</param>
        [JsonConstructor]
        public Vote(int day, int agent, int target)
        {
            if (day < 0)
            {
                throw new AIWolfRuntimeException(GetType() + ": Invalid day " + day + ".");
            }
            if (agent < 1)
            {
                throw new AIWolfRuntimeException(GetType() + ": Invalid agent index " + agent + ".");
            }
            if (target < 1)
            {
                throw new AIWolfRuntimeException(GetType() + ": Invalid target index " + target + ".");
            }
            Day = day;
            _Agent = agent;
            Agent = Agent.GetAgent(_Agent);
            _Target = target;
            Target = Agent.GetAgent(_Target);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Agent + "voted " + Target + "@" + Day;
        }
    }
}

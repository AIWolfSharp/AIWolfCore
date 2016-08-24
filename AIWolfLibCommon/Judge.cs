//
// Judge.cs
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
    /// The judge whether the player is human or werewolf.
    /// </summary>
    [DataContract]
    public class Judge
    {
        /// <summary>
        /// The day of this judge.
        /// </summary>
        /// <value>The day of this judge.</value>
        [DataMember(Name = "day")]
        public int Day { get; }

        /// <summary>
        /// The agent who judged.
        /// </summary>
        /// <value>The agent who judged.</value>
        public Agent Agent { get; }

        /// <summary>
        /// The index number of the agent who judged.
        /// </summary>
        /// <value>The index number of the agent who judged.</value>
        [DataMember(Name = "agent")]
        public int _Agent { get; }

        /// <summary>
        /// The judged agent.
        /// </summary>
        /// <value>The judged agent.</value>
        public Agent Target { get; }

        /// <summary>
        /// The index nunmber of the judged agent.
        /// </summary>
        /// <value>The index number of the judged agent.</value>
        [DataMember(Name = "target")]
        public int _Target { get; }


        /// <summary>
        /// The result of this judge.
        /// </summary>
        /// <value>Whether the judged agent is human or werewolf.</value>
        public Species Result { get; }

        /// <summary>
        /// The result of this judge in string.
        /// </summary>
        /// <value>"HUMAN" or "WEREWOLF".</value>
        [DataMember(Name = "result")]
        public string _Result { get; }

        /// <summary>
        /// Initializes a new instance of Judge class.
        /// </summary>
        /// <param name="day">The day of this judge.</param>
        /// <param name="agent">The agent who judged.</param>
        /// <param name="target">The judged agent.</param>
        /// <param name="result">The result of this judge.</param>
        public Judge(int day, Agent agent, Agent target, Species result)
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
            Result = result;
            _Result = Result.ToString();
        }

        /// <summary>
        /// Initializes a new instance of Judge class.
        /// </summary>
        /// <param name="day">The day of this judge.</param>
        /// <param name="agent">The index of agent who judged.</param>
        /// <param name="target">The index of judged agent.</param>
        /// <param name="result">The result of this judge.</param>
        [JsonConstructor]
        public Judge(int day, int agent, int target, string result)
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
            _Result = result;
            Result = (Species)Enum.Parse(typeof(Species), _Result);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Agent + "->" + Target + "@" + Day + ":" + Result;
        }
    }
}

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
        [DataMember(Name = "day")]
        public int Day { get; }

        /// <summary>
        /// The agent who judged.
        /// </summary>
        public Agent Agent { get; }

        /// <summary>
        /// The index number of the agent who judged.
        /// </summary>
        [DataMember(Name = "agent")]
        public int _Agent { get; }

        /// <summary>
        /// The judged agent.
        /// </summary>
        public Agent Target { get; }

        /// <summary>
        /// The index nunmber of the judged agent.
        /// </summary>
        [DataMember(Name = "target")]
        public int _Target { get; }


        /// <summary>
        /// The result of this judge.
        /// </summary>
        public Species Result { get; }

        /// <summary>
        /// The result of this judge in string.
        /// </summary>
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
            Day = day;
            if (day < 0)
            {
                Error.RuntimeError(GetType() + "(): Invalid day " + day + ".", "Force it to be 0.");
                Day = 0;
            }

            Agent = agent;
            if (agent == null)
            {
                Error.RuntimeError(GetType() + "(): Agent is null.", "Force it to be Agent[00].");
                Agent = Agent.GetAgent(0);
            }
            _Agent = Agent.AgentIdx;

            Target = target;
            if (target == null)
            {
                Error.RuntimeError(GetType() + "(): Target is null.", "Force it to be Agent[00].");
                Target = Agent.GetAgent(0);
            }
            _Target = Target.AgentIdx;

            Result = result;
            if (result == Species.UNC)
            {
                Error.RuntimeError(GetType() + "(): Species.UNC is not allowed as result.", "Force it to be Species.HUMAN.");
                Result = Species.HUMAN;
            }
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
            Day = day;
            if (day < 0)
            {
                Error.RuntimeError(GetType() + "(): Invalid day " + day + ".", "Force it to be 0.");
                Day = 0;
            }

            _Agent = agent;
            if (agent < 1)
            {
                Error.RuntimeError(GetType() + "(): Invalid agent index " + agent + ".", "Force it to be 0.");
                _Agent = 0;
            }
            Agent = Agent.GetAgent(_Agent);

            _Target = target;
            if (target < 1)
            {
                Error.RuntimeError(GetType() + "(): Invalid target index " + target + ".", "Force it to be 0.");
                _Target = 0;
            }
            Target = Agent.GetAgent(_Target);

            _Result = result;
            Species r;
            if (!Enum.TryParse(result, out r) || r == Species.UNC)
            {
                Error.RuntimeError(GetType() + "(): Invalid result string " + result + ".", "Force it to be HUMAN.");
                _Result = "HUMAN";
                r = Species.HUMAN;
            }
            Result = r;
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

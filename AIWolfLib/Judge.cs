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
        int _Agent { get; }

        /// <summary>
        /// The judged agent.
        /// </summary>
        public Agent Target { get; }

        /// <summary>
        /// The index nunmber of the judged agent.
        /// </summary>
        [DataMember(Name = "target")]
        int _Target { get; }


        /// <summary>
        /// The result of this judge.
        /// </summary>
        public Species Result { get; }

        /// <summary>
        /// The result of this judge in string.
        /// </summary>
        [DataMember(Name = "result")]
        string _Result { get; }

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
            if (Day < 0)
            {
                Error.RuntimeError("Invalid day " + Day + ".");
                Day = 0;
                Error.Warning("Force it to be " + Day + ".");
            }

            Agent = agent;
            if (Agent == null)
            {
                Error.RuntimeError("Agent must not be null.");
                Agent = Agent.GetAgent(0);
                Error.Warning("Force it to be " + Agent + ".");
            }
            _Agent = Agent.AgentIdx;

            Target = target;
            if (Target == null)
            {
                Error.RuntimeError("Target must not be null.");
                Target = Agent.GetAgent(0);
                Error.Warning("Force it to be " + Target + ".");
            }
            _Target = Target.AgentIdx;

            Result = result;
            if (Result == Species.UNC)
            {
                Error.RuntimeError("Invalid result " + Result + ".");
                Result = Species.HUMAN;
                Error.Warning("Force it to be " + Result + ".");
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
        Judge(int day, int agent, int target, string result) : this(day, Agent.GetAgent(agent), Agent.GetAgent(target), Species.HUMAN)
        {
            Species r;
            if (!Enum.TryParse(result, out r) || r == Species.UNC)
            {
                Error.RuntimeError("Invalid result string " + result + ".");
                r = Species.HUMAN;
                Error.Warning("Force it to be " + r + ".");
            }
            Result = r;
            _Result = r.ToString();
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

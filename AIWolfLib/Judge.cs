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
        /// <param name="agent">The index of agent who judged.</param>
        /// <param name="target">The index of judged agent.</param>
        /// <param name="result">The result of this judge.</param>
        [JsonConstructor]
        public Judge(int day, int agent, int target, string result)
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

            Species r;
            if (!Enum.TryParse(result, out r) || r == Species.UNC)
            {
                Error.RuntimeError(GetType() + "(): Invalid result string " + result + ".", "Force it to be HUMAN.");
                r = Species.HUMAN;
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

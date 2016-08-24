//
// Agent.cs
//
// Copyright (c) 2016 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AIWolf.Lib
{
    /// <summary>
    /// Agent class.
    /// </summary>
    [DataContract]
    public sealed class Agent : IComparable<Agent>
    {
        static Dictionary<int, Agent> agentMap = new Dictionary<int, Agent>();

        /// <summary>
        /// Returns the agent of given index.
        /// </summary>
        /// <param name="idx">Agent's index number.</param>
        /// <returns>The agent of given index number.</returns>
        public static Agent GetAgent(int idx)
        {
            if (idx < 0)
            {
                throw new AIWolfRuntimeException("Agent.GetAgent: Invalid index " + idx + ".");
            }
            if (!agentMap.ContainsKey(idx))
            {
                agentMap[idx] = new Agent(idx);
            }
            return agentMap[idx];
        }

        /// <summary>
        /// The index number of this agent.
        /// </summary>
        /// <value>The index number of this agent.</value>
        [DataMember(Name = "agentIdx")]
        public int AgentIdx { get; }

        /// <summary>
        /// Initializes a new instance of Agent class with given index number.
        /// </summary>
        /// <param name="idx">The index number of this agent.</param>
        Agent(int idx)
        {
            AgentIdx = idx;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("Agent[{0:00}]", AgentIdx);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            result = prime * result + AgentIdx;
            return result;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null)
            {
                return false;
            }
            if (GetType() != obj.GetType())
            {
                return false;
            }
            Agent other = (Agent)obj;
            if (AgentIdx != other.AgentIdx)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates
        /// whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="target">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        /// <remarks>
        /// Less than zero : This instance precedes obj in the sort order.
        /// Zero : This instance occurs in the same position in the sort order as obj.
        /// Greater than zero : This instance follows obj in the sort order.
        /// </remarks>
        public int CompareTo(Agent target)
        {
            if (target == null)
            {
                return 1;
            }
            return AgentIdx - target.AgentIdx;
        }
    }
}

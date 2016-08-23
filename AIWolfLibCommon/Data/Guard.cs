﻿//
// Guard.cs
//
// Copyright (c) 2016 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

using System.Runtime.Serialization;

namespace AIWolf.Common.Data
{
    /// <summary>
    /// Guard class.
    /// </summary>
    /// <remarks></remarks>
    [DataContract]
    public class Guard
    {
        /// <summary>
        /// The day of the guard.
        /// </summary>
        /// <value>The day of the guard.</value>
        [DataMember(Name = "day")]
        public int Day { get; }

        /// <summary>
        /// The agent of the bodyguard.
        /// </summary>
        /// <value>The agent of the bodyguard.</value>
        [DataMember(Name = "agent")]
        public Agent Agent { get; }

        /// <summary>
        /// The agent guarded by the bodyguard.
        /// </summary>
        /// <value>The agent guarded.</value>
        [DataMember(Name = "target")]
        public Agent Target { get; }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="day">The day of guard.</param>
        /// <param name="agent">The agent of the bodyguard.</param>
        /// <param name="target">The agent guarded by the bodyguard.</param>
        public Guard(int day, Agent agent, Agent target)
        {
            Day = day;
            Agent = agent;
            Target = target;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        /// <remarks></remarks>
        public override string ToString()
        {
            return Agent + " guarded " + Target + "@" + Day;
        }
    }
}
//
// GameSetting.cs
//
// Copyright (c) 2016 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace AIWolf.Lib
{
    /// <summary>
    /// Settings of game.
    /// </summary>
    [DataContract]
    public class GameSetting
    {
        /// <summary>
        /// The number of each role.
        /// </summary>
        [DataMember(Name = "roleNumMap")]
        public Dictionary<Role, int> RoleNumMap { get; } = new Dictionary<Role, int>();

        /// <summary>
        /// The maximum number of talks.
        /// </summary>
        [DataMember(Name = "maxTalk")]
        public int MaxTalk { get; }

        /// <summary>
        /// Whether or not the game permit to attack no one.
        /// </summary>
        [DataMember(Name = "enableNoAttack")]
        public bool EnableNoAttack { get; }

        /// <summary>
        /// Whether or not agent can see who vote to who.
        /// </summary>
        [DataMember(Name = "voteVisible")]
        public bool VoteVisible { get; }

        /// <summary>
        /// Whether or not there is vote in the first day.
        /// </summary>
        [DataMember(Name = "votableInFirstDay")]
        public bool VotableInFirstDay { get; }

        /// <summary>
        /// The random seed.
        /// </summary>
        [DataMember(Name = "randomSeed")]
        public long RandomSeed { get; }

        /// <summary>
        /// The number of players.
        /// </summary>
        [DataMember(Name = "playerNum")]
        public int PlayerNum
        {
            get
            {
                return RoleNumMap == null ? 0 : RoleNumMap.Values.Sum();
            }
        }
    }
}

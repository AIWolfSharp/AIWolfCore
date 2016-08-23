//
// GameSetting.cs
//
// Copyright (c) 2016 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

using AIWolf.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace AIWolf.Common.Net
{
    /// <summary>
    /// Settings of game.
    /// </summary>
    [DataContract]
    public class GameSetting
    {
        /// <summary>
        /// Number of each roles.
        /// </summary>
        /// <remarks>
        /// Order: bodyguard, freemason, medium, possessed, seer, villager, werewolf.
        /// </remarks>
        static readonly int[][] roleNumArray =
        {
            null,//0
            null,//1
            null,//2
            null,//3
            null,//4
            new[] {0, 0, 0, 1, 1, 2, 1 },//5
            new[] {0, 0, 0, 1, 1, 3, 1 },//6
            new[] {0, 0, 0, 0, 1, 4, 2 },//7
            new[] {0, 0, 1, 0, 1, 4, 2 },//8
            new[] {0, 0, 1, 0, 1, 5, 2 },//9
            new[] {1, 0, 1, 1, 1, 4, 2 },//10
            new[] {1, 0, 1, 1, 1, 5, 2 },//11
            new[] {1, 0, 1, 1, 1, 5, 3 },//12
            new[] {1, 0, 1, 1, 1, 6, 3 },//13
            new[] {1, 0, 1, 1, 1, 7, 3 },//14
            new[] {1, 0, 1, 1, 1, 8, 3 },//15
            new[] {1, 0, 1, 1, 1, 9, 3 },//16
            new[] {1, 0, 1, 1, 1, 10, 3 },//17
            new[] {1, 0, 1, 1, 1, 11, 3 },//18
        };

        /// <summary>
        /// Default setting of game.
        /// </summary>
        /// <param name="agentNum">The number of agents.</param>
        /// <returns>Default setting of game with given number of agents.</returns>
        public static GameSetting GetDefaultGame(int agentNum)
        {
            if (agentNum < 5 || agentNum > 18)
            {
                throw new AIWolfRuntimeException("GameSetting.GetDefaultGame: agentNum must be between 5 and 18.");
            }

            GameSetting setting = new GameSetting();
            setting.MaxTalk = 10;
            setting.EnableNoAttack = false;
            setting.VoteVisible = true;
            setting.VotableInFirstDay = false;

            Role[] roles = (Role[])Enum.GetValues(typeof(Role));
            for (int i = 0; i < roles.Length; i++)
            {
                setting.RoleNumMap[roles[i]] = roleNumArray[agentNum][i];
            }
            return setting;
        }

        /// <summary>
        /// The number of each role.
        /// </summary>
        /// <value>Dictionary storing the number of each role.</value>
        [DataMember(Name = "roleNumMap")]
        public Dictionary<Role, int> RoleNumMap { get; set; }

        /// <summary>
        /// The maximum number of talks.
        /// </summary>
        /// <value>The maximum number of talks.</value>
        [DataMember(Name = "maxTalk")]
        public int MaxTalk { get; set; }

        /// <summary>
        /// Whether or not the game permit to attack no one.
        /// </summary>
        /// <value>True if the game permit to attack no one, otherwise, false.</value>
        [DataMember(Name = "enableNoAttack")]
        public bool EnableNoAttack { get; set; }

        /// <summary>
        /// Whether or not agent can see who vote to who.
        /// </summary>
        /// <value>True if agent can see who vote to who, otherwise, false.</value>
        [DataMember(Name = "voteVisible")]
        public bool VoteVisible { get; set; }

        /// <summary>
        /// Whether or not there is vote in the first day.
        /// </summary>
        /// <value>True if there is vote in the first day, otherwise, false.</value>
        [DataMember(Name = "votableInFirstDay")]
        public bool VotableInFirstDay { get; private set; }

        /// <summary>
        /// The random seed.
        /// </summary>
        /// <value>The random seed.</value>
        [DataMember(Name = "randomSeed")]
        public long RandomSeed { get; set; } = Environment.TickCount;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public GameSetting()
        {
            RoleNumMap = new Dictionary<Role, int>();
        }

        /// <summary>
        /// The number of players.
        /// </summary>
        /// <value>The number of players.</value>
        [DataMember(Name = "playerNum")]
        public int PlayerNum
        {
            get
            {
                return RoleNumMap.Values.Sum();
            }
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of the current instance.</returns>
        public object Clone()
        {
            GameSetting gameSetting = new GameSetting();
            gameSetting.EnableNoAttack = EnableNoAttack;
            gameSetting.VotableInFirstDay = VotableInFirstDay;
            gameSetting.VoteVisible = VoteVisible;
            gameSetting.MaxTalk = MaxTalk;
            gameSetting.RandomSeed = RandomSeed;
            gameSetting.RoleNumMap = new Dictionary<Role, int>(RoleNumMap);
            return gameSetting;
        }
    }
}

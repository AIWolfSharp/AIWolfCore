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
#if JHELP
    /// <summary>
    /// ゲーム設定
    /// </summary>
#else
    /// <summary>
    /// Settings of game.
    /// </summary>
#endif
    [DataContract]
    public class GameSetting
    {
#if JHELP
        /// <summary>
        /// 各役職の人数
        /// </summary>
#else
        /// <summary>
        /// The number of each role.
        /// </summary>
#endif
        [DataMember(Name = "roleNumMap")]
        public Dictionary<Role, int> RoleNumMap { get; } = new Dictionary<Role, int>();

#if JHELP
        /// <summary>
        /// １会話フェーズで許される最大発話回数
        /// </summary>
#else
        /// <summary>
        /// The maximum number of talks.
        /// </summary>
#endif
        [DataMember(Name = "maxTalk")]
        public int MaxTalk { get; }

#if JHELP
        /// <summary>
        /// 誰も襲撃しないことを許すか否か
        /// </summary>
#else
        /// <summary>
        /// Whether or not the game permit to attack no one.
        /// </summary>
#endif
        [DataMember(Name = "enableNoAttack")]
        public bool EnableNoAttack { get; }

#if JHELP
        /// <summary>
        /// 誰が誰に投票したかわかるか否か
        /// </summary>
#else
        /// <summary>
        /// Whether or not agent can see who vote to who.
        /// </summary>
#endif
        [DataMember(Name = "voteVisible")]
        public bool VoteVisible { get; }

#if JHELP
        /// <summary>
        /// 初日に投票があるか否か
        /// </summary>
#else
        /// <summary>
        /// Whether or not there is vote in the first day.
        /// </summary>
#endif
        [DataMember(Name = "votableInFirstDay")]
        public bool VotableInFirstDay { get; }

#if JHELP
        /// <summary>
        /// 乱数の種
        /// </summary>
#else
        /// <summary>
        /// The random seed.
        /// </summary>
#endif
        [DataMember(Name = "randomSeed")]
        public long RandomSeed { get; }

#if JHELP
        /// <summary>
        /// プレイヤーの数
        /// </summary>
#else
        /// <summary>
        /// The number of players.
        /// </summary>
#endif
        [DataMember(Name = "playerNum")]
        public int PlayerNum
        {
            get
            {
                return RoleNumMap == null ? 0 : RoleNumMap.Values.Sum();
            }
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        GameSetting() { }
    }
}

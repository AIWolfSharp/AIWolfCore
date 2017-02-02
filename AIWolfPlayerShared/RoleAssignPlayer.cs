//
// RoleAssignPlayer.cs
//
// Copyright (c) 2017 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

using AIWolf.Lib;
using AIWolf.Player.Lib;

namespace AIWolf.Player.Sample
{
#if JHELP
    /// <summary>
    /// 役職ごとに実際に使用するプレイヤーを切り替えるプレイヤー
    /// </summary>
#else
    /// <summary>
    /// Player class which switches player actually used according to its role.
    /// </summary>
#endif
    public class RoleAssignPlayer : AbstractRoleAssignPlayer
    {
#if JHELP
        /// <summary>
        /// 狩人プレイヤー
        /// </summary>
#else
        /// <summary>
        /// Bodyguard player.
        /// </summary>
#endif
        protected override IPlayer BodyguardPlayer
        {
            get
            {
                return new Bodyguard();
            }
        }

#if JHELP
        /// <summary>
        /// 霊媒師プレイヤー
        /// </summary>
#else
        /// <summary>
        /// Medium player.
        /// </summary>
#endif
        protected override IPlayer MediumPlayer
        {
            get
            {
                return new Medium();
            }
        }

#if JHELP
        /// <summary>
        /// 裏切り者プレイヤー
        /// </summary>
#else
        /// <summary>
        /// Possessed player.
        /// </summary>
#endif
        protected override IPlayer PossessedPlayer
        {
            get
            {
                return new Possessed();
            }
        }

#if JHELP
        /// <summary>
        /// 占い師プレイヤー
        /// </summary>
#else
        /// <summary>
        /// Seer player.
        /// </summary>
#endif
        protected override IPlayer SeerPlayer
        {
            get
            {
                return new Seer();
            }
        }

#if JHELP
        /// <summary>
        /// 村人プレイヤー
        /// </summary>
#else
        /// <summary>
        /// Villager player.
        /// </summary>
#endif
        protected override IPlayer VillagerPlayer
        {
            get
            {
                return new Villager();
            }
        }

#if JHELP
        /// <summary>
        /// 人狼プレイヤー
        /// </summary>
#else
        /// <summary>
        /// Werewolf player.
        /// </summary>
#endif
        protected override IPlayer WerewolfPlayer
        {
            get
            {
                return new Werewolf();
            }
        }
    }
}

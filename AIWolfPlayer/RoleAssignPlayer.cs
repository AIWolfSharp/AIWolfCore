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
        protected override IPlayer BodyguardPlayer
        {
            get
            {
                return new Bodyguard();
            }
        }

        protected override IPlayer MediumPlayer
        {
            get
            {
                return new Medium();
            }
        }

        protected override IPlayer PossessedPlayer
        {
            get
            {
                return new Possessed();
            }
        }

        protected override IPlayer SeerPlayer
        {
            get
            {
                return new Seer();
            }
        }

        protected override IPlayer VillagerPlayer
        {
            get
            {
                return new Villager();
            }
        }

        protected override IPlayer WerewolfPlayer
        {
            get
            {
                return new Werewolf();
            }
        }
    }
}

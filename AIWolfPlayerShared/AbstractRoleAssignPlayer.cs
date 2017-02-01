//
// AbstractRoleAssignPlayer.cs
//
// Copyright (c) 2017 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

using AIWolf.Lib;
using AIWolf.Player.Sample;

namespace AIWolf.Player.Lib
{
#if JHELP
    /// <summary>
    /// 役職ごとに実際に使用するプレイヤーを切り替えるプレイヤーの抽象クラス
    /// </summary>
#else
    /// <summary>
    /// Abstract player class which switches player actually used according to its role.
    /// </summary>
#endif
    public abstract class AbstractRoleAssignPlayer : IPlayer
    {
#if JHELP
        /// <summary>
        /// 村人プレイヤー
        /// </summary>
#else
        /// <summary>
        /// Villager player.
        /// </summary>
#endif
        protected abstract IPlayer VillagerPlayer { get; }

#if JHELP
        /// <summary>
        /// 狩人プレイヤー
        /// </summary>
#else
        /// <summary>
        /// Bodyguard player.
        /// </summary>
#endif
        protected abstract IPlayer BodyguardPlayer { get; }

#if JHELP
        /// <summary>
        /// 占い師プレイヤー
        /// </summary>
#else
        /// <summary>
        /// Seer player.
        /// </summary>
#endif
        protected abstract IPlayer SeerPlayer { get; }

#if JHELP
        /// <summary>
        /// 霊媒師プレイヤー
        /// </summary>
#else
        /// <summary>
        /// Medium player.
        /// </summary>
#endif
        protected abstract IPlayer MediumPlayer { get; }

#if JHELP
        /// <summary>
        /// 裏切り者プレイヤー
        /// </summary>
#else
        /// <summary>
        /// Possessed player.
        /// </summary>
#endif
        protected abstract IPlayer PossessedPlayer { get; }

#if JHELP
        /// <summary>
        /// 人狼プレイヤー
        /// </summary>
#else
        /// <summary>
        /// Werewolf player.
        /// </summary>
#endif
        protected abstract IPlayer WerewolfPlayer { get; }

        IPlayer player;

        public string Name
        {
            get
            {
                return GetType().ToString();
            }
        }

        public void Update(GameInfo gameInfo)
        {
            player.Update(gameInfo);
        }

        public void Initialize(GameInfo gameInfo, GameSetting gameSetting)
        {
            switch (gameInfo.Role)
            {
                case Role.VILLAGER:
                    player = VillagerPlayer;
                    break;
                case Role.SEER:
                    player = SeerPlayer;
                    break;
                case Role.MEDIUM:
                    player = MediumPlayer;
                    break;
                case Role.BODYGUARD:
                    player = BodyguardPlayer;
                    break;
                case Role.POSSESSED:
                    player = PossessedPlayer;
                    break;
                case Role.WEREWOLF:
                    player = WerewolfPlayer;
                    break;
                default:
                    player = VillagerPlayer;
                    break;
            }
            player.Initialize(gameInfo, gameSetting);
        }

        public void DayStart()
        {
            player.DayStart();
        }

        public string Talk()
        {
            return player.Talk();
        }

        public string Whisper()
        {
            return player.Whisper();
        }

        public Agent Vote()
        {
            return player.Vote();
        }

        public Agent Attack()
        {
            return player.Attack();
        }

        public Agent Divine()
        {
            return player.Divine();
        }

        public Agent Guard()
        {
            return player.Guard();
        }

        public void Finish()
        {
            player.Finish();
        }
    }
}

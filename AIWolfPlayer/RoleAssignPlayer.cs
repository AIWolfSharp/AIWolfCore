//
// RoleAssignPlayer.cs
//
// Copyright (c) 2017 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

using AIWolf.Lib;

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
    public class RoleAssignPlayer : IPlayer
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
        IPlayer villagerPlayer = new Villager();

#if JHELP
        /// <summary>
        /// 狩人プレイヤー
        /// </summary>
#else
        /// <summary>
        /// Bodyguard player.
        /// </summary>
#endif
        IPlayer bodyguardPlayer = new Bodyguard();

#if JHELP
        /// <summary>
        /// 占い師プレイヤー
        /// </summary>
#else
        /// <summary>
        /// Seer player.
        /// </summary>
#endif
        IPlayer seerPlayer = new Seer();

#if JHELP
        /// <summary>
        /// 霊媒師プレイヤー
        /// </summary>
#else
        /// <summary>
        /// Medium player.
        /// </summary>
#endif
        IPlayer mediumPlayer = new Medium();

#if JHELP
        /// <summary>
        /// 裏切り者プレイヤー
        /// </summary>
#else
        /// <summary>
        /// Possessed player.
        /// </summary>
#endif
        IPlayer possessedPlayer = new Possessed();

#if JHELP
        /// <summary>
        /// 人狼プレイヤー
        /// </summary>
#else
        /// <summary>
        /// Werewolf player.
        /// </summary>
#endif
        IPlayer werewolfPlayer = new Werewolf();

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
                    player = villagerPlayer;
                    break;
                case Role.SEER:
                    player = seerPlayer;
                    break;
                case Role.MEDIUM:
                    player = mediumPlayer;
                    break;
                case Role.BODYGUARD:
                    player = bodyguardPlayer;
                    break;
                case Role.POSSESSED:
                    player = possessedPlayer;
                    break;
                case Role.WEREWOLF:
                    player = werewolfPlayer;
                    break;
                default:
                    player = villagerPlayer;
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

//
// AbstractPlayer.cs
//
// Copyright (c) 2017 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

using AIWolf.Lib;
using AIWolf.Player.Sample;
using System;

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
    public abstract class AbstractRoleAssignPlayer : AbstractPlayer
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
        protected AbstractVillager VillagerPlayer { get; set; } = new Villager();

#if JHELP
        /// <summary>
        /// 狩人プレイヤー
        /// </summary>
#else
        /// <summary>
        /// Bodyguard player.
        /// </summary>
#endif
        protected AbstractBodyguard BodyguardPlayer { get; set; } = new Bodyguard();

#if JHELP
        /// <summary>
        /// 占い師プレイヤー
        /// </summary>
#else
        /// <summary>
        /// Seer player.
        /// </summary>
#endif
        protected AbstractSeer SeerPlayer { get; set; } = new Seer();

#if JHELP
        /// <summary>
        /// 霊媒師プレイヤー
        /// </summary>
#else
        /// <summary>
        /// Medium player.
        /// </summary>
#endif
        protected AbstractMedium MediumPlayer { get; set; } = new Medium();

#if JHELP
        /// <summary>
        /// 裏切り者プレイヤー
        /// </summary>
#else
        /// <summary>
        /// Possessed player.
        /// </summary>
#endif
        protected AbstractPossessed PossessedPlayer { get; set; } = new Possessed();

#if JHELP
        /// <summary>
        /// 人狼プレイヤー
        /// </summary>
#else
        /// <summary>
        /// Werewolf player.
        /// </summary>
#endif
        protected AbstractWerewolf WerewolfPlayer { get; set; } = new Werewolf();

        IPlayer player;

        sealed public override void Update(GameInfo gameInfo)
        {
            player.Update(gameInfo);
        }

        sealed public override void Initialize(GameInfo gameInfo, GameSetting gameSetting)
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

        sealed public override void DayStart()
        {
            player.DayStart();
        }

        sealed public override string Talk()
        {
            return player.Talk();
        }

        sealed public override string Whisper()
        {
            return player.Whisper();
        }

        sealed public override Agent Vote()
        {
            return player.Vote();
        }

        sealed public override Agent Attack()
        {
            return player.Attack();
        }

        sealed public override Agent Divine()
        {
            return player.Divine();
        }

        sealed public override Agent Guard()
        {
            return player.Guard();
        }

        sealed public override void Finish()
        {
            player.Finish();
        }
    }

#if JHELP
    /// <summary>
    /// プレイヤー用抽象クラス
    /// </summary>
#else
    /// <summary>
    /// Abstract class for player.
    /// </summary>
#endif
    public abstract class AbstractPlayer : IPlayer
    {
        public virtual string Name
        {
            get
            {
                return GetType().ToString();
            }
        }

        public abstract Agent Attack();
        public abstract void DayStart();
        public abstract Agent Divine();
        public abstract void Finish();
        public abstract Agent Guard();
        public abstract void Initialize(GameInfo gameInfo, GameSetting gameSetting);
        public abstract string Talk();
        public abstract void Update(GameInfo gameInfo);
        public abstract Agent Vote();
        public abstract string Whisper();
    }

#if JHELP
    /// <summary>
    /// 村人用抽象クラス
    /// </summary>
#else
    /// <summary>
    /// Abstract class for villager.
    /// </summary>
#endif
    public abstract class AbstractVillager : AbstractPlayer
    {
        sealed public override Agent Attack()
        {
            throw new UnexpectedMethodCallException();
        }

        sealed public override Agent Divine()
        {
            throw new UnexpectedMethodCallException();
        }

        sealed public override Agent Guard()
        {
            throw new UnexpectedMethodCallException();
        }

        sealed public override string Whisper()
        {
            throw new UnexpectedMethodCallException();
        }
    }

#if JHELP
    /// <summary>
    /// 狩人用抽象クラス
    /// </summary>
#else
    /// <summary>
    /// Abstract class for bodyguard.
    /// </summary>
#endif
    public abstract class AbstractBodyguard : AbstractPlayer
    {
        sealed public override Agent Attack()
        {
            throw new UnexpectedMethodCallException();
        }

        sealed public override Agent Divine()
        {
            throw new UnexpectedMethodCallException();
        }

        sealed public override string Whisper()
        {
            throw new UnexpectedMethodCallException();
        }
    }

#if JHELP
    /// <summary>
    /// 占い師用抽象クラス
    /// </summary>
#else
    /// <summary>
    /// Abstract class for seer.
    /// </summary>
#endif
    public abstract class AbstractSeer : AbstractPlayer
    {
        sealed public override Agent Attack()
        {
            throw new UnexpectedMethodCallException();
        }

        sealed public override Agent Guard()
        {
            throw new UnexpectedMethodCallException();
        }

        sealed public override string Whisper()
        {
            throw new UnexpectedMethodCallException();
        }
    }

#if JHELP
    /// <summary>
    /// 霊媒師用抽象クラス
    /// </summary>
#else
    /// <summary>
    /// Abstract class for medium.
    /// </summary>
#endif
    public abstract class AbstractMedium : AbstractPlayer
    {
        sealed public override Agent Attack()
        {
            throw new UnexpectedMethodCallException();
        }

        sealed public override Agent Divine()
        {
            throw new UnexpectedMethodCallException();
        }

        sealed public override Agent Guard()
        {
            throw new UnexpectedMethodCallException();
        }

        sealed public override string Whisper()
        {
            throw new UnexpectedMethodCallException();
        }
    }

#if JHELP
    /// <summary>
    /// 裏切り者用抽象クラス
    /// </summary>
#else
    /// <summary>
    /// Abstract class for possessed.
    /// </summary>
#endif
    public abstract class AbstractPossessed : AbstractPlayer
    {
        sealed public override Agent Attack()
        {
            throw new UnexpectedMethodCallException();
        }

        sealed public override Agent Divine()
        {
            throw new UnexpectedMethodCallException();
        }

        sealed public override Agent Guard()
        {
            throw new UnexpectedMethodCallException();
        }

        sealed public override string Whisper()
        {
            throw new UnexpectedMethodCallException();
        }
    }

#if JHELP
    /// <summary>
    /// 人狼用抽象クラス
    /// </summary>
#else
    /// <summary>
    /// Abstract class for werewolf.
    /// </summary>
#endif
    public abstract class AbstractWerewolf : AbstractPlayer
    {
        sealed public override Agent Divine()
        {
            throw new UnexpectedMethodCallException();
        }

        sealed public override Agent Guard()
        {
            throw new UnexpectedMethodCallException();
        }
    }

    class UnexpectedMethodCallException : Exception
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public UnexpectedMethodCallException()
        {
        }

        /// <summary>
        /// Initializes a new instance of this class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public UnexpectedMethodCallException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of this class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception,
        /// or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public UnexpectedMethodCallException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

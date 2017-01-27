using AIWolf.Lib;
using AIWolf.Player.Lib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AIWolf.Player.Sample
{

#if JHELP
    /// <summary>
    /// 追加ゲーム情報
    /// </summary>
#else
    /// <summary>
    /// Additional game information.
    /// </summary>
#endif
    class AdditionalGameInfo
    {
        /// <summary>
        /// 日付
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// 自分以外のエージェント
        /// </summary>
        public List<Agent> Others { get; }

        /// <summary>
        /// 自分以外の生存エージェント
        /// </summary>
        public List<Agent> AliveOthers { get; }

        /// <summary>
        /// 死亡したエージェント
        /// </summary>
        public List<Agent> DeadAgents { get; } = new List<Agent>();

        /// <summary>
        /// 追放されたエージェント
        /// </summary>
        public List<Agent> ExecutedAgents { get; } = new List<Agent>();

        /// <summary>
        /// 殺されたエージェント
        /// </summary>
        public List<Agent> KilledAgents { get; } = new List<Agent>();

        /// <summary>
        /// 昨日殺されたエージェント
        /// </summary>
        public Agent LastKilledAgent { get; set; }

        /// <summary>
        /// カミングアウト状況
        /// </summary>
        public Dictionary<Agent, Role> ComingoutMap { get; } = new Dictionary<Agent, Role>();

        /// <summary>
        /// 推定宣言状況
        /// </summary>
        public Dictionary<Agent, List<Talk>> EstimateMap { get; } = new Dictionary<Agent, List<Talk>>();

        /// <summary>
        /// 占い報告リスト
        /// </summary>
        public List<Judge> DivinationList { get; } = new List<Judge>();

        /// <summary>
        /// 霊媒結果リスト
        /// </summary>
        public List<Judge> IdentList { get; } = new List<Judge>();

        int talkListHead; // GameInfo.TalkList読み込みのヘッド

        /// <summary>
        /// Initializes a new instance of AdditionalGameInfo.
        /// </summary>
        /// <param name="gameInfo">Game information.</param>
        public AdditionalGameInfo(GameInfo gameInfo)
        {
            Day = -1;
            Others = gameInfo.AliveAgentList.Where(a => a != gameInfo.Agent).ToList();
            AliveOthers = new List<Agent>(Others);
        }

        /// <summary>
        /// Update AdditionalGameinfo.
        /// </summary>
        /// <param name="gameInfo">Game information.</param>
        public void Update(GameInfo gameInfo)
        {
            // 1日の最初の呼び出しではその日の初期化などを行う
            if (gameInfo.Day != Day)
            {
                Day = gameInfo.Day;
                talkListHead = 0;
                AddExecutedAgent(gameInfo.ExecutedAgent); // 前日に追放されたエージェントを登録
                if (gameInfo.LastDeadAgentList.Count != 0)
                {
                    LastKilledAgent = gameInfo.LastDeadAgentList[0]; // 妖狐がいないので長さ最大1
                }
                if (LastKilledAgent != null)
                {
                    if (!KilledAgents.Contains(LastKilledAgent))
                    {
                        KilledAgents.Add(LastKilledAgent);
                    }
                    AliveOthers.Remove(LastKilledAgent);
                    if (!DeadAgents.Contains(LastKilledAgent))
                    {
                        DeadAgents.Add(LastKilledAgent);
                    }
                }
            }
        }

        /// <summary>
        /// エージェントを追放されたエージェントのリストに追加する．死亡者・生存者リストも更新する
        /// </summary>
        void AddExecutedAgent(Agent executedAgent)
        {
            if (executedAgent != null)
            {
                if (!ExecutedAgents.Contains(executedAgent))
                {
                    ExecutedAgents.Add(executedAgent);
                }
                AliveOthers.Remove(executedAgent);
                if (!DeadAgents.Contains(executedAgent))
                {
                    DeadAgents.Add(executedAgent);
                }
            }
        }
    }


#if JHELP
    /// <summary>
    /// 村人プレイヤーの見本
    /// </summary>
#else
    /// <summary>
    /// Sample villager player.
    /// </summary>
#endif
    public class Villager : AbstractVillager
    {
        // このエージェント
        Agent me;
        // ゲーム情報
        GameInfo currentGameInfo;
        // 追加ゲーム情報
        AdditionalGameInfo agi;
        // 投票先候補
        Agent voteCandidate;
        // 宣言した投票先
        Agent declaredVoteCandidate;
        // 人狼候補リスト
        List<Agent> werewolves = new List<Agent>();
        // 発言の待ち行列
        Queue<Content> talkQueue = new Queue<Content>();

        public override void DayStart()
        {
            declaredVoteCandidate = null;
            voteCandidate = null;
            talkQueue.Clear();
        }

        public override void Finish()
        {
        }

        public override void Initialize(GameInfo gameInfo, GameSetting gameSetting)
        {
            me = gameInfo.Agent;
            agi = new AdditionalGameInfo(gameInfo);
            werewolves.Clear();
        }

        public override string Talk()
        {
            ChooseVoteCandidate();
            if (voteCandidate != declaredVoteCandidate)
            {
                talkQueue.Enqueue(new Content(new VoteContentBuilder(voteCandidate)));
                declaredVoteCandidate = voteCandidate;
            }
            return talkQueue.Count == 0 ? Content.SKIP.Text : talkQueue.Dequeue().Text;
        }

        public override void Update(GameInfo gameInfo)
        {
            currentGameInfo = gameInfo;
            agi.Update(currentGameInfo);
        }

        public override Agent Vote()
        {
            return voteCandidate;
        }

        /// <summary>
        /// 投票先候補を選ぶ
        /// </summary>
        void ChooseVoteCandidate()
        {
            // 自分や死亡したエージェントを人狼と判定していて，生存している占い師を投票先候補とする
            werewolves.Clear();
            foreach (Judge judge in agi.DivinationList)
            {
                if ((judge.Target == me || agi.KilledAgents.Contains(judge.Target))
                        && judge.Result == Species.WEREWOLF)
                {
                    if (!werewolves.Contains(judge.Agent))
                    {
                        werewolves.Add(judge.Agent);
                    }
                }
            }
            List<Agent> candidates = werewolves.Where(a => !agi.DeadAgents.Contains(a)).ToList();
            // 投票先候補が見つかった場合
            if (candidates.Count != 0)
            {
                // 以前の投票先から変わる場合，新たに推測発言と占い要請をする
                if (!candidates.Contains(voteCandidate))
                {
                    voteCandidate = candidates.Shuffle().First();
                    talkQueue.Enqueue(new Content(new EstimateContentBuilder(voteCandidate, Role.WEREWOLF)));
                    talkQueue.Enqueue(new Content(new RequestContentBuilder(null, new Content(new DivinationContentBuilder(voteCandidate)))));
                }
            }
            // 投票先候補が見つからなかった場合
            else
            {
                // 既定の投票先があれば投票先はそのまま。投票先未定の場合自分以外の生存者から投票先を選ぶ
                if (voteCandidate == null)
                {
                    voteCandidate = agi.AliveOthers.Shuffle().First();
                }
            }
        }
    }

    public class Bodyguard : AbstractBodyguard
    {
        public override void DayStart()
        {
            throw new NotImplementedException();
        }

        public override void Finish()
        {
            throw new NotImplementedException();
        }

        public override Agent Guard()
        {
            throw new NotImplementedException();
        }

        public override void Initialize(GameInfo gameInfo, GameSetting gameSetting)
        {
            throw new NotImplementedException();
        }

        public override string Talk()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameInfo gameInfo)
        {
            throw new NotImplementedException();
        }

        public override Agent Vote()
        {
            throw new NotImplementedException();
        }
    }

    public class Medium : AbstractMedium
    {
        public override void DayStart()
        {
            throw new NotImplementedException();
        }

        public override void Finish()
        {
            throw new NotImplementedException();
        }

        public override void Initialize(GameInfo gameInfo, GameSetting gameSetting)
        {
            throw new NotImplementedException();
        }

        public override string Talk()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameInfo gameInfo)
        {
            throw new NotImplementedException();
        }

        public override Agent Vote()
        {
            throw new NotImplementedException();
        }
    }

    public class Seer : AbstractSeer
    {
        public override void DayStart()
        {
            throw new NotImplementedException();
        }

        public override Agent Divine()
        {
            throw new NotImplementedException();
        }

        public override void Finish()
        {
            throw new NotImplementedException();
        }

        public override void Initialize(GameInfo gameInfo, GameSetting gameSetting)
        {
            throw new NotImplementedException();
        }

        public override string Talk()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameInfo gameInfo)
        {
            throw new NotImplementedException();
        }

        public override Agent Vote()
        {
            throw new NotImplementedException();
        }
    }

    public class Possessed : AbstractPossessed
    {
        public override void DayStart()
        {
            throw new NotImplementedException();
        }

        public override void Finish()
        {
            throw new NotImplementedException();
        }

        public override void Initialize(GameInfo gameInfo, GameSetting gameSetting)
        {
            throw new NotImplementedException();
        }

        public override string Talk()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameInfo gameInfo)
        {
            throw new NotImplementedException();
        }

        public override Agent Vote()
        {
            throw new NotImplementedException();
        }
    }

    public class Werewolf : AbstractWerewolf
    {
        public override Agent Attack()
        {
            throw new NotImplementedException();
        }

        public override void DayStart()
        {
            throw new NotImplementedException();
        }

        public override void Finish()
        {
            throw new NotImplementedException();
        }

        public override void Initialize(GameInfo gameInfo, GameSetting gameSetting)
        {
            throw new NotImplementedException();
        }

        public override string Talk()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameInfo gameInfo)
        {
            throw new NotImplementedException();
        }

        public override Agent Vote()
        {
            throw new NotImplementedException();
        }

        public override string Whisper()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Defines extension method to shuffle what implements IEnumerable interface.
    /// </summary>
    public static class ShuffleExtensions
    {
        /// <summary>
        /// Returns randomized sequence of T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s">Sequence of T.</param>
        /// <returns>Randomized sequence of T.</returns>
        public static IOrderedEnumerable<T> Shuffle<T>(this IEnumerable<T> s)
        {
            return s.OrderBy(x => Guid.NewGuid());
        }
    }
}

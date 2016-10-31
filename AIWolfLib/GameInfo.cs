//
// GameInfo.cs
//
// Copyright (c) 2016 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace AIWolf.Lib
{
#if JHELP
    /// <summary>
    /// ゲーム情報
    /// </summary>
#else
    /// <summary>
    /// Game information.
    /// </summary>
#endif
    [DataContract]
    public class GameInfo
    {
#if JHELP
        /// <summary>
        /// 本日
        /// </summary>
#else
        /// <summary>
        /// Current day.
        /// </summary>
#endif
        [DataMember(Name = "day")]
        public int Day { get; }

#if JHELP
        /// <summary>
        /// このゲーム情報を受け取るエージェント
        /// </summary>
#else
        /// <summary>
        /// The agent who receives this GameInfo.
        /// </summary>
#endif
        public Agent Agent { get; }

        /// <summary>
        /// The index number of agent who receives this game information.
        /// </summary>
        [DataMember(Name = "agent")]
        int _Agent { get; }

#if JHELP
        /// <summary>
        /// このゲーム情報を受け取るエージェントの役職
        /// </summary>
#else
        /// <summary>
        /// The role of player who receives this GameInfo.
        /// </summary>
#endif
        public Role Role
        {
            get
            {
                return _Agent != 0 && RoleMap.ContainsKey(Agent) ? RoleMap[Agent] : Role.UNC;
            }
        }

#if JHELP
        /// <summary>
        /// 霊能結果
        /// </summary>
        /// <remarks>霊能力者限定</remarks>
#else
        /// <summary>
        /// The result of the inquest.
        /// </summary>
        /// <remarks>Medium only.</remarks>
#endif
        [DataMember(Name = "mediumResult")]
        public Judge MediumResult { get; }

#if JHELP
        /// <summary>
        /// 占い結果
        /// </summary>
        /// <remarks>占い師限定</remarks>
#else
        /// <summary>
        /// The result of the dvination.
        /// </summary>
        /// <remarks>Seer only.</remarks>
#endif
        [DataMember(Name = "divineResult")]
        public Judge DivineResult { get; }

#if JHELP
        /// <summary>
        /// 昨夜処刑されたエージェント
        /// </summary>
#else
        /// <summary>
        /// The agent executed last night.
        /// </summary>
#endif
        public Agent ExecutedAgent { get; }

        /// <summary>
        /// The index number of the agent executed last night.
        /// </summary>
        [DataMember(Name = "executedAgent")]
        int _ExecutedAgent { get; }

#if JHELP
        /// <summary>
        /// 昨夜襲撃されたエージェント
        /// </summary>
#else
        /// <summary>
        /// The agent attacked last night.
        /// </summary>
#endif
        public Agent AttackedAgent { get; }

        /// <summary>
        /// The index number of the agent attacked last night.
        /// </summary>
        [DataMember(Name = "attackedAgent")]
        int _AttackedAgent { get; }

#if JHELP
        /// <summary>
        /// 昨夜護衛されたエージェント
        /// </summary>
#else
        /// <summary>
        /// The agent guarded last night.
        /// </summary>
#endif
        public Agent GuardedAgent { get; }

        /// <summary>
        /// The index number of the agent guarded last night.
        /// </summary>
        [DataMember(Name = "guardedAgent")]
        int _GuardedAgent { get; }

#if JHELP
        /// <summary>
        /// 処刑投票のリスト
        /// </summary>
        /// <remarks>各プレイヤーの投票先がわかる</remarks>
#else
        /// <summary>
        /// The list of votes for execution.
        /// </summary>
        /// <remarks>You can see who votes to who.</remarks>
#endif
        [DataMember(Name = "voteList")]
        public List<Vote> VoteList { get; }

#if JHELP
        /// <summary>
        /// 襲撃投票リスト
        /// </summary>
        /// <remarks>人狼限定</remarks>
#else
        /// <summary>
        /// The list of votes for attack.
        /// </summary>
        /// <remarks>Werewolf only.</remarks>
#endif
        [DataMember(Name = "attackVoteList")]
        public List<Vote> AttackVoteList { get; }

#if JHELP
        /// <summary>
        /// 本日の会話リスト
        /// </summary>
#else
        /// <summary>
        /// The list of today's talks.
        /// </summary>
#endif
        [DataMember(Name = "talkList")]
        public List<Talk> TalkList { get; }

#if JHELP
        /// <summary>
        /// 本日の囁きリスト
        /// </summary>
        /// <remarks>人狼限定</remarks>
#else
        /// <summary>
        /// The list of today's whispers.
        /// </summary>
        /// <remarks>Werewolf only.</remarks>
#endif
        [DataMember(Name = "whisperList")]
        public List<Whisper> WhisperList { get; }

#if JHELP
        /// <summary>
        /// 全エージェントの生死状況
        /// </summary>
#else
        /// <summary>
        /// The statuses of all agents.
        /// </summary>
#endif
        public Dictionary<Agent, Status> StatusMap { get; }

        /// <summary>
        /// The statuses of all agents.
        /// </summary>
        [DataMember(Name = "statusMap")]
        Dictionary<int, string> _StatusMap { get; }

#if JHELP
        /// <summary>
        /// 役職既知のエージェント
        /// </summary>
        /// <remarks>
        /// 人間の場合，自分自身しかわからない
        /// 人狼の場合，誰が他の人狼かがわかる
        /// </remarks>
#else
        /// <summary>
        /// The known roles of agents.
        /// </summary>
        /// <remarks>
        /// If you are human, you know only yourself.
        /// If you are werewolf, you know other werewolves.
        /// </remarks>
#endif
        public Dictionary<Agent, Role> RoleMap { get; }

        /// <summary>
        /// The known roles of agents.
        /// </summary>
        /// <remarks>
        /// If you are human, you know only yourself.
        /// If you are werewolf, you know other werewolves.
        /// </remarks>
        [DataMember(Name = "roleMap")]
        Dictionary<int, string> _RoleMap { get; }

#if JHELP
        /// <summary>
        /// エージェントのリスト
        /// </summary>
#else
        /// <summary>
        /// The list of agents.
        /// </summary>
#endif
        public List<Agent> AgentList
        {
            get
            {
                return StatusMap.Keys.ToList();
            }
        }

#if JHELP
        /// <summary>
        /// 生存しているエージェントのリスト
        /// </summary>
        /// <remarks>すべてのエージェントが死んだ場合，nullではなく空のリストを返す</remarks>
#else
        /// <summary>
        /// The list of alive agents.
        /// </summary>
        /// <remarks>If all agents are dead, this returns an empty list, not null.</remarks>
#endif
        public List<Agent> AliveAgentList
        {
            get
            {
                return AgentList.Where(a => StatusMap[a] == Status.ALIVE).ToList();
            }
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="day">The current day.</param>
        /// <param name="agent">The agent who receives this.</param>
        /// <param name="mediumResult">The result of the inquest.</param>
        /// <param name="divineResult">The result of the divination.</param>
        /// <param name="executedAgent">The agent executed.</param>
        /// <param name="attackedAgent">The agent attacked.</param>
        /// <param name="guardedAgent">The agent guarded.</param>
        /// <param name="voteList">The list of votes for execution.</param>
        /// <param name="attackVoteList">The list of votes for attack.</param>
        /// <param name="talkList">The list of talks.</param>
        /// <param name="whisperList">The list of whispers.</param>
        /// <param name="statusMap">The map between agent and its status.</param>
        /// <param name="roleMap">The map between agent and its role.</param>
        [JsonConstructor]
        GameInfo(int day, int agent, Judge mediumResult, Judge divineResult, int executedAgent, int attackedAgent, int guardedAgent,
            List<Vote> voteList, List<Vote> attackVoteList, List<Talk> talkList, List<Whisper> whisperList,
            Dictionary<int, string> statusMap, Dictionary<int, string> roleMap)
        {
            Day = day;
            if (Day < 0)
            {
                Error.RuntimeError("Invalid day " + Day + ".");
                Error.Warning("Force it to be 0.");
                Day = 0;
            }

            Agent = Agent.GetAgent(agent);
            if (Agent == null)
            {
                Error.RuntimeError("Agent must not be null.");
                Error.Warning("Force it to be Agent[00].");
                Agent = Agent.GetAgent(0);
            }
            _Agent = Agent.AgentIdx;

            MediumResult = mediumResult;
            DivineResult = divineResult;

            ExecutedAgent = Agent.GetAgent(executedAgent);
            _ExecutedAgent = ExecutedAgent == null ? -1 : ExecutedAgent.AgentIdx;

            AttackedAgent = Agent.GetAgent(attackedAgent);
            _AttackedAgent = AttackedAgent == null ? -1 : AttackedAgent.AgentIdx;

            GuardedAgent = Agent.GetAgent(guardedAgent);
            _GuardedAgent = GuardedAgent == null ? -1 : GuardedAgent.AgentIdx;

            VoteList = voteList == null ? new List<Vote>() : voteList;
            AttackVoteList = attackVoteList == null ? new List<Vote>() : attackVoteList;
            TalkList = talkList == null ? new List<Talk>() : talkList;
            WhisperList = whisperList == null ? new List<Whisper>() : whisperList;

            StatusMap = new Dictionary<Agent, Status>();
            if (statusMap != null)
            {
                foreach (var p in statusMap)
                {
                    Status status;
                    if (!Enum.TryParse(p.Value, out status))
                    {
                        Error.RuntimeError("Invalid status string " + p.Value + ".");
                        Error.Warning("Force it to be Status.ALIVE.");
                        status = Status.ALIVE;
                    }
                    StatusMap[Agent.GetAgent(p.Key)] = status;
                }
            }
            _StatusMap = StatusMap.ToDictionary(p => p.Key.AgentIdx, p => p.Value.ToString());

            RoleMap = new Dictionary<Agent, Role>();
            if (roleMap != null)
            {
                foreach (var p in roleMap)
                {
                    Role role;
                    if (!Enum.TryParse(p.Value, out role) || role == Role.UNC)
                    {
                        Error.RuntimeError("Invalid role string " + p.Value + ".");
                        Error.Warning("It is removed from role map.");
                    }
                    else
                    {
                        RoleMap[Agent.GetAgent(p.Key)] = role;
                    }
                }
            }
            _RoleMap = RoleMap.Where(m => m.Value != Role.UNC).ToDictionary(m => m.Key.AgentIdx, m => m.Value.ToString());
        }
    }
}

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
    /// <summary>
    /// Game information.
    /// </summary>
    [DataContract]
    public class GameInfo
    {
        int day;
        Agent agent;
        Agent executedAgent;
        Agent attackedAgent;
        Agent guardedAgent;
        Dictionary<Agent, Status> statusMap = new Dictionary<Agent, Status>();
        Dictionary<Agent, Role> roleMap = new Dictionary<Agent, Role>();

        /// <summary>
        /// Current day.
        /// </summary>
        [DataMember(Name = "day")]
        public int Day
        {
            set
            {
                if (value < 0)
                {
                    Error.RuntimeError(GetType() + ".Day: Invalid day " + value + ".", "Force it to be 0.");
                    day = 0;
                }
                else
                {
                    day = value;
                }
            }
            get
            {
                return day;
            }
        }

        /// <summary>
        /// The agent who receives this GameInfo.
        /// </summary>
        public Agent Agent
        {
            set
            {
                agent = value;
                _Agent = agent == null ? -1 : agent.AgentIdx;
            }
            get
            {
                return agent;
            }
        }

        /// <summary>
        /// The index number of agent who receives this game information.
        /// </summary>
        [DataMember(Name = "agent")]
        public int _Agent { get; private set; } = -1;

        /// <summary>
        /// The role of player who receives this GameInfo.
        /// </summary>
        public Role Role
        {
            get
            {
                return Agent != null && RoleMap.ContainsKey(Agent) ? RoleMap[Agent] : Role.UNC;
            }
        }

        /// <summary>
        /// The result of the inquest.
        /// </summary>
        /// <remarks>Medium only.</remarks>
        [DataMember(Name = "mediumResult")]
        public Judge MediumResult { get; set; }

        /// <summary>
        /// The result of the dvination.
        /// </summary>
        /// <remarks>Seer only.</remarks>
        [DataMember(Name = "divineResult")]
        public Judge DivineResult { get; set; }

        /// <summary>
        /// The agent executed last night.
        /// </summary>
        public Agent ExecutedAgent
        {
            set
            {
                executedAgent = value;
                _ExecutedAgent = executedAgent == null ? -1 : executedAgent.AgentIdx;
            }
            get
            {
                return executedAgent;
            }
        }

        /// <summary>
        /// The index number of the agent executed last night.
        /// </summary>
        [DataMember(Name = "executedAgent")]
        public int _ExecutedAgent { get; private set; } = -1;

        /// <summary>
        /// The agent attacked last night.
        /// </summary>
        public Agent AttackedAgent
        {
            set
            {
                attackedAgent = value;
                _AttackedAgent = attackedAgent == null ? -1 : attackedAgent.AgentIdx;
            }
            get
            {
                return attackedAgent;
            }
        }

        /// <summary>
        /// The index number of the agent attacked last night.
        /// </summary>
        [DataMember(Name = "attackedAgent")]
        public int _AttackedAgent { get; private set; } = -1;

        /// <summary>
        /// The agent guarded last night.
        /// </summary>
        public Agent GuardedAgent
        {
            set
            {
                guardedAgent = value;
                _GuardedAgent = guardedAgent == null ? -1 : guardedAgent.AgentIdx;
            }
            get
            {
                return guardedAgent;
            }
        }

        /// <summary>
        /// The index number of the agent guarded last night.
        /// </summary>
        [DataMember(Name = "guardedAgent")]
        public int _GuardedAgent { get; private set; } = -1;

        /// <summary>
        /// The list of votes for execution.
        /// </summary>
        /// <remarks>You can see who votes to who.</remarks>
        [DataMember(Name = "voteList")]
        public List<Vote> VoteList { get; set; } = new List<Vote>();

        /// <summary>
        /// The list of votes for attack.
        /// </summary>
        /// <remarks>Werewolf only.</remarks>
        [DataMember(Name = "attackVoteList")]
        public List<Vote> AttackVoteList { get; set; } = new List<Vote>();

        /// <summary>
        /// The list of today's talks.
        /// </summary>
        [DataMember(Name = "talkList")]
        public List<Talk> TalkList { get; set; } = new List<Talk>();

        /// <summary>
        /// The list of today's whispers.
        /// </summary>
        /// <remarks>Werewolf only.</remarks>
        [DataMember(Name = "whisperList")]
        public List<Talk> WhisperList { get; set; } = new List<Talk>();

        /// <summary>
        /// The statuses of all agents.
        /// </summary>
        public Dictionary<Agent, Status> StatusMap
        {
            set
            {
                statusMap = value;
                _StatusMap = statusMap == null ? null : statusMap.ToDictionary(p => p.Key.AgentIdx, p => p.Value.ToString());
            }
            get
            {
                return statusMap;
            }
        }

        /// <summary>
        /// The statuses of all agents.
        /// </summary>
        [DataMember(Name = "statusMap")]
        public Dictionary<int, string> _StatusMap { get; private set; } = new Dictionary<int, string>();

        /// <summary>
        /// The known roles of agents.
        /// </summary>
        /// <remarks>
        /// If you are human, you know only yourself.
        /// If you are werewolf, you know other werewolves.
        /// </remarks>
        public Dictionary<Agent, Role> RoleMap
        {
            set
            {
                roleMap = value;
                _RoleMap = roleMap == null ? null : roleMap.Where(m => m.Value != Role.UNC).ToDictionary(m => m.Key.AgentIdx, m => m.Value.ToString());
            }
            get
            {
                return roleMap;
            }
        }

        /// <summary>
        /// The known roles of agents.
        /// </summary>
        /// <remarks>
        /// If you are human, you know only yourself.
        /// If you are werewolf, you know other werewolves.
        /// </remarks>
        [DataMember(Name = "roleMap")]
        public Dictionary<int, string> _RoleMap { get; private set; } = new Dictionary<int, string>();

        /// <summary>
        /// The list of agents.
        /// </summary>
        public List<Agent> AgentList
        {
            get
            {
                return StatusMap.Keys.ToList();
            }
        }

        /// <summary>
        /// The list of alive agents.
        /// </summary>
        /// <remarks>If all agents are dead, this returns an empty list, not null.</remarks>
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
        public GameInfo() { }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        [JsonConstructor]
        public GameInfo(int day, int agent, Judge mediumResult, Judge divineResult, int executedAgent, int attackedAgent, int guardedAgent,
            List<Vote> voteList, List<Vote> attackVoteList, List<Talk> talkList, List<Talk> whisperList,
            Dictionary<int, string> statusMap, Dictionary<int, string> roleMap)
        {
            Day = day;

            _Agent = agent;
            Agent = Agent.GetAgent(_Agent);

            MediumResult = mediumResult;
            DivineResult = divineResult;

            _ExecutedAgent = executedAgent;
            ExecutedAgent = Agent.GetAgent(_ExecutedAgent);

            _AttackedAgent = attackedAgent;
            AttackedAgent = Agent.GetAgent(_AttackedAgent);

            _GuardedAgent = guardedAgent;
            GuardedAgent = Agent.GetAgent(_GuardedAgent);

            VoteList = voteList;
            AttackVoteList = attackVoteList;
            TalkList = talkList;
            WhisperList = whisperList;

            StatusMap.Clear();
            _StatusMap = statusMap;
            foreach (var p in _StatusMap)
            {
                Status status;
                if (!Enum.TryParse(p.Value, out status))
                {
                    Error.RuntimeError(GetType() + "(): Invalid status string " + p.Value + ".", "Force it to be Status.ALIVE.");
                    status = Status.ALIVE;
                }
                StatusMap[Agent.GetAgent(p.Key)] = status;
            }

            RoleMap.Clear();
            _RoleMap = roleMap;
            foreach (var p in _RoleMap)
            {
                Role role;
                if (!Enum.TryParse(p.Value, out role) || role == Role.UNC)
                {
                    Error.RuntimeError(GetType() + "(): Invalid role string " + p.Value + ".", "It is removed from role map.");
                }
                else
                {
                    RoleMap[Agent.GetAgent(p.Key)] = role;
                }
            }
        }
    }
}

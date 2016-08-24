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
        /// <value>Current day.</value>
        [DataMember(Name = "day")]
        public int Day
        {
            set
            {
                if (value < 0)
                {
                    throw new AIWolfRuntimeException(GetType() + ": Invalid day " + value + ".");
                }
                day = value;
            }
            get
            {
                return day;
            }
        }

        /// <summary>
        /// The agent who receives this GameInfo.
        /// </summary>
        /// <value>The agent who receives this GameInfo.</value>
        public Agent Agent
        {
            set
            {
                agent = value;
                _Agent = agent.AgentIdx;
            }
            get
            {
                return agent;
            }
        }

        /// <summary>
        /// The index number of agent who receives this game information.
        /// </summary>
        /// <value>The index number of agent who receives this game information.</value>
        [DataMember(Name = "agent")]
        public int _Agent { get; private set; } = -1;

        /// <summary>
        /// The role of player who receives this GameInfo.
        /// </summary>
        /// <value>The role of player who receives this GameInfo.</value>
        public Role? Role
        {
            get
            {
                return Agent != null && RoleMap.ContainsKey(Agent) ? (Role?)RoleMap[Agent] : null;
            }
        }

        /// <summary>
        /// The result of the inquest.
        /// </summary>
        /// <value>The result of the inquest.</value>
        /// <remarks>Medium only.</remarks>
        [DataMember(Name = "mediumResult")]
        public Judge MediumResult { get; set; }

        /// <summary>
        /// The result of the dvination.
        /// </summary>
        /// <value>The result of the dvination.</value>
        /// <remarks>Seer only.</remarks>
        [DataMember(Name = "divineResult")]
        public Judge DivineResult { get; set; }

        /// <summary>
        /// The agent executed last night.
        /// </summary>
        /// <value>The agent executed last night.</value>
        public Agent ExecutedAgent
        {
            set
            {
                executedAgent = value;
                _ExecutedAgent = executedAgent.AgentIdx;
            }
            get
            {
                return executedAgent;
            }
        }

        /// <summary>
        /// The index number of the agent executed last night.
        /// </summary>
        /// <value>The index number of the agent executed last night.</value>
        [DataMember(Name = "executedAgent")]
        public int _ExecutedAgent { get; private set; } = -1;

        /// <summary>
        /// The agent attacked last night.
        /// </summary>
        /// <value>The agent attacked last night.</value>
        public Agent AttackedAgent
        {
            set
            {
                attackedAgent = value;
                _AttackedAgent = attackedAgent.AgentIdx;
            }
            get
            {
                return attackedAgent;
            }
        }

        /// <summary>
        /// The index number of the agent attacked last night.
        /// </summary>
        /// <value>The index number of the agent attacked last night.</value>
        [DataMember(Name = "attackedAgent")]
        public int _AttackedAgent { get; private set; } = -1;

        /// <summary>
        /// The agent guarded last night.
        /// </summary>
        /// <value>The agent guarded last night.</value>
        public Agent GuardedAgent
        {
            set
            {
                guardedAgent = value;
                _GuardedAgent = guardedAgent.AgentIdx;
            }
            get
            {
                return guardedAgent;
            }
        }

        /// <summary>
        /// The index number of the agent guarded last night.
        /// </summary>
        /// <value>The index number of the agent guarded last night.</value>
        [DataMember(Name = "guardedAgent")]
        public int _GuardedAgent { get; private set; } = -1;

        /// <summary>
        /// The list of votes for execution.
        /// </summary>
        /// <value>The list of votes for execution.</value>
        /// <remarks>You can see who votes to who.</remarks>
        [DataMember(Name = "voteList")]
        public List<Vote> VoteList { get; set; } = new List<Vote>();

        /// <summary>
        /// The list of votes for attack.
        /// </summary>
        /// <value>The list of votes for attack.</value>
        /// <remarks>Werewolf only.</remarks>
        [DataMember(Name = "attackVoteList")]
        public List<Vote> AttackVoteList { get; set; } = new List<Vote>();

        /// <summary>
        /// The list of today's talks.
        /// </summary>
        /// <value>The list of today's talks.</value>
        [DataMember(Name = "talkList")]
        public List<Talk> TalkList { get; set; } = new List<Talk>();

        /// <summary>
        /// The list of today's whispers.
        /// </summary>
        /// <value>The list of today's whispers.</value>
        /// <remarks>Werewolf only.</remarks>
        [DataMember(Name = "whisperList")]
        public List<Talk> WhisperList { get; set; } = new List<Talk>();

        /// <summary>
        /// The statuses of all agents.
        /// </summary>
        /// <value>The dictionary storing the statuses of all agents.</value>
        public Dictionary<Agent, Status> StatusMap
        {
            set
            {
                statusMap = value;
                _StatusMap = statusMap.ToDictionary(p => p.Key.AgentIdx, p => p.Value.ToString());
            }
            get
            {
                return statusMap;
            }
        }

        /// <summary>
        /// The statuses of all agents.
        /// </summary>
        /// <value>Dictionary storing the statuses of all agents.</value>
        [DataMember(Name = "statusMap")]
        public Dictionary<int, string> _StatusMap { get; private set; } = new Dictionary<int, string>();

        /// <summary>
        /// The known roles of agents.
        /// </summary>
        /// <value>The dictionary storing the known roles of agents.</value>
        /// <remarks>
        /// If you are human, you know only yourself.
        /// If you are werewolf, you know other werewolves.
        /// </remarks>
        public Dictionary<Agent, Role> RoleMap
        {
            set
            {
                roleMap = value;
                _RoleMap = roleMap.ToDictionary(m => m.Key.AgentIdx, m => m.Value.ToString());
            }
            get
            {
                return roleMap;
            }
        }

        /// <summary>
        /// The known roles of agents.
        /// </summary>
        /// <value>Dictionary storing the known roles of agents.</value>
        /// <remarks>
        /// If you are human, you know only yourself.
        /// If you are werewolf, you know other werewolves.
        /// </remarks>
        [DataMember(Name = "roleMap")]
        public Dictionary<int, string> _RoleMap { get; private set; } = new Dictionary<int, string>();

        /// <summary>
        /// The list of agents.
        /// </summary>
        /// <value>The list of agents.</value>
        public List<Agent> AgentList
        {
            get
            {
                return new List<Agent>(StatusMap.Keys);
            }
        }

        /// <summary>
        /// The list of alive agents.
        /// </summary>
        /// <value>The list of alive agents.</value>
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
            if (agent < 1)
            {
                throw new AIWolfRuntimeException(GetType() + ": Invalid agent index " + agent + ".");
            }
            if (executedAgent < 1)
            {
                throw new AIWolfRuntimeException(GetType() + ": Invalid executed agent index " + executedAgent + ".");
            }
            if (attackedAgent < 1)
            {
                throw new AIWolfRuntimeException(GetType() + ": Invalid attacked agent index " + attackedAgent + ".");
            }
            if (guardedAgent < 1)
            {
                throw new AIWolfRuntimeException(GetType() + ": Invalid guarded agent index " + guardedAgent + ".");
            }
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
                if (p.Key < 1)
                {
                    throw new AIWolfRuntimeException(GetType() + ": Invalid agent index " + p.Key + ".");
                }
                Status status;
                if (!Enum.TryParse(p.Value, out status))
                {
                    throw new AIWolfRuntimeException(GetType() + ": Invalid status string " + p.Value + ".");
                }
                StatusMap[Agent.GetAgent(p.Key)] = status;
            }

            RoleMap.Clear();
            _RoleMap = roleMap;
            foreach (var p in _RoleMap)
            {
                if (p.Key < 1)
                {
                    throw new AIWolfRuntimeException(GetType() + ": Invalid agent index " + p.Key + ".");
                }
                Role role;
                if (!Enum.TryParse(p.Value, out role))
                {
                    throw new AIWolfRuntimeException(GetType() + ": Invalid role string " + p.Value + ".");
                }
                RoleMap[Agent.GetAgent(p.Key)] = role;
            }
        }
    }
}

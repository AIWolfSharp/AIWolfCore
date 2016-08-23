//
// GameInfoToSend.cs
//
// Copyright (c) 2016 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

using AIWolf.Common.Data;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AIWolf.Common.Net
{
    /// <summary>
    ///  The game information to be sent to each player.
    /// </summary>
    [DataContract]
    public class GameInfoToSend
    {
        /// <summary>
        /// Current day.
        /// </summary>
        /// <value>Current day.</value>
        [DataMember(Name = "day")]
        public int Day { get; set; }

        /// <summary>
        /// The index number of agent who receives this game information.
        /// </summary>
        /// <value>The index number of agent who receives this game information.</value>
        [DataMember(Name = "agent")]
        public int Agent { get; set; }

        /// <summary>
        /// The result of the inquest.
        /// </summary>
        /// <value>JudgeToSend representing the result of inquest.</value>
        /// <remarks>Medium only.</remarks>
        [DataMember(Name = "mediumResult")]
        public Judge MediumResult { get; set; }

        /// <summary>
        /// The result of the divination.
        /// </summary>
        /// <value>JudgeToSend representating the result of divination.</value>
        /// <remarks>Seer only.</remarks>
        [DataMember(Name = "divineResult")]
        public Judge DivineResult { get; set; }

        /// <summary>
        /// The index number of the agent executed last night.
        /// </summary>
        /// <value>The index number of the agent executed last night.</value>
        [DataMember(Name = "executedAgent")]
        public int ExecutedAgent { get; set; } = -1;

        /// <summary>
        /// The index number of the agent attacked last night.
        /// </summary>
        /// <value>The index number of the agent attacked last night.</value>
        [DataMember(Name = "attackedAgent")]
        public int AttackedAgent { get; set; } = -1;

        /// <summary>
        /// The index number of the agent guarded last night.
        /// </summary>
        /// <value>The index number of the agent guarded last night.</value>
        [DataMember(Name = "guardedAgent")]
        public int GuardedAgent { get; set; } = -1;

        /// <summary>
        /// The list of votes for execution.
        /// </summary>
        /// <value>The list of votes for execution.</value>
        /// <remarks>You can see who votes to who.</remarks>
        [DataMember(Name = "voteList")]
        public List<Vote> VoteList { get; set; }

        /// <summary>
        /// The list of votes for attack.
        /// </summary>
        /// <value>The list of votes for attack.</value>
        /// <remarks>Werewolf only.</remarks>
        [DataMember(Name = "attackVoteList")]
        public List<Vote> AttackVoteList { get; set; }

        /// <summary>
        /// The list of today's talks.
        /// </summary>
        /// <value>The list of today's talks.</value>
        [DataMember(Name = "talkList")]
        public List<Talk> TalkList { get; set; }

        /// <summary>
        /// The list of today's whispers.
        /// </summary>
        /// <value>The list of today's whispers.</value>
        /// <remarks>Werewolf only.</remarks>
        [DataMember(Name = "whisperList")]
        public List<Talk> WhisperList { get; set; }

        /// <summary>
        /// The statuses of all agents.
        /// </summary>
        /// <value>Dictionary storing the statuses of all agents.</value>
        [DataMember(Name = "statusMap")]
        public Dictionary<int, string> StatusMap { get; set; }

        /// <summary>
        /// The known roles of agents.
        /// </summary>
        /// <value>Dictionary storing the known roles of agents.</value>
        /// <remarks>
        /// If you are human, you know only yourself.
        /// If you are werewolf, you know other werewolves.
        /// </remarks>
        [DataMember(Name = "roleMap")]
        public Dictionary<int, string> RoleMap { get; set; }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public GameInfoToSend()
        {
            VoteList = new List<Vote>();
            AttackVoteList = new List<Vote>();
            StatusMap = new Dictionary<int, string>();
            RoleMap = new Dictionary<int, string>();
            TalkList = new List<Talk>();
            WhisperList = new List<Talk>();
        }

        /// <summary>
        /// Returns the instance of GameInfo class equivalent to this.
        /// </summary>
        /// <returns>The instance of GameInfo class equivalent to this.</returns>
        public GameInfo ToGameInfo()
        {
            GameInfo gi = new GameInfo();
            gi.Day = Day;
            gi.Agent = Data.Agent.GetAgent(Agent);

            if (MediumResult != null)
            {
                gi.MediumResult = MediumResult;
            }
            if (DivineResult != null)
            {
                gi.DivineResult = DivineResult;
            }
            gi.ExecutedAgent = Data.Agent.GetAgent(ExecutedAgent);
            gi.AttackedAgent = Data.Agent.GetAgent(AttackedAgent);
            gi.GuardedAgent = Data.Agent.GetAgent(GuardedAgent);

            gi.VoteList = new List<Vote>();
            foreach (Vote vote in VoteList)
            {
                gi.VoteList.Add(vote);
            }
            gi.AttackVoteList = new List<Vote>();
            foreach (Vote vote in AttackVoteList)
            {
                gi.AttackVoteList.Add(vote);
            }

            gi.TalkList = new List<Talk>();
            foreach (Talk talk in TalkList)
            {
                gi.TalkList.Add(talk);
            }
            gi.WhisperList = new List<Talk>();
            foreach (Talk whisper in WhisperList)
            {
                gi.WhisperList.Add(whisper);
            }

            gi.StatusMap = new Dictionary<Agent, Status>();
            foreach (int agent in StatusMap.Keys)
            {
                gi.StatusMap.Add(Data.Agent.GetAgent(agent), (Status)Enum.Parse(typeof(Status), StatusMap[agent]));
            }
            gi.RoleMap = new Dictionary<Agent, Role>();
            foreach (int agent in RoleMap.Keys)
            {
                gi.RoleMap.Add(Data.Agent.GetAgent(agent), (Role)Enum.Parse(typeof(Role), RoleMap[agent]));
            }

            return gi;
        }
    }
}

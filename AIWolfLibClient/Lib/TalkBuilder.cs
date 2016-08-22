using AIWolf.Common;
using AIWolf.Common.Data;
using AIWolf.Common.Net;

namespace AIWolf.Client.Lib
{
    /// <summary>
    /// Class to create talk contents.
    /// </summary>
    public class TalkBuilder
    {
        private GameInfo gameInfo;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="gameInfo">The game information.</param>
        public TalkBuilder(GameInfo gameInfo)
        {
            this.gameInfo = gameInfo;
        }

        protected void CheckTarget(string methodName, Agent target)
        {
            if (target == null)
            {
                throw new AIWolfAgentException(GetType().Name + "." + methodName + ": Target is null.");
            }
            if (!gameInfo.AgentList.Contains(target))
            {
                throw new AIWolfAgentException(GetType().Name + "." + methodName + ": Invalid target " + target + ".");
            }
        }

        /// <summary>
        /// Returns the talk about estimation.
        /// </summary>
        /// <param name="target">The agent estimated.</param>
        /// <param name="role">The estimated role.</param>
        /// <returns>The talk about estimation.</returns>
        /// <remarks>If the given target agent is invalid, this method throws AIWolfAgentException.</remarks>
        public string Estimate(Agent target, Role role)
        {
            CheckTarget("Estimate", target);
            return Topic.ESTIMATE.ToString() + " " + target.ToString() + " " + role.ToString();
        }

        /// <summary>
        /// Returns the talk about comingout.
        /// </summary>
        /// <param name="target">The agent who playing the role.</param>
        /// <param name="role">The role which the agent is playing.</param>
        /// <returns>The talk about comingout.</returns>
        /// <remarks>If the given target agent is invalid, this method throws AIWolfAgentException.</remarks>
        public string Comingout(Agent target, Role role)
        {
            CheckTarget("Comingout", target);
            return Topic.COMINGOUT.ToString() + " " + target.ToString() + " " + role.ToString();
        }

        /// <summary>
        /// Returns the talk about divination.
        /// </summary>
        /// <param name="target">The agent divined.</param>
        /// <param name="species">The species which the divined agent is found to be.</param>
        /// <returns>Talk about divination.</returns>
        /// <remarks>If the given target agent is invalid, this method throws AIWolfAgentException.</remarks>
        public string Divined(Agent target, Species species)
        {
            CheckTarget("Divined", target);
            return Topic.DIVINED.ToString() + " " + target.ToString() + " " + species.ToString();
        }

        /// <summary>
        /// Returns the talk about inquest.
        /// </summary>
        /// <param name="target">The agent inquired.</param>
        /// <param name="species">The species which the inquired agent is found to be.</param>
        /// <returns>Talk about inquest.</returns>
        /// <remarks>If the given target agent is invalid, this method throws AIWolfAgentException.</remarks>
        public string Inquested(Agent target, Species species)
        {
            CheckTarget("Inquested", target);
            return Topic.INQUESTED.ToString() + " " + target.ToString() + " " + species.ToString();
        }

        /// <summary>
        /// Returns the talk about guard.
        /// </summary>
        /// <param name="target">The agent guarded.</param>
        /// <returns>Talk about guard.</returns>
        /// <remarks>If the given target agent is invalid, this method throws AIWolfAgentException.</remarks>
        public string Guarded(Agent target)
        {
            CheckTarget("Guarded", target);
            return Topic.GUARDED.ToString() + " " + target.ToString();
        }

        /// <summary>
        /// Returns the talk about vote for execution.
        /// </summary>
        /// <param name="target">The agent the talker wants to execute.</param>
        /// <returns>Talk about vote.</returns>
        /// <remarks>If the given target agent is invalid, this method throws AIWolfAgentException.</remarks>
        public string Vote(Agent target)
        {
            CheckTarget("Vote", target);
            return Topic.VOTE.ToString() + " " + target.ToString();
        }

        /// <summary>
        /// Returns the talk about agreement.
        /// </summary>
        /// <param name="talkType">TALK/WHISPER.</param>
        /// <param name="day">The day of the talk/whisper.</param>
        /// <param name="id">Index number of the talk/whisper.</param>
        /// <returns>Talk about agreement.</returns>
        /// <remarks>If day or id is invalid, this method throws AIWolfAgentException.</remarks>
        public string Agree(TalkType talkType, int day, int id)
        {
            if (day < 0 || day > gameInfo.Day)
            {
                throw new AIWolfAgentException(GetType().Name + ".Agree: Invalid day " + day + ".");
            }
            if (id < 0)
            {
                throw new AIWolfAgentException(GetType().Name + ".Agree: Invalid id " + id + ".");
            }
            return Topic.AGREE.ToString() + " " + talkType.ToString() + " day" + day + " ID:" + id;
        }

        /// <summary>
        /// Returns the talk about disagreement.
        /// </summary>
        /// <param name="talkType">TALK/WHISPER.</param>
        /// <param name="day">The day of the talk/whisper.</param>
        /// <param name="id">Index number of the talk/whisper.</param>
        /// <returns>Talk about disagreement.</returns>
        /// <remarks>If day or id is invalid, this method throws AIWolfAgentException.</remarks>
        public string Disagree(TalkType talkType, int day, int id)
        {
            if (day < 0 || day > gameInfo.Day)
            {
                throw new AIWolfAgentException(GetType().Name + ".Agree: Invalid day " + day + ".");
            }
            if (id < 0)
            {
                throw new AIWolfAgentException(GetType().Name + ".Agree: Invalid id " + id + ".");
            }
            return Topic.DISAGREE.ToString() + " " + talkType.ToString() + " day" + day + " ID:" + id;
        }

        /// <summary>
        /// There is nothing to talk.
        /// </summary>
        /// <returns>String "Over".</returns>
        public string Over()
        {
            return Talk.OVER;
        }

        /// <summary>
        /// Skip this turn though there is something to talk.
        /// </summary>
        /// <returns>String "Skip".</returns>
        public string Skip()
        {
            return Talk.SKIP;
        }
    }
}

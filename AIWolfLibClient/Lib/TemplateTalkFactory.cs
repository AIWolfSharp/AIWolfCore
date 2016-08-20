using AIWolf.Common;
using AIWolf.Common.Data;

namespace AIWolf.Client.Lib
{
    /// <summary>
    /// Factory class to create talk contents.
    /// </summary>
    public class TemplateTalkFactory
    {
        /// <summary>
        /// Returns talk about estimation.
        /// </summary>
        /// <param name="target">The agent estimated.</param>
        /// <param name="role">The estimated role.</param>
        /// <returns>Talk about estimation.</returns>
        /// <remarks>If target is null, this throws AIWolfAgentException.</remarks>
        public static string Estimate(Agent target, Role role)
        {
            if (target == null)
            {
                throw new AIWolfAgentException("TemplateTalkFactory.Estimate: Target is null.");
            }
            return Topic.ESTIMATE.ToString() + " " + target.ToString() + " " + role.ToString();
        }

        /// <summary>
        /// Returns talk about comingout.
        /// </summary>
        /// <param name="target">The agent who playing the role.</param>
        /// <param name="role">The role which the agent is playing.</param>
        /// <returns>Talk about comingout that which role someone is playing.</returns>
        /// <remarks>If target is null, this throws AIWolfAgentException.</remarks>
        public static string Comingout(Agent target, Role role)
        {
            if (target == null)
            {
                throw new AIWolfAgentException("TemplateTalkFactory.Comingout: Target is null.");
            }
            return Topic.COMINGOUT.ToString() + " " + target.ToString() + " " + role.ToString();
        }

        /// <summary>
        /// Returns talk about divination.
        /// </summary>
        /// <param name="target">The agent divined.</param>
        /// <param name="species">The species which the divined agent is found to be.</param>
        /// <returns>Talk about divination.</returns>
        /// <remarks>If target is null, this throws AIWolfAgentException.</remarks>
        public static string Divined(Agent target, Species species)
        {
            if (target == null)
            {
                throw new AIWolfAgentException("TemplateTalkFactory.Divined: Target is null.");
            }
            return Topic.DIVINED.ToString() + " " + target.ToString() + " " + species.ToString();
        }

        /// <summary>
        /// Returns talk about inquest.
        /// </summary>
        /// <param name="target">The agent inquired.</param>
        /// <param name="species">The species which the inquired agent is found to be.</param>
        /// <returns>Talk about inquest.</returns>
        /// <remarks>If target is null, this throws AIWolfAgentException.</remarks>
        public static string Inquested(Agent target, Species species)
        {
            if (target == null)
            {
                throw new AIWolfAgentException("TemplateTalkFactory.Inquested: Target is null.");
            }
            return Topic.INQUESTED.ToString() + " " + target.ToString() + " " + species.ToString();
        }

        /// <summary>
        /// Returns talk about guard.
        /// </summary>
        /// <param name="target">The agent guarded.</param>
        /// <returns>Talk about guard.</returns>
        /// <remarks>If target is null, this throws AIWolfAgentException.</remarks>
        public static string Guarded(Agent target)
        {
            if (target == null)
            {
                throw new AIWolfAgentException("TemplateTalkFactory.Guarded: Target is null.");
            }
            return Topic.GUARDED.ToString() + " " + target.ToString();
        }

        /// <summary>
        /// Returns talk about vote for execution.
        /// </summary>
        /// <param name="target">The agent whom the talker wants to execute.</param>
        /// <returns>Talk about vote for execution.</returns>
        /// <remarks>If target is null, this throws AIWolfAgentException.</remarks>
        public static string Vote(Agent target)
        {
            if (target == null)
            {
                throw new AIWolfAgentException("TemplateTalkFactory.Vote: Target is null.");
            }
            return Topic.VOTE.ToString() + " " + target.ToString();
        }

        /// <summary>
        /// Returns talk about agreement.
        /// </summary>
        /// <param name="talkType">TALK/WHISPER.</param>
        /// <param name="day">The day of talk/whisper.</param>
        /// <param name="id">Index number of talk/whisper.</param>
        /// <returns>Talk about agreement.</returns>
        /// <remarks>If day or id is negative, this throws AIWolfAgentException.</remarks>
        public static string Agree(TalkType talkType, int day, int id)
        {
            if (day < 0)
            {
                throw new AIWolfAgentException("TemplateTalkFactory.Agree: Invalid day " + day + ".");
            }
            if (id < 0)
            {
                throw new AIWolfAgentException("TemplateTalkFactory.Agree: Invalid id " + id + ".");
            }
            return Topic.AGREE.ToString() + " " + talkType.ToString() + " day" + day + " ID:" + id;
        }

        /// <summary>
        /// Returns talk about disagreement.
        /// </summary>
        /// <param name="talkType">TALK/WHISPER.</param>
        /// <param name="day">The day of talk/whisper.</param>
        /// <param name="id">Index number of talk/whisper.</param>
        /// <returns>Talk about disagreement.</returns>
        /// <remarks>If day or id is negative, this throws AIWolfAgentException.</remarks>
        public static string Disagree(TalkType talkType, int day, int id)
        {
            if (day < 0)
            {
                throw new AIWolfAgentException("TemplateTalkFactory.Disagree: Invalid day " + day + ".");
            }
            if (id < 0)
            {
                throw new AIWolfAgentException("TemplateTalkFactory.Disagree: Invalid id " + id + ".");
            }
            return Topic.DISAGREE.ToString() + " " + talkType.ToString() + " day" + day + " ID:" + id;
        }

        /// <summary>
        /// There is nothing to talk.
        /// </summary>
        /// <returns>String "Over".</returns>
        /// <remarks></remarks>
        public static string Over()
        {
            return Talk.OVER;
        }

        /// <summary>
        /// Skip this turn though there is something to talk.
        /// </summary>
        /// <returns>String "Skip".</returns>
        /// <remarks></remarks>
        public static string Skip()
        {
            return Talk.SKIP;
        }
    }
}

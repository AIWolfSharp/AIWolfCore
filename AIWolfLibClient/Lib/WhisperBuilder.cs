using AIWolf.Common;
using AIWolf.Common.Data;

namespace AIWolf.Client.Lib
{
    /// <summary>
    /// Class to create whisper contents.
    /// </summary>
    public class WhisperBuilder
    {
        /// <summary>
        /// Returns whisper about attack.
        /// </summary>
        /// <param name="target">The agent whom the whisperer wants to attack.</param>
        /// <returns>Whisper about attack.</returns>
        /// <remarks>If target is null, this throws AIWolfAgentException.</remarks>
        public static string Attack(Agent target)
        {
            if (target == null)
            {
                throw new AIWolfAgentException("TemplateWhisperFactory.Attack: Target is null.");
            }
            return Topic.ATTACK.ToString() + " " + target.ToString();
        }

        /// <summary>
        /// Returns whisper about estimation.
        /// </summary>
        /// <param name="target">The agent estimated.</param>
        /// <param name="role">The estimated role.</param>
        /// <returns>Whisper about estimation.</returns>
        /// <remarks>If target is null, this throws AIWolfAgentException.</remarks>
        public static string Estimate(Agent target, Role role)
        {
            if (target == null)
            {
                throw new AIWolfAgentException("TemplateWhisperFactory.Estimate: Target is null.");
            }
            return Topic.ESTIMATE.ToString() + " " + target.ToString() + " " + role.ToString();
        }

        /// <summary>
        /// Returns whisper about comingout.
        /// </summary>
        /// <param name="target">The agent who playing the role.</param>
        /// <param name="role">The role which the agent is playing.</param>
        /// <returns>Whisper about comingout that which role someone is playing.</returns>
        /// <remarks>If target is null, this throws AIWolfAgentException.</remarks>
        public static string Comingout(Agent target, Role role)
        {
            if (target == null)
            {
                throw new AIWolfAgentException("TemplateWhisperFactory.Comingout: Target is null.");
            }
            return Topic.COMINGOUT.ToString() + " " + target.ToString() + " " + role.ToString();
        }

        /// <summary>
        /// Returns whisper about divination.
        /// </summary>
        /// <param name="target">The agent divined.</param>
        /// <param name="species">The species which the divined agent is found to be.</param>
        /// <returns>Whisper about divination.</returns>
        /// <remarks>If target is null, this throws AIWolfAgentException.</remarks>
        public static string Divined(Agent target, Species species)
        {
            if (target == null)
            {
                throw new AIWolfAgentException("TemplateWhisperFactory.Divined: Target is null.");
            }
            return Topic.DIVINED.ToString() + " " + target.ToString() + " " + species.ToString();
        }

        /// <summary>
        /// Returns whisper about inquest.
        /// </summary>
        /// <param name="target">The agent inquired.</param>
        /// <param name="species">The species which the inquired agent is found to be.</param>
        /// <returns>Whisper about inquest.</returns>
        /// <remarks>If target is null, this throws AIWolfAgentException.</remarks>
        public static string Inquested(Agent target, Species species)
        {
            if (target == null)
            {
                throw new AIWolfAgentException("TemplateWhisperFactory.Inquested: Target is null.");
            }
            return Topic.INQUESTED.ToString() + " " + target.ToString() + " " + species.ToString();
        }

        /// <summary>
        /// Returns whisper about guard.
        /// </summary>
        /// <param name="target">The agent guarded.</param>
        /// <returns>Whisper about guard.</returns>
        /// <remarks>If target is null, this throws AIWolfAgentException.</remarks>
        public static string Guarded(Agent target)
        {
            if (target == null)
            {
                throw new AIWolfAgentException("TemplateWhisperFactory.Guarded: Target is null.");
            }

            string[] split = { Topic.GUARDED.ToString(), (target != null) ? target.ToString() : "null" };
            return Topic.GUARDED.ToString() + " " + target.ToString();
        }

        /// <summary>
        /// Returns whisper about vote for execution.
        /// </summary>
        /// <param name="target">The agent whom the whisperer wants to execute.</param>
        /// <returns>Whisper about vote for execution.</returns>
        /// <remarks>If target is null, this throws AIWolfAgentException.</remarks>
        public static string Vote(Agent target)
        {
            if (target == null)
            {
                throw new AIWolfAgentException("TemplateWhisperFactory.Vote: Target is null.");
            }
            return Topic.VOTE.ToString() + " " + target.ToString();
        }

        /// <summary>
        /// Returns whisper about agreement.
        /// </summary>
        /// <param name="talkType">TALK/WHISPER.</param>
        /// <param name="day">The day of talk/whisper.</param>
        /// <param name="id">Index number of talk/whisper.</param>
        /// <returns>Whisper about agreement.</returns>
        /// <remarks>If day or id is negative, this throws AIWolfAgentException.</remarks>
        public static string Agree(TalkType talkType, int day, int id)
        {
            if (day < 0)
            {
                throw new AIWolfAgentException("TemplateWhisperFactory.Agree: Invalid day " + day + ".");
            }
            if (id < 0)
            {
                throw new AIWolfAgentException("TemplateWhisperFactory.Agree: Invalid id " + id + ".");
            }
            return Topic.AGREE.ToString() + " " + talkType.ToString() + " day" + day + " ID:" + id;
        }

        /// <summary>
        /// Returns whisper about disagreement.
        /// </summary>
        /// <param name="talkType">TALK/WHISPER.</param>
        /// <param name="day">The day of talk/whisper.</param>
        /// <param name="id">Index number of talk/whisper.</param>
        /// <returns>Whisper about disagreement.</returns>
        /// <remarks>If day or id is negative, this throws AIWolfAgentException.</remarks>
        public static string Disagree(TalkType talkType, int day, int id)
        {
            if (day < 0)
            {
                throw new AIWolfAgentException("TemplateWhisperFactory.Disagree: Invalid day " + day + ".");
            }
            if (id < 0)
            {
                throw new AIWolfAgentException("TemplateWhisperFactory.Disagree: Invalid id " + id + ".");
            }
            return Topic.DISAGREE.ToString() + " " + talkType.ToString() + " day" + day + " ID:" + id;
        }

        /// <summary>
        /// There is nothing to whisper.
        /// </summary>
        /// <returns>String "Over".</returns>
        public static string Over()
        {
            return Talk.OVER;
        }

        /// <summary>
        /// Skip this turn though there is something to whisper.
        /// </summary>
        /// <returns>String "Skip".</returns>
        public static string Skip()
        {
            return Talk.SKIP;
        }
    }
}

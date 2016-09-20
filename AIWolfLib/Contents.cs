//
// Contents.cs
//
// Copyright (c) 2016 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//


namespace AIWolf.Lib
{
    /// <summary>
    /// Contents of utterance.
    /// </summary>
    public class Contents
    {
        /// <summary>
        /// The topic of this utterance.
        /// </summary>
        /// <remarks>DUMMY means invalid utterance.</remarks>
        public Topic Topic { get; } = Topic.DUMMY;

        /// <summary>
        /// The target agent mentioned in this utterance.
        /// </summary>
        /// <remarks>Required except AGREE and DISAGREE.</remarks>
        public Agent Target { get; }

        /// <summary>
        /// The role mentioned in this utterance.
        /// </summary>
        /// <remarks>Required on ESTIMATE and COMINGOUT.</remarks>
        public Role Role { get; } = Role.UNC;

        /// <summary>
        /// The species mentioned in this utterance.
        /// </summary>
        /// <remarks>Required on DIVINED and INQUESTED.</remarks>
        public Species Species { get; } = Species.UNC;

        /// <summary>
        /// The utterance mentioned in this utterance.
        /// </summary>
        /// <remarks>Required on AGREE and DISAGREE.</remarks>
        public Utterance Utterance { get; }

        /// <summary>
        /// Initializes a new instance of contents having topic of skip and over.
        /// </summary>
        /// <param name="topic">The topic of this contents.</param>
        internal Contents(Topic topic)
        {
            if (topic == Topic.Skip || topic == Topic.Over)
            {
                Topic = topic;
            }
            else
            {
                Error.RuntimeError("Can not initialize by this constructor in case of " + topic + ".");
                Error.Warning("Force topic to be DUMMY.");
            }
        }

        /// <summary>
        /// Initializes a new instance of contents having topic of estimation and comingout.
        /// </summary>
        /// <param name="topic">The topic of this contents.</param>
        /// <param name="target">The target agent mentioned in this contents.</param>
        /// <param name="role">The role of the target.</param>
        internal Contents(Topic topic, Agent target, Role role)
        {
            if (topic == Topic.ESTIMATE || topic == Topic.COMINGOUT)
            {
                Topic = topic;
                Target = target;
                Role = role;
            }
            else
            {
                Error.RuntimeError("Can not initialize by this constructor in case of " + topic + ".");
                Error.Warning("Force topic to be DUMMY.");
            }
        }

        /// <summary>
        /// Initializes a new instance of contents having topic of divination and inquest.
        /// </summary>
        /// <param name="topic">The topic of this contents.</param>
        /// <param name="target">The target agent mentioned in this contents.</param>
        /// <param name="species">The species of the target.</param>
        internal Contents(Topic topic, Agent target, Species species)
        {
            if (topic == Topic.DIVINED || topic == Topic.INQUESTED)
            {
                Topic = topic;
                Target = target;
                Species = species;
            }
            else
            {
                Error.RuntimeError("Can not initialize by this constructor in case of " + topic + ".");
                Error.Warning("Force topic to be DUMMY.");
            }
        }

        /// <summary>
        /// Initializes a new instance of contents having topic of attack, guard and vote.
        /// </summary>
        /// <param name="topic">The topic of this contents.</param>
        /// <param name="target">The target agent mentioned in this contents.</param>
        internal Contents(Topic topic, Agent target)
        {
            if (topic == Topic.ATTACK || topic == Topic.GUARDED || topic == Topic.VOTE)
            {
                Topic = topic;
                Target = target;
            }
            else
            {
                Error.RuntimeError("Can not initialize by this constructor in case of " + topic + ".");
                Error.Warning("Force topic to be DUMMY.");
            }
        }

        /// <summary>
        /// Initializes a new instance of contents having topic of agreement and disagreement.
        /// </summary>
        /// <param name="topic">The topic of this contents.</param>
        /// <param name="utterance">The utterance which the talker agrees/disagrees with.</param>
        internal Contents(Topic topic, Utterance utterance)
        {
            if (topic == Topic.AGREE || topic == Topic.DISAGREE)
            {
                Topic = topic;
                Utterance = utterance;
            }
            else
            {
                Error.RuntimeError("Can not initialize by this constructor in case of " + topic + ".");
                Error.Warning("Force topic to be DUMMY.");
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            switch (Topic)
            {
                case Topic.DUMMY:
                case Topic.Skip:
                case Topic.Over:
                    return Topic.ToString();
                case Topic.ESTIMATE:
                case Topic.COMINGOUT:
                    return Topic + ": target=" + Target + " role=" + Role;
                case Topic.DIVINED:
                case Topic.INQUESTED:
                    return Topic + ": target=" + Target + " species=" + Species;
                case Topic.GUARDED:
                case Topic.VOTE:
                case Topic.ATTACK:
                    return Topic + ": target=" + Target;
                case Topic.AGREE:
                case Topic.DISAGREE:
                    return Topic + ": utterance=" + Utterance;
                default:
                    return "";
            }
        }
    }
}

//
// Talk.cs
//
// Copyright (c) 2016 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace AIWolf.Lib
{
    /// <summary>
    /// Talk/whisper.
    /// </summary>
    [DataContract]
    public class Talk
    {
        Topic topic;

        /// <summary>
        /// There is nothing to talk/whisper.
        /// </summary>
        public const string Over = "Over";

        /// <summary>
        /// Skip this turn though there is something to talk/whisper.
        /// </summary>
        public const string Skip = "Skip";

        /// <summary>
        /// The index number of this talk/whisper.
        /// </summary>
        [DataMember(Name = "idx")]
        public int Idx { get; }

        /// <summary>
        /// The day of this talk/whisper.
        /// </summary>
        [DataMember(Name = "day")]
        public int Day { get; }

        /// <summary>
        /// The agent who talked/whispered.
        /// </summary>
        public Agent Agent { get; }

        /// <summary>
        /// The index number of the agent who talked/whispered.
        /// </summary>
        [DataMember(Name = "agent")]
        public int _Agent { get; }

        /// <summary>
        /// The contents of this talk/whisper.
        /// </summary>
        [DataMember(Name = "content")]
        public string Text { get; }

        /// <summary>
        /// The topic of this talk/whisper.
        /// </summary>
        public Topic Topic
        {
            get
            {
                return topic;
            }
        }

        /// <summary>
        /// The contents of this talk/whisper.
        /// </summary>
        /// <remarks>
        /// If talk/whisper is invalid, Contents.Topic is set to DUMMY.
        /// </remarks>
        public Contents Contents { get; }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="idx">The index of this talk/whisper.</param>
        /// <param name="day">The day of this talk/whisper.</param>
        public Talk(int idx, int day)
        {
            Idx = idx;
            if (Idx < 0)
            {
                Error.RuntimeError(GetType() + "(): Invalid idx " + Idx + ".", "Force it to be 0.");
                Idx = 0;
            }

            Day = day;
            if (Day < 0)
            {
                Error.RuntimeError(GetType() + "(): Invalid day " + Day + ".", "Force it to be 0.");
                Day = 0;
            }
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="idx">The index of this talk/whisper.</param>
        /// <param name="day">The day of this talk/whisper.</param>
        /// <param name="agent">The agent who talked/whispered.</param>
        /// <param name="text">The text of this talk/whisper.</param>
        public Talk(int idx, int day, Agent agent, string text) : this(idx, day)
        {
            Agent = agent;
            if (Agent == null)
            {
                Error.RuntimeError(GetType() + "(): Agent must not be null.", "Force it to be Agent[00].");
                Agent = Agent.GetAgent(0);
            }
            _Agent = Agent.AgentIdx;

            Text = text;
            Contents = ParseText();
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="idx">The index of this talk/whisper.</param>
        /// <param name="day">The day of this talk/whisper.</param>
        /// <param name="agent">The index of agent who talked/whispered.</param>
        /// <param name="text">The text of this talk/whisper.</param>
        [JsonConstructor]
        public Talk(int idx, int day, int agent, string text) : this(idx, day, Agent.GetAgent(agent), text)
        {
        }

        /// <summary>
        /// Parses the text of this talk/whisper.
        /// </summary>
        /// <returns>Contents of this talk/whisper.</returns>
        /// <remarks>Returns null if the content is invalid.</remarks>
        Contents ParseText()
        {
            string[] sentence;

            if (Text == null || Text.Length == 0)
            {
                Error.RuntimeError(GetType() + ".ParseContent(): Content is empty or null.", "Force the meaning to be null.");
                return null;
            }

            sentence = Text.Split();
            if (!Enum.TryParse(sentence[0], out topic))
            {
                Error.RuntimeError(GetType() + ".ParseContent(): Can not find any topic in content " + Text + ".", "Force the meaning to be null.");
                return null;
            }

            switch (sentence.Length)
            {
                case 1:
                    if (topic == Topic.Skip || topic == Topic.Over)
                    {
                        return new Contents(topic);
                    }
                    Error.RuntimeError(GetType() + ".ParseContent(): Illegal content " + Text + ".", "Force the meaning to be null.");
                    return null;
                case 2:
                    int targetId = GetInt(sentence[1]);
                    Agent target = Agent.GetAgent(targetId);
                    if (topic == Topic.ATTACK || topic == Topic.GUARDED || topic == Topic.VOTE)
                    {
                        return new Contents(topic, target);
                    }
                    Error.RuntimeError(GetType() + ".ParseContent(): Illegal content " + Text + ".", "Force the meaning to be null.");
                    return null;
                case 3:
                    targetId = GetInt(sentence[1]);
                    if (targetId < 1)
                    {
                        Error.RuntimeError(GetType() + ".ParseContent(): Illegal content " + Text + ".", "Force the meaning to be null.");
                        return null;
                    }
                    target = Agent.GetAgent(targetId);
                    if (topic == Topic.ESTIMATE || topic == Topic.COMINGOUT)
                    {
                        Role role;
                        if (!Enum.TryParse(sentence[2], out role))
                        {
                            Error.RuntimeError(GetType() + ".ParseContent(): Illegal content " + Text + ".", "Force the meaning to be null.");
                            return null;
                        }
                        return new Contents(topic, target, role);
                    }
                    if (topic == Topic.DIVINED || topic == Topic.INQUESTED)
                    {
                        Species species;
                        if (!Enum.TryParse(sentence[2], out species))
                        {
                            Error.RuntimeError(GetType() + ".ParseContent(): Illegal content " + Text + ".", "Force the meaning to be null.");
                            return null;
                        }
                        return new Contents(topic, target, species);
                    }
                    Error.RuntimeError(GetType() + ".ParseContent(): Illegal content " + Text + ".", "Force the meaning to be null.");
                    return null;
                case 4:
                    if (topic == Topic.AGREE || topic == Topic.DISAGREE)
                    {
                        int day = GetInt(sentence[2]);
                        int id = GetInt(sentence[3]);
                        if (day < 0 || id < 0)
                        {
                            Error.RuntimeError(GetType() + ".ParseContent(): Illegal content " + Text + ".", "Force the meaning to be null.");
                            return null;
                        }
                        if (sentence[1].Equals("TALK"))
                        {
                            return new Contents(topic, new Talk(id, day));
                        }
                        else if (sentence[1].Equals("WHISPER"))
                        {
                            return new Contents(topic, new Whisper(id, day));
                        }
                        else
                        {
                            Error.RuntimeError(GetType() + ".ParseContent(): Illegal content " + Text + ".", "Force the meaning to be null.");
                            return null;
                        }
                    }
                    Error.RuntimeError(GetType() + ".ParseContent(): Illegal content " + Text + ".", "Force the meaning to be null.");
                    return null;
                default:
                    break;
            }
            Error.RuntimeError(GetType() + ".ParseContent(): Illegal content " + Text + ".", "Force the meaning to be null.");
            return null;
        }

        /// <summary>
        /// Finds integer value from the given text.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <returns>Integer value if found, otherwise -1.</returns>
        int GetInt(string text)
        {
            var m = new Regex(@"-?[\d]+").Match(text);
            if (m.Success)
            {
                return int.Parse(m.Value);
            }
            return -1;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return String.Format("Day{0:D2}[{1:D3}]\t{2}\t{3}", Day, Idx, Agent, Text);
        }
    }
}

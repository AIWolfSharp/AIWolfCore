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
        public static string Over { get; } = "Over";

        /// <summary>
        /// Skip this turn though there is something to talk/whisper.
        /// </summary>
        public static string Skip { get; } = "Skip";

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
        /// The meaning of this talk/whisper.
        /// </summary>
        /// <remarks>
        /// Null means invalid talk.
        /// </remarks>
        public object Contents { get; }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="idx">The index of this talk/whisper.</param>
        /// <param name="day">The day of this talk/whisper.</param>
        /// <param name="agent">The agent who talked/whispered.</param>
        /// <param name="content">The contents of this talk/whisper.</param>
        public Talk(int idx, int day, Agent agent, string content)
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

            Agent = agent;
            if (Agent == null)
            {
                Error.RuntimeError(GetType() + "(): Agent must not be null.", "Force it to be Agent[00].");
                Agent = Agent.GetAgent(0);
            }
            _Agent = Agent.AgentIdx;

            Text = content;
            Contents = ParseContent();
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="idx">The index of this talk/whisper.</param>
        /// <param name="day">The day of this talk/whisper.</param>
        /// <param name="agent">The index of agent who talked/whispered.</param>
        /// <param name="content">The contents of this talk/whisper.</param>
        [JsonConstructor]
        public Talk(int idx, int day, int agent, string content) : this(idx, day, Agent.GetAgent(agent), content)
        {
        }

        /// <summary>
        /// Parses content of this talk/whisper.
        /// </summary>
        /// <returns>An object representing the meaning of this talk/whisper.</returns>
        /// <remarks>Returns null if the content is invalid.</remarks>
        object ParseContent()
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
                        return Text;
                    }
                    Error.RuntimeError(GetType() + ".ParseContent(): Illegal content " + Text + ".", "Force the meaning to be null.");
                    return null;
                case 2:
                    int targetId = GetInt(sentence[1]);
                    Agent target = Agent.GetAgent(targetId);
                    if (topic == Topic.ATTACK)
                    {
                        return new Attack(target);
                    }
                    if (topic == Topic.GUARDED)
                    {
                        return new Guarded(target);
                    }
                    if (topic == Topic.VOTE)
                    {
                        return new Vote(target);
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
                    if (topic == Topic.ESTIMATE)
                    {
                        Role role;
                        if (!Enum.TryParse(sentence[2], out role))
                        {
                            Error.RuntimeError(GetType() + ".ParseContent(): Illegal content " + Text + ".", "Force the meaning to be null.");
                            return null;
                        }
                        return new Estimate(target, role);
                    }
                    if (topic == Topic.COMINGOUT)
                    {
                        Role role;
                        if (!Enum.TryParse(sentence[2], out role))
                        {
                            Error.RuntimeError(GetType() + ".ParseContent(): Illegal content " + Text + ".", "Force the meaning to be null.");
                            return null;
                        }
                        return new Comingout(target, role);
                    }
                    if (topic == Topic.DIVINED)
                    {
                        Species species;
                        if (!Enum.TryParse(sentence[2], out species))
                        {
                            Error.RuntimeError(GetType() + ".ParseContent(): Illegal content " + Text + ".", "Force the meaning to be null.");
                            return null;
                        }
                        return new Divined(target, species);
                    }
                    if (topic == Topic.INQUESTED)
                    {
                        Species species;
                        if (!Enum.TryParse(sentence[2], out species))
                        {
                            Error.RuntimeError(GetType() + ".ParseContent(): Illegal content " + Text + ".", "Force the meaning to be null.");
                            return null;
                        }
                        return new Inquested(target, species);
                    }
                    Error.RuntimeError(GetType() + ".ParseContent(): Illegal content " + Text + ".", "Force the meaning to be null.");
                    return null;
                case 4:
                    if (topic == Topic.AGREE || topic == Topic.DISAGREE)
                    {
                        bool isWhisper;
                        if (sentence[1].Equals("TALK"))
                        {
                            isWhisper = false;
                        }
                        else if (sentence[1].Equals("WHISPER"))
                        {
                            isWhisper = true;
                        }
                        else
                        {
                            Error.RuntimeError(GetType() + ".ParseContent(): Illegal content " + Text + ".", "Force the meaning to be null.");
                            return null;
                        }
                        int day = GetInt(sentence[2]);
                        int id = GetInt(sentence[3]);
                        if (day < 0 || id < 0)
                        {
                            Error.RuntimeError(GetType() + ".ParseContent(): Illegal content " + Text + ".", "Force the meaning to be null.");
                            return null;
                        }
                        if (topic == Topic.AGREE)
                        {
                            return new Agree(isWhisper, day, id);
                        }
                        else // DISAGREE
                        {
                            return new Disagree(isWhisper, day, id);
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

        /// <summary>
        /// Talk/whisper about estimation.
        /// </summary>
        public struct Estimate
        {
            public Agent Target { get; }
            public Role Role { get; }

            public Estimate(Agent target, Role role)
            {
                Target = target;
                Role = role;
            }
        }

        /// <summary>
        /// Talk/whisper about comingout.
        /// </summary>
        public struct Comingout
        {
            public Agent Target { get; }
            public Role Role { get; }

            public Comingout(Agent target, Role role)
            {
                Target = target;
                Role = role;
            }
        }

        /// <summary>
        /// Talk/whisper about divination.
        /// </summary>
        public struct Divined
        {
            public Agent Target { get; }
            public Species Species { get; }

            public Divined(Agent target, Species species)
            {
                Target = target;
                Species = species;
            }
        }

        /// <summary>
        /// Talk/whisper about inquest. 
        /// </summary>
        public struct Inquested
        {
            public Agent Target { get; }
            public Species Species { get; }

            public Inquested(Agent target, Species species)
            {
                Target = target;
                Species = species;
            }
        }

        /// <summary>
        /// Talk/whisper about guard.
        /// </summary>
        public struct Guarded
        {
            public Agent Target { get; }

            public Guarded(Agent target)
            {
                Target = target;
            }
        }

        /// <summary>
        /// Talk/whisper about attack.
        /// </summary>
        public struct Attack
        {
            public Agent Target { get; }

            public Attack(Agent target)
            {
                Target = target;
            }
        }

        /// <summary>
        /// Talk/whisper about vote.
        /// </summary>
        public struct Vote
        {
            public Agent Target { get; }

            public Vote(Agent target)
            {
                Target = target;
            }
        }

        /// <summary>
        /// Talk/whisper about agreement.
        /// </summary>
        public struct Agree
        {
            public bool IsWhisper { get; }
            public int Day { get; }
            public int Id { get; }

            public Agree(bool isWhisper, int day, int id)
            {
                IsWhisper = isWhisper;
                Day = day;
                Id = id;
            }
        }

        /// <summary>
        /// Talk/whisper about disagreement.
        /// </summary>
        public struct Disagree
        {
            public bool IsWhisper { get; }
            public int Day { get; }
            public int Id { get; }

            public Disagree(bool isWhisper, int day, int id)
            {
                IsWhisper = isWhisper;
                Day = day;
                Id = id;
            }
        }
    }
}

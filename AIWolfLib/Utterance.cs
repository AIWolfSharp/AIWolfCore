//
// Utterance.cs
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
#if JHELP
    /// <summary>
    /// 発話抽象クラス
    /// </summary>
#else
    /// <summary>
    /// Abstract utterance class.
    /// </summary>
#endif
    [DataContract]
    public abstract class Utterance
    {
#if JHELP
        /// <summary>
        /// 発話することがない
        /// </summary>
#else
        /// <summary>
        /// There is nothing to utter.
        /// </summary>
#endif
        public const string Over = "Over";

#if JHELP
        /// <summary>
        /// 発話することはあるがこのターンはスキップ
        /// </summary>
#else
        /// <summary>
        /// Skip this turn though there is something to utter.
        /// </summary>
#endif
        public const string Skip = "Skip";

#if JHELP
        /// <summary>
        /// この発話のインデックス番号
        /// </summary>
#else
        /// <summary>
        /// The index number of this utterance.
        /// </summary>
#endif
        [DataMember(Name = "idx")]
        public int Idx { get; }

#if JHELP
        /// <summary>
        /// この発話の日
        /// </summary>
#else
        /// <summary>
        /// The day of this utterance.
        /// </summary>
#endif
        [DataMember(Name = "day")]
        public int Day { get; }

#if JHELP
        /// <summary>
        /// 発話したエージェント
        /// </summary>
#else
        /// <summary>
        /// The agent who uttered.
        /// </summary>
#endif
        public Agent Agent { get; }

        /// <summary>
        /// The index number of the agent who uttered.
        /// </summary>
        [DataMember(Name = "agent")]
        int _Agent { get; }

#if JHELP
        /// <summary>
        /// この発話の内容文字列
        /// </summary>
#else
        /// <summary>
        /// The contents of this utterance.
        /// </summary>
#endif
        [DataMember(Name = "content")]
        public string Text { get; }

#if JHELP
        /// <summary>
        /// この発話のトピック
        /// </summary>
#else
        /// <summary>
        /// The topic of this utterance.
        /// </summary>
#endif
        public Topic Topic { get; }

#if JHELP
        /// <summary>
        /// この発話の内容
        /// </summary>
        /// <remarks>
        /// 不正発話の場合Contents.TopicはDUMMYにセットされる
        /// </remarks>
#else
        /// <summary>
        /// The contents of this utterance.
        /// </summary>
        /// <remarks>
        /// If this utterance is invalid, Contents.Topic is set to DUMMY.
        /// </remarks>
#endif
        public Contents Contents { get; }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="idx">The index of this utterance.</param>
        /// <param name="day">The day of this utterance.</param>
        protected Utterance(int idx, int day)
        {
            Idx = idx;
            if (Idx < 0)
            {
                Error.RuntimeError("Invalid idx " + Idx + ".");
                Idx = 0;
                Error.Warning("Force it to be " + Idx + ".");
            }

            Day = day;
            if (Day < 0)
            {
                Error.RuntimeError("Invalid day " + Day + ".");
                Day = 0;
                Error.Warning("Force it to be " + Day + ".");
            }
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="idx">The index of this utterance.</param>
        /// <param name="day">The day of this utterance.</param>
        /// <param name="agent">The agent who uttered.</param>
        /// <param name="text">The text of this utterance.</param>
        protected Utterance(int idx, int day, Agent agent, string text) : this(idx, day)
        {
            Agent = agent;
            if (Agent == null)
            {
                Error.RuntimeError("Agent must not be null.");
                Agent = Agent.GetAgent(0);
                Error.Warning("Force it to be " + Agent + ".");
            }
            _Agent = Agent.AgentIdx;

            Text = text;
            Contents = ParseText(Text);
            Topic = Contents.Topic;
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="idx">The index of this utterance.</param>
        /// <param name="day">The day of this utterance.</param>
        /// <param name="agent">The index of agent who uttered.</param>
        /// <param name="text">The text of this utterance.</param>
        [JsonConstructor]
        protected Utterance(int idx, int day, int agent, string text) : this(idx, day, Agent.GetAgent(agent), text)
        {
        }

#if JHELP
        /// <summary>
        /// 発話文字列を解析する
        /// </summary>
        /// <param name="text">解析する発話文字列</param>
        /// <returns>発話内容</returns>
        /// <remarks>不正文字列の場合nullを返す</remarks>
#else
        /// <summary>
        /// Parses the text of utterance.
        /// </summary>
        /// <param name="text">The text of utterance to be parsed.</param>
        /// <returns>Contents of this utterance.</returns>
        /// <remarks>Returns null if the text is invalid.</remarks>
#endif
        public static Contents ParseText(string text)
        {
            if (text == null || text.Length == 0)
            {
                Error.RuntimeError("Text is empty or null.");
                Error.Warning("Force the contents to be null.");
                return null;
            }

            string[] sentence = text.Split();
            Topic topic;
            if (!Enum.TryParse(sentence[0], out topic))
            {
                Error.RuntimeError("Can not find any topic in text " + text + ".");
                Error.Warning("Force the contents to be null.");
                return null;
            }

            switch (sentence.Length)
            {
                case 1:
                    if (topic == Topic.Skip || topic == Topic.Over)
                    {
                        return new Contents(topic);
                    }
                    Error.RuntimeError("Illegal text " + text + ".");
                    Error.Warning("Force the contents to be null.");
                    return null;
                case 2:
                    int targetId = GetInt(sentence[1]);
                    Agent target = Agent.GetAgent(targetId);
                    if (topic == Topic.ATTACK || topic == Topic.GUARDED || topic == Topic.VOTE)
                    {
                        return new Contents(topic, target);
                    }
                    Error.RuntimeError("Illegal text " + text + ".");
                    Error.Warning("Force the contents to be null.");
                    return null;
                case 3:
                    targetId = GetInt(sentence[1]);
                    if (targetId < 1)
                    {
                        Error.RuntimeError("Illegal text " + text + ".");
                        Error.Warning("Force the contents to be null.");
                        return null;
                    }
                    target = Agent.GetAgent(targetId);
                    if (topic == Topic.ESTIMATE || topic == Topic.COMINGOUT)
                    {
                        Role role;
                        if (!Enum.TryParse(sentence[2], out role))
                        {
                            Error.RuntimeError("Illegal text " + text + ".");
                            Error.Warning("Force the contents to be null.");
                            return null;
                        }
                        return new Contents(topic, target, role);
                    }
                    if (topic == Topic.DIVINED || topic == Topic.INQUESTED)
                    {
                        Species species;
                        if (!Enum.TryParse(sentence[2], out species))
                        {
                            Error.RuntimeError("Illegal text " + text + ".");
                            Error.Warning("Force the contents to be null.");
                            return null;
                        }
                        return new Contents(topic, target, species);
                    }
                    Error.RuntimeError("Illegal text " + text + ".");
                    Error.Warning("Force the contents to be null.");
                    return null;
                case 4:
                    if (topic == Topic.AGREE || topic == Topic.DISAGREE)
                    {
                        int day = GetInt(sentence[2]);
                        int id = GetInt(sentence[3]);
                        if (day < 0 || id < 0)
                        {
                            Error.RuntimeError("Illegal text " + text + ".");
                            Error.Warning("Force the contents to be null.");
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
                            Error.RuntimeError("Illegal text " + text + ".");
                            Error.Warning("Force the contents to be null.");
                            return null;
                        }
                    }
                    Error.RuntimeError("Illegal text " + text + ".");
                    Error.Warning("Force the contents to be null.");
                    return null;
                default:
                    break;
            }
            Error.RuntimeError("Illegal text " + text + ".");
            Error.Warning("Force the contents to be null.");
            return null;
        }

        /// <summary>
        /// Finds integer value from the given text.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <returns>Integer value if found, otherwise -1.</returns>
        static int GetInt(string text)
        {
            var m = new Regex(@"-?[\d]+").Match(text);
            if (m.Success)
            {
                return int.Parse(m.Value);
            }
            return -1;
        }

#if JHELP
        /// <summary>
        /// このオブジェクトを表す文字列を返す
        /// </summary>
        /// <returns>このオブジェクトを表す文字列</returns>
#else
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
#endif
        public abstract override string ToString();
    }
}

//
// Content.cs
//
// Copyright (c) 2017 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//


using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AIWolf.Lib
{
#if JHELP
    /// <summary>
    /// Contentクラス
    /// </summary>
#else
    /// <summary>
    /// Content class.
    /// </summary>
#endif
    public class Content
    {
#if JHELP
        /// <summary>
        /// Contentのテキスト表現
        /// </summary>
#else
        /// <summary>
        /// The text representation of the Content.
        /// </summary>
#endif
        public string Text { get; internal set; }

#if JHELP
        /// <summary>
        /// Contentのトピック
        /// </summary>
#else
        /// <summary>
        /// The topic of the Content.
        /// </summary>
#endif
        public Topic Topic { get; }

#if JHELP
        /// <summary>
        /// Contentの主語
        /// </summary>
#else
        /// <summary>
        /// The subject of the Content.
        /// </summary>
#endif
        public Agent Subject { get; internal set; }

#if JHELP
        /// <summary>
        /// Contentの対象エージェント
        /// </summary>
#else
        /// <summary>
        /// The target agent of the Content.
        /// </summary>
#endif
        public Agent Target { get; }

#if JHELP
        /// <summary>
        /// Contentが言及する役職
        /// </summary>
#else
        /// <summary>
        /// The role the Content refers to.
        /// </summary>
#endif
        public Role Role { get; }

#if JHELP
        /// <summary>
        /// Contentが言及する種族
        /// </summary>
#else
        /// <summary>
        /// The species the Content refers to.
        /// </summary>
#endif
        public Species Result { get; }

#if JHELP
        /// <summary>
        /// Contentが言及する発話
        /// </summary>
#else
        /// <summary>
        /// The utterance the Content refers to.
        /// </summary>
#endif
        public Utterance Utterance { get; }

#if JHELP
        /// <summary>
        /// Content中の演算子
        /// </summary>
#else
        /// <summary>
        /// The operator in the Content.
        /// </summary>
#endif
        public Operator Operator { get; }

#if JHELP
        /// <summary>
        /// Content中の被演算子リスト
        /// </summary>
#else
        /// <summary>
        /// The list of the operands in the Content.
        /// </summary>
#endif
        public List<Content> ContentList { get; }

#if JHELP
        /// <summary>
        /// Contentクラスの新しインスタンスを初期化する
        /// </summary>
        /// <param name="builder">発話内容に応じたContentBuilder</param>
#else
        /// <summary>
        /// Initializes a new instance of Content class.
        /// </summary>
        /// <param name="builder">ContentBuildr for the content.</param>
#endif
        public Content(ContentBuilder builder)
        {
            Text = builder.Text;
            Topic = builder.Topic;
            Subject = builder.Subject;
            Target = builder.Target;
            Role = builder.Role;
            Result = builder.Result;
            Utterance = builder.Utterance;
            Operator = builder.Operator;
            ContentList = builder.ContentList;
        }

#if JHELP
        /// <summary>
        /// Contentクラスの新しインスタンスを初期化する
        /// </summary>
        /// <param name="text">発話テキスト</param>
#else
        /// <summary>
        /// Initializes a new instance of Content class.
        /// </summary>
        /// <param name="text">The uttered text.</param>
#endif
        public Content(string text)
        {
            Text = text;
            string sentence = GetSentence(Text);
            if (sentence != null) // Complex sentence.
            {
                Topic = Topic.OPERATOR;
                Operator = Operator.REQUEST;
                ContentList.Add(new Content(sentence));
            }
            else // Simple sentence.
            {
                string[] split = Text.Split();
                int offset = 0;
                if (split[0].StartsWith("Agent"))
                {
                    Subject = Agent.GetAgent(GetInt(split[0]));
                    offset = 1;
                }
                Topic topic;
                if (!Enum.TryParse(split[0 + offset], out topic))
                {
                    throw new AIWolfLibException("Content: Can't find any topic in " + split[0 + offset]);
                }
                Topic = topic;
                if (split.Length >= 2 + offset && split[1 + offset].StartsWith("Agent"))
                {
                    Target = Agent.GetAgent(GetInt(split[1 + offset]));
                }
                switch (Topic)
                {
                    case Topic.SKIP:
                    case Topic.OVER:
                        break;
                    case Topic.AGREE:
                    case Topic.DISAGREE:
                        if (split[1 + offset].Equals("TALK"))
                        {
                            Utterance = new Talk(GetInt(split[3 + offset]), GetInt(split[2 + offset]));
                        }
                        else
                        {
                            Utterance = new Whisper(GetInt(split[3 + offset]), GetInt(split[2 + offset]));
                        }
                        break;
                    case Topic.ESTIMATE:
                    case Topic.COMINGOUT:
                        Role role;
                        if (!Enum.TryParse(split[2 + offset], out role))
                        {
                            throw new AIWolfLibException("Content: Can't find any role in " + split[2 + offset]);
                        }
                        Role = role;
                        break;
                    case Topic.DIVINED:
                    case Topic.IDENTIFIED:
                        Species species;
                        if (!Enum.TryParse(split[2 + offset], out species))
                        {
                            throw new AIWolfLibException("Content: Can't find any species in " + split[2 + offset]);
                        }
                        Result = species;
                        break;
                    case Topic.ATTACK:
                    case Topic.DIVINATION:
                    case Topic.GUARD:
                    case Topic.GUARDED:
                    case Topic.VOTE:
                        break;
                    default:
                        break;
                }
            }
        }

        static readonly Regex regexGetInt = new Regex(@"-?[\d]+");

        /// <summary>
        /// Finds integer value from the given text.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <returns>Integer value if found, otherwise -1.</returns>
        static int GetInt(string text)
        {
            var m = regexGetInt.Match(text);
            if (m.Success)
            {
                return int.Parse(m.Value);
            }
            return -1;
        }

        static readonly Regex regexGetSentence = new Regex(@"REQUEST\((.+?)\)");

        static string GetSentence(string text)
        {
            var m = regexGetSentence.Match(text);
            if (m.Success)
            {
                return m.Groups[1].ToString();
            }
            return null;
        }

    }
}

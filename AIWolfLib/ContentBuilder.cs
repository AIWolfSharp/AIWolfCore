﻿//
// ContentBuilder.cs
//
// Copyright (c) 2017 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//


using System.Collections.Generic;

namespace AIWolf.Lib
{
#if JHELP
    /// <summary>
    /// 発話の種類
    /// </summary>
#else
    /// <summary>
    /// The type of an utterance.
    /// </summary>
#endif
    public enum UtteranceType
    {
#if JHELP
        /// <summary>
        /// 会話
        /// </summary>
#else
        /// <summary>
        /// Talk.
        /// </summary>
#endif
        TALK,
#if JHELP
        /// <summary>
        /// 囁き
        /// </summary>
#else
        /// <summary>
        /// Whisper.
        /// </summary>
#endif
        WHISPER
    }

#if JHELP
    /// <summary>
    /// Contentクラスビルダーの抽象クラス
    /// </summary>
#else
    /// <summary>
    /// Abstract class of the builder for Content class.
    /// </summary>
#endif
    public abstract class ContentBuilder
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
        internal abstract string Text { get; }

#if JHELP
        /// <summary>
        /// Contentのトピック
        /// </summary>
#else
        /// <summary>
        /// The topic of the Content.
        /// </summary>
#endif
        internal Topic Topic { get; set; }

#if JHELP
        /// <summary>
        /// Contentの主語
        /// </summary>
#else
        /// <summary>
        /// The subject of the Content.
        /// </summary>
#endif
        internal Agent Subject { get; set; }

#if JHELP
        /// <summary>
        /// Contentの対象エージェント
        /// </summary>
#else
        /// <summary>
        /// The target agent of the Content.
        /// </summary>
#endif
        internal Agent Target { get; set; }

#if JHELP
        /// <summary>
        /// Contentが言及する役職
        /// </summary>
#else
        /// <summary>
        /// The role the Content refers to.
        /// </summary>
#endif
        internal Role Role { get; set; }

#if JHELP
        /// <summary>
        /// Contentが言及する種族
        /// </summary>
#else
        /// <summary>
        /// The species the Content refers to.
        /// </summary>
#endif
        internal Species Result { get; set; }

#if JHELP
        /// <summary>
        /// Contentが言及する発話
        /// </summary>
#else
        /// <summary>
        /// The utterance the Content refers to.
        /// </summary>
#endif
        internal Utterance Utterance { get; set; }

#if JHELP
        /// <summary>
        /// Content中の演算子
        /// </summary>
#else
        /// <summary>
        /// The operator in the Content.
        /// </summary>
#endif
        internal Operator Operator { get; set; }

#if JHELP
        /// <summary>
        /// Content中の被演算子リスト
        /// </summary>
#else
        /// <summary>
        /// The list of the operands in the Content.
        /// </summary>
#endif
        internal List<Content> ContentList { get; set; }
    }

#if JHELP
    /// <summary>
    /// 同意発話ビルダークラス
    /// </summary>
#else
    /// <summary>
    /// Builder class for the utterance of an agreement.
    /// </summary>
#endif
    public class AgreeContentBuilder : ContentBuilder
    {
        /// <summary>
        /// Initializes a new instance of AgreeContentBuilder.
        /// </summary>
        /// <param name="utteranceType">The type of the utterance agreed with.</param>
        /// <param name="talkDay">The day of the utterance agreed with.</param>
        /// <param name="talkID">The ID of the utterance agreed with.</param>
        public AgreeContentBuilder(UtteranceType utteranceType, int talkDay, int talkID)
        {
            Topic = Topic.AGREE;
            if (utteranceType == UtteranceType.TALK)
            {
                Utterance = new Talk(talkID, talkDay);
            }
            else
            {
                Utterance = new Whisper(talkID, talkDay);
            }
        }

        internal override string Text
        {
            get
            {
                string talkType = "TALK";
                if (Utterance is Whisper)
                {
                    talkType = "WHISPER";
                }
                return string.Join(" ", new string[] { Subject == null ? "" : Subject.ToString(), Topic.ToString(), talkType, "day" + Utterance.Day, "ID:" + Utterance.Idx }).Trim();
            }
        }
    }

#if JHELP
    /// <summary>
    /// 不意発話ビルダークラス
    /// </summary>
#else
    /// <summary>
    /// Builder class for the utterance of a disagreement.
    /// </summary>
#endif
    public class DisagreeContentBuilder : ContentBuilder
    {
        /// <summary>
        /// Initializes a new instance of DisagreeContentBuilder.
        /// </summary>
        /// <param name="utteranceType">The type of the utterance disagreed with.</param>
        /// <param name="talkDay">The day of the utterance disagreed with.</param>
        /// <param name="talkID">The ID of the utterance disagreed with.</param>
        public DisagreeContentBuilder(UtteranceType utteranceType, int talkDay, int talkID)
        {
            Topic = Topic.DISAGREE;
            if (utteranceType == UtteranceType.TALK)
            {
                Utterance = new Talk(talkID, talkDay);
            }
            else
            {
                Utterance = new Whisper(talkID, talkDay);
            }
        }

        internal override string Text
        {
            get
            {
                string talkType = "TALK";
                if (Utterance is Whisper)
                {
                    talkType = "WHISPER";
                }
                return string.Join(" ", new string[] { Subject == null ? "" : Subject.ToString(), Topic.ToString(), talkType, "day" + Utterance.Day, "ID:" + Utterance.Idx }).Trim();
            }
        }
    }

#if JHELP
    /// <summary>
    /// 襲撃発話ビルダークラス
    /// </summary>
#else
    /// <summary>
    /// Builder class for the utterance of an attack.
    /// </summary>
#endif
    public class AttackContentBuilder : ContentBuilder
    {
        /// <summary>
        /// Initializes a new instance of AttackContentBuilder.
        /// </summary>
        /// <param name="target">The agent to be attacked.</param>
        public AttackContentBuilder(Agent target)
        {
            Topic = Topic.ATTACK;
            Target = target;
        }

        internal override string Text
        {
            get
            {
                return string.Join(" ", new string[] { Subject == null ? "" : Subject.ToString(), Topic.ToString(), Target.ToString() }).Trim();
            }
        }
    }

#if JHELP
    /// <summary>
    /// 占い発話ビルダークラス
    /// </summary>
#else
    /// <summary>
    /// Builder class for the utterance of a divination.
    /// </summary>
#endif
    public class DivinationContentBuilder : ContentBuilder
    {
        /// <summary>
        /// Initializes a new instance of DivinationContentBuilder.
        /// </summary>
        /// <param name="target">The agent to be divined.</param>
        public DivinationContentBuilder(Agent target)
        {
            Topic = Topic.DIVINATION;
            Target = target;
        }

        internal override string Text
        {
            get
            {
                return string.Join(" ", new string[] { Subject == null ? "" : Subject.ToString(), Topic.ToString(), Target.ToString() }).Trim();
            }
        }
    }

#if JHELP
    /// <summary>
    /// 護衛発話ビルダークラス
    /// </summary>
#else
    /// <summary>
    /// Builder class for the utterance of a guard.
    /// </summary>
#endif
    public class GuardContentBuilder : ContentBuilder
    {
        /// <summary>
        /// Initializes a new instance of GuardContentBuilder.
        /// </summary>
        /// <param name="target">The agent to be guarded.</param>
        public GuardContentBuilder(Agent target)
        {
            Topic = Topic.GUARD;
            Target = target;
        }

        internal override string Text
        {
            get
            {
                return string.Join(" ", new string[] { Subject == null ? "" : Subject.ToString(), Topic.ToString(), Target.ToString() }).Trim();
            }
        }
    }

#if JHELP
    /// <summary>
    /// 護衛報告発話ビルダークラス
    /// </summary>
#else
    /// <summary>
    /// Builder class for the report of a guard.
    /// </summary>
#endif
    public class GuardedAgentContentBuilder : ContentBuilder
    {
        /// <summary>
        /// Initializes a new instance of GuardedAgentContentBuilder.
        /// </summary>
        /// <param name="target">The guarded agent.</param>
        public GuardedAgentContentBuilder(Agent target)
        {
            Topic = Topic.GUARDED;
            Target = target;
        }

        internal override string Text
        {
            get
            {
                return string.Join(" ", new string[] { Subject == null ? "" : Subject.ToString(), Topic.ToString(), Target.ToString() }).Trim();
            }
        }
    }

#if JHELP
    /// <summary>
    /// 投票発話ビルダークラス
    /// </summary>
#else
    /// <summary>
    /// Builder class for the utterance of a vote.
    /// </summary>
#endif
    public class VoteContentBuilder : ContentBuilder
    {
        /// <summary>
        /// Initializes a new instance of VoteContentBuilder.
        /// </summary>
        /// <param name="target">The agent to be voted for.</param>
        public VoteContentBuilder(Agent target)
        {
            Topic = Topic.VOTE;
            Target = target;
        }

        internal override string Text
        {
            get
            {
                return string.Join(" ", new string[] { Subject == null ? "" : Subject.ToString(), Topic.ToString(), Target.ToString() }).Trim();
            }
        }
    }

#if JHELP
    /// <summary>
    /// カミングアウト発話ビルダークラス
    /// </summary>
#else
    /// <summary>
    /// Builder class for the utterance of a coming-out.
    /// </summary>
#endif
    public class ComingoutContentBuilder : ContentBuilder
    {
        /// <summary>
        /// Initializes a new instance of ComingoutContentBuilder.
        /// </summary>
        /// <param name="target">The agent whose role is come out with.</param>
        /// <param name="role">The role come out with.</param>
        public ComingoutContentBuilder(Agent target, Role role)
        {
            Topic = Topic.COMINGOUT;
            Target = target;
            Role = role;
        }

        internal override string Text
        {
            get
            {
                return string.Join(" ", new string[] { Subject == null ? "" : Subject.ToString(), Topic.ToString(), Target.ToString(), Role.ToString() }).Trim();
            }
        }
    }

#if JHELP
    /// <summary>
    /// 推測発話ビルダークラス
    /// </summary>
#else
    /// <summary>
    /// Builder class for the utterance of a estimation.
    /// </summary>
#endif
    public class EstimateContentBuilder : ContentBuilder
    {
        /// <summary>
        /// Initializes a new instance of EstimateContentBuilder.
        /// </summary>
        /// <param name="target">The estimated agent.</param>
        /// <param name="role">The estimated role.</param>
        public EstimateContentBuilder(Agent target, Role role)
        {
            Topic = Topic.ESTIMATE;
            Target = target;
            Role = role;
        }

        internal override string Text
        {
            get
            {
                return string.Join(" ", new string[] { Subject == null ? "" : Subject.ToString(), Topic.ToString(), Target.ToString(), Role.ToString() }).Trim();
            }
        }
    }

#if JHELP
    /// <summary>
    /// 占い結果報告ビルダークラス
    /// </summary>
#else
    /// <summary>
    /// Builder class for the report of a divination.
    /// </summary>
#endif
    public class DivinedResultContentBuilder : ContentBuilder
    {
        /// <summary>
        /// Initializes a new instance of DivinedResultContentBuilder.
        /// </summary>
        /// <param name="target">The divined agent.</param>
        /// <param name="result">The species of target.</param>
        public DivinedResultContentBuilder(Agent target, Species result)
        {
            Topic = Topic.DIVINED;
            Target = target;
            Result = result;
        }

        internal override string Text
        {
            get
            {
                return string.Join(" ", new string[] { Subject == null ? "" : Subject.ToString(), Topic.ToString(), Target.ToString(), Result.ToString() }).Trim();
            }
        }
    }

#if JHELP
    /// <summary>
    /// 霊媒結果報告ビルダークラス
    /// </summary>
#else
    /// <summary>
    /// Builder class for the report of an identification.
    /// </summary>
#endif
    public class IdentContentBuilder : ContentBuilder
    {
        /// <summary>
        /// Initializes a new instance of IdentContentBuilder.
        /// </summary>
        /// <param name="target">The identified agent.</param>
        /// <param name="result">The species of target.</param>
        public IdentContentBuilder(Agent target, Species result)
        {
            Topic = Topic.IDENTIFIED;
            Target = target;
            Result = result;
        }

        internal override string Text
        {
            get
            {
                return string.Join(" ", new string[] { Subject == null ? "" : Subject.ToString(), Topic.ToString(), Target.ToString(), Result.ToString() }).Trim();
            }
        }
    }

#if JHELP
    /// <summary>
    /// 要求発話ビルダークラス
    /// </summary>
#else
    /// <summary>
    /// Builder class for the report of a request.
    /// </summary>
#endif
    public class RequestContentBuilder : ContentBuilder
    {
        /// <summary>
        /// Initializes a new instance of RequestContentBuilder.
        /// </summary>
        /// <param name="agent">The requested agent.</param>
        /// <param name="content">Content representing the requested action.</param>
        public RequestContentBuilder(Agent agent, Content content)
        {
            Topic = Topic.OPERATOR;
            Operator = Operator.REQUEST;
            content.Subject = agent;
            content.Text = string.Join(" ", new string[] { agent == null ? "" : agent.ToString(), content.Text }).Trim();
            ContentList = new List<Content>();
            ContentList.Add(content);
        }

        internal override string Text
        {
            get
            {
                return string.Join(" ", new string[] { Subject == null ? "" : Subject.ToString(), Operator + "(" + ContentList[0].Text + ")" }).Trim();
            }
        }
    }

#if JHELP
    /// <summary>
    /// SKIPビルダークラス
    /// </summary>
#else
    /// <summary>
    /// Builder class for SKIP.
    /// </summary>
#endif
    public class SkipContentBuilder : ContentBuilder
    {
        /// <summary>
        /// Initializes a new instance of SkipContentBuilder.
        /// </summary>
        public SkipContentBuilder()
        {
            Topic = Topic.SKIP;
        }

        internal override string Text
        {
            get
            {
                return string.Join(" ", new string[] { Subject == null ? "" : Subject.ToString(), Utterance.SKIP }).Trim();
            }
        }
    }

#if JHELP
    /// <summary>
    /// OVERビルダークラス
    /// </summary>
#else
    /// <summary>
    /// Builder class for OVER.
    /// </summary>
#endif
    public class OverContentBuilder : ContentBuilder
    {
        /// <summary>
        /// Initializes a new instance of OverContentBuilder.
        /// </summary>
        public OverContentBuilder()
        {
            Topic = Topic.OVER;
        }

        internal override string Text
        {
            get
            {
                return string.Join(" ", new string[] { Subject == null ? "" : Subject.ToString(), Utterance.OVER }).Trim();
            }
        }
    }

}


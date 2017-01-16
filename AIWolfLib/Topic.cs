//
// Topic.cs
//
// Copyright (c) 2016 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

namespace AIWolf.Lib
{
#if JHELP
    /// <summary>
    /// 会話/囁きのトピック
    /// </summary>
#else
    /// <summary>
    /// Enumeration type for topic of talk/whisper.
    /// </summary>
#endif
    public enum Topic
    {
#if JHELP
        /// <summary>
        /// ダミートピック
        /// </summary>
#else
        /// <summary>
        /// Dummy topic.
        /// </summary>
#endif
        DUMMY,

#if JHELP
        /// <summary>
        /// 役職の推定
        /// </summary>
#else
        /// <summary>
        /// Estimation.
        /// </summary>
#endif
        ESTIMATE,

#if JHELP
        /// <summary>
        /// カミングアウト
        /// </summary>
#else
        /// <summary>
        /// Comingout.
        /// </summary>
#endif
        COMINGOUT,

#if JHELP
        /// <summary>
        /// 占い行為
        /// </summary>
#else
        /// <summary>
        /// Divination.
        /// </summary>
#endif
        DIVINATION,

#if JHELP
        /// <summary>
        /// 占い結果の報告
        /// </summary>
#else
        /// <summary>
        /// Report of a divination.
        /// </summary>
#endif
        DIVINED,

#if JHELP
        /// <summary>
        /// 霊能結果の報告
        /// </summary>
#else
        /// <summary>
        /// Report of an identification.
        /// </summary>
#endif
        IDENTIFIED,

#if JHELP
        /// <summary>
        /// 護衛行為
        /// </summary>
#else
        /// <summary>
        /// Guard.
        /// </summary>
#endif
        GUARD,

#if JHELP
        /// <summary>
        /// 護衛先の報告
        /// </summary>
#else
        /// <summary>
        /// Report of a guard.
        /// </summary>
#endif
        GUARDED,

#if JHELP
        /// <summary>
        /// 投票先の表明
        /// </summary>
#else
        /// <summary>
        /// Vote.
        /// </summary>
#endif
        VOTE,

#if JHELP
        /// <summary>
        /// 襲撃先の表明
        /// </summary>
#else
        /// <summary>
        /// Attack.
        /// </summary>
#endif
        ATTACK,

#if JHELP
        /// <summary>
        /// 同意
        /// </summary>
#else
        /// <summary>
        /// Agreement.
        /// </summary>
#endif
        AGREE,

#if JHELP
        /// <summary>
        /// 不同意
        /// </summary>
#else
        /// <summary>
        /// Disagreement.
        /// </summary>
#endif
        DISAGREE,

#if JHELP
        /// <summary>
        /// 話す/囁くことはない
        /// </summary>
#else
        /// <summary>
        /// There is nothing to talk/whisper.
        /// </summary>
#endif
        Over,

#if JHELP
        /// <summary>
        /// 話す/囁くことはあるがこのターンはスキップ
        /// </summary>
#else
        /// <summary>
        /// Skip this turn though there is something to talk/whisper.
        /// </summary>
#endif
        Skip
    }
}

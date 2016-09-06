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
    /// <summary>
    /// Enumeration type for topic of talk/whisper.
    /// </summary>
    public enum Topic
    {
        /// <summary>
        /// Estimation.
        /// </summary>
        ESTIMATE,

        /// <summary>
        /// Comingout.
        /// </summary>
        COMINGOUT,

        /// <summary>
        /// Divination.
        /// </summary>
        DIVINED,

        /// <summary>
        /// Inquest.
        /// </summary>
        INQUESTED,

        /// <summary>
        /// Guard.
        /// </summary>
        GUARDED,

        /// <summary>
        /// Vote.
        /// </summary>
        VOTE,

        /// <summary>
        /// Attack.
        /// </summary>
        ATTACK,

        /// <summary>
        /// Agreement.
        /// </summary>
        AGREE,

        /// <summary>
        /// Disagreement.
        /// </summary>
        DISAGREE,

        /// <summary>
        /// There is nothing to talk/whisper.
        /// </summary>
        Over,

        /// <summary>
        /// Skip this turn though there is something to talk/whisper.
        /// </summary>
        Skip
    }
}

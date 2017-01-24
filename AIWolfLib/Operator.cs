//
// Operator.cs
//
// Copyright (c) 2017 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//


namespace AIWolf.Lib
{
#if JHELP
    /// <summary>
    /// 演算子
    /// </summary>
#else
    /// <summary>
    /// Enumeration type for operator.
    /// </summary>
#endif
    public enum Operator
    {
#if JHELP
        /// <summary>
        /// 何もしない
        /// </summary>
#else
        /// <summary>
        /// No operation.
        /// </summary>
#endif
        NOP,

#if JHELP
        /// <summary>
        /// 行動の要求
        /// </summary>
#else
        /// <summary>
        /// Request for the action.
        /// </summary>
#endif
        REQUEST,

#if JHELP
        /// <summary>
        /// 行動の理由
        /// </summary>
#else
        /// <summary>
        /// Reason for the action.
        /// </summary>
#endif
        BECAUSE,

#if JHELP
        /// <summary>
        /// AND
        /// </summary>
#else
        /// <summary>
        /// AND.
        /// </summary>
#endif
        AND,

#if JHELP
        /// <summary>
        /// OR
        /// </summary>
#else
        /// <summary>
        /// OR.
        /// </summary>
#endif
        OR
    }
}

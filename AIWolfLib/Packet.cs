//
// Packet.cs
//
// Copyright (c) 2016 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AIWolf.Lib
{
    /// <summary>
    /// Packet for sending data to client.
    /// </summary>
    [DataContract]
    public class Packet
    {
        /// <summary>
        /// The request from the server.
        /// </summary>
        /// <value>Request.</value>
        [DataMember(Name = "request")]
        public Request Request { get; }

        /// <summary>
        /// The game information.
        /// </summary>
        /// <value>The instance of GameInfoToSend class representating the game information.</value>
        [DataMember(Name = "gameInfo")]
        public GameInfo GameInfo { get; }

        /// <summary>
        /// The setting of game.
        /// </summary>
        /// <value>The instance of GameSetting class representating the game setting.</value>
        [DataMember(Name = "gameSetting")]
        public GameSetting GameSetting { get; }

        /// <summary>
        /// The history of talks.
        /// </summary>
        /// <value>The list of TalkToSend representating the history of talks.</value>
        [DataMember(Name = "talkHistory")]
        public List<Talk> TalkHistory { get; }

        /// <summary>
        /// The history of whispers.
        /// </summary>
        /// <value>The list of TalkToSend representating the history of whispers.</value>
        [DataMember(Name = "whisperHistory")]
        public List<Talk> WhisperHistory { get; }

        /// <summary>
        /// Initializes a new instance of this class with given request.
        /// </summary>
        /// <param name="request">Request given.</param>
        public Packet(Request request)
        {
            Request = request;
        }

        /// <summary>
        /// Initializes a new instance of this class with request and game information given.
        /// </summary>
        /// <param name="request">Request given.</param>
        /// <param name="gameInfo">GemeInfoToSend representation of game information given.</param>
        public Packet(Request request, GameInfo gameInfo)
        {
            Request = request;
            GameInfo = gameInfo;
        }

        /// <summary>
        /// Initializes a new instance of this class with request, game information and setting of game given.
        /// </summary>
        /// <param name="request">Request given.</param>
        /// <param name="gameInfo">GemeInfoToSend representation of game information given.</param>
        /// <param name="gameSetting">GameSetting representation of setting of game given.</param>
        public Packet(Request request, GameInfo gameInfo, GameSetting gameSetting)
        {
            Request = request;
            GameInfo = gameInfo;
            GameSetting = gameSetting;
        }

        /// <summary>
        /// Initializes a new instance of this class with request, history of talk and whisper given.
        /// </summary>
        /// <param name="request">Request given.</param>
        /// <param name="talkHistoryList">History of talk given.</param>
        /// <param name="whisperHistoryList">History of whisper given.</param>
        public Packet(Request request, List<Talk> talkHistoryList, List<Talk> whisperHistoryList)
        {
            Request = request;
            TalkHistory = talkHistoryList;
            WhisperHistory = whisperHistoryList;
        }
    }
}

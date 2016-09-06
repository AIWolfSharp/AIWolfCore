//
// TcpipClient.cs
//
// Copyright (c) 2016 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace AIWolf.Lib
{
    /// <summary>
    /// AIWolf client using TCP/IP connection.
    /// </summary>
    public class TcpipClient
    {
        string host;
        int port;

        TcpClient tcpClient;

        IPlayer player;

        GameInfo lastGameInfo;

        /// <summary>
        /// The requested role.
        /// </summary>
        public Role RequestRole { get; }

        /// <summary>
        /// Whether or not this client is running.
        /// </summary>
        public bool Running { get; private set; }

        /// <summary>
        /// The name of player this client manages.
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// The number of milliseconds to wait for the request call.
        /// </summary>
        public int Timeout { get; set; } = -1; // Do not limit by default.

        /// <summary>
        /// Initializes a new instance of this class which connects given port of given host.
        /// </summary>
        /// <param name="host">Hostname this client connects.</param>
        /// <param name="port">Port number this client connects.</param>
        public TcpipClient(string host, int port)
        {
            this.host = host;
            this.port = port;
            Running = false;
        }

        /// <summary>
        /// Initializes a new instance of this class which connects given port of given host, and requests given role.
        /// </summary>
        /// <param name="host">Hostname this client connects.</param>
        /// <param name="port">Port number this client connects.</param>
        /// <param name="requestRole">Role this client requests.</param>
        public TcpipClient(string host, int port, Role requestRole) : this(host, port)
        {
            RequestRole = requestRole;
        }

        /// <summary>
        /// Connects the player to the server.
        /// </summary>
        /// <param name="player">The player to be connected.</param>
        public void Connect(IPlayer player)
        {
            this.player = player;

            tcpClient = new TcpClient();
            tcpClient.ConnectAsync(host, port).Wait();

            try
            {
                StreamReader sr = new StreamReader(tcpClient.GetStream());
                StreamWriter sw = new StreamWriter(tcpClient.GetStream());
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Packet packet = ToPacket(line);

                    object obj = Recieve(packet);
                    if (packet.Request.HasReturn())
                    {
                        if (obj == null)
                        {
                            sw.WriteLine();
                        }
                        else if (obj is string)
                        {
                            sw.WriteLine(obj);
                        }
                        else
                        {
                            sw.WriteLine(DataConverter.Serialize(obj));
                        }
                        sw.Flush();
                    }
                }
            }
            catch (IOException e)
            {
                if (Running)
                {
                    throw e;
                }
            }
            finally
            {
                Running = false;
            }
        }

        /// <summary>
        /// Receives a packet from the server and sends it to the player, and returns what returned from the player.
        /// </summary>
        /// <param name="packet">The packet from the server.</param>
        /// <returns>The object returned from the player.</returns>
        public object Recieve(Packet packet)
        {
            GameInfo gameInfo = lastGameInfo;
            GameSetting gameSetting = packet.GameSetting;

            if (packet.GameInfo != null)
            {
                gameInfo = packet.GameInfo;
                lastGameInfo = gameInfo;
            }

            if (packet.TalkHistory != null)
            {
                Talk lastTalk = null;
                if (gameInfo.TalkList != null && gameInfo.TalkList.Count != 0)
                {
                    lastTalk = gameInfo.TalkList.Last();
                }
                gameInfo.TalkList.AddRange(packet.TalkHistory.Where(t => IsAfter(t, lastTalk)));
            }

            if (packet.WhisperHistory != null)
            {
                Talk lastWhisper = null;
                if (gameInfo.WhisperList != null && gameInfo.WhisperList.Count != 0)
                {
                    lastWhisper = gameInfo.WhisperList.Last();
                }
                gameInfo.WhisperList.AddRange(packet.WhisperHistory.Where(w => IsAfter(w, lastWhisper)));
            }

            Task<object> task = Task.Run(() =>
            {
                object returnObject = null;
                switch (packet.Request)
                {
                    case Request.DUMMY:
                        break;
                    case Request.INITIALIZE:
                        Running = true;
                        player.Initialize(gameInfo, gameSetting);
                        break;
                    case Request.DAILY_INITIALIZE:
                        player.Update(gameInfo);
                        player.DayStart();
                        break;
                    case Request.DAILY_FINISH:
                        player.Update(gameInfo);
                        break;
                    case Request.NAME:
                        if (PlayerName == null)
                        {
                            returnObject = player.Name;
                            if (returnObject == null)
                            {
                                returnObject = player.GetType().Name;
                            }
                        }
                        else
                        {
                            returnObject = PlayerName;
                        }
                        break;
                    case Request.ROLE:
                        if (RequestRole != Role.UNC)
                        {
                            returnObject = RequestRole.ToString();
                        }
                        else
                        {
                            returnObject = "none";
                        }
                        break;
                    case Request.ATTACK:
                        player.Update(gameInfo);
                        returnObject = player.Attack();
                        break;
                    case Request.TALK:
                        player.Update(gameInfo);
                        returnObject = player.Talk();
                        if (returnObject == null)
                        {
                            returnObject = Talk.Skip;
                        }
                        break;
                    case Request.WHISPER:
                        player.Update(gameInfo);
                        returnObject = player.Whisper();
                        if (returnObject == null)
                        {
                            returnObject = Talk.Skip;
                        }
                        break;
                    case Request.DIVINE:
                        player.Update(gameInfo);
                        returnObject = player.Divine();
                        break;
                    case Request.GUARD:
                        player.Update(gameInfo);
                        returnObject = player.Guard();
                        break;
                    case Request.VOTE:
                        player.Update(gameInfo);
                        returnObject = player.Vote();
                        break;
                    case Request.FINISH:
                        player.Update(gameInfo);
                        player.Finish();
                        Running = false;
                        break;
                    default:
                        break;
                }
                return returnObject;
            });
            if (task.Wait(Timeout))
            {
                return task.Result;
            }
            else
            {
                Error.TimeoutError(string.Format("{0}@{1} exceeds the time limit({2}ms).", packet.Request, player.Name, Timeout));
                task.Wait(-1);
                return task.Result;
            }
        }

        /// <summary>
        /// Whether or not the given talk is after lastTalk.
        /// </summary>
        /// <param name="talk">The talk to be checked.</param>
        /// <param name="lastTalk">The last talk.</param>
        /// <returns>True if the given talk is after the last talk.</returns>
        /// <remarks>If it is same, return false.</remarks>
        bool IsAfter(Talk talk, Talk lastTalk)
        {
            if (lastTalk != null)
            {
                if (talk.Day < lastTalk.Day)
                {
                    return false;
                }
                if (talk.Day == lastTalk.Day && talk.Idx <= lastTalk.Idx)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns the instance of Packet class converted from the JSON string given.
        /// </summary>
        /// <param name="line">The JSON string to be converted.</param>
        /// <returns>The instance of Packet class converted from the JSON string.</returns>
        Packet ToPacket(string line)
        {
            Dictionary<string, object> map = DataConverter.Deserialize<Dictionary<string, object>>(line);

            if (map["request"] == null)
            {
                Error.RuntimeError(GetType() + ".ToPacket(): There is no request in " + line + ".", "Force it to be Request.DUMMY.");
                return new Packet(Request.DUMMY);
            }

            Request request;
            if (!Enum.TryParse((string)map["request"], out request))
            {
                Error.RuntimeError(GetType() + ".ToPacket(): Invalid request in " + line + ".", "Force it to be Request.DUMMY.");
                return new Packet(Request.DUMMY);
            }

            if (map["gameInfo"] != null)
            {
                GameInfo gameInfo = DataConverter.Deserialize<GameInfo>(DataConverter.Serialize(map["gameInfo"]));
                if (map["gameSetting"] != null)
                {
                    GameSetting gameSetting = DataConverter.Deserialize<GameSetting>(DataConverter.Serialize(map["gameSetting"]));
                    return new Packet(request, gameInfo, gameSetting);
                }
                else
                {
                    return new Packet(request, gameInfo);
                }
            }
            else if (map["talkHistory"] != null)
            {
                List<Talk> talkHistoryList = DataConverter.Deserialize<List<Dictionary<string, string>>>(DataConverter.Serialize(map["talkHistory"]))
                    .Select(m => DataConverter.Deserialize<Talk>(DataConverter.Serialize(m))).ToList();
                List<Talk> whisperHistoryList = DataConverter.Deserialize<List<Dictionary<string, string>>>(DataConverter.Serialize(map["whisperHistory"]))
                    .Select(m => DataConverter.Deserialize<Talk>(DataConverter.Serialize(m))).ToList();
                return new Packet(request, talkHistoryList, whisperHistoryList);
            }
            else
            {
                return new Packet(request);
            }
        }
    }
}

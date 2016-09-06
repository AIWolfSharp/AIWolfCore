//
// ClientStarter.cs
//
// Copyright (c) 2016 Takashi OTSUKI
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
//

using AIWolf.Lib;
using Microsoft.Extensions.DependencyModel;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace AIWolf
{
    /// <summary>
    /// Client starter class.
    /// </summary>
    public class ClientStarter
    {
        /// <summary>
        /// Main method.
        /// </summary>
        /// <param name="args">Arguments.</param>
        /// <remarks>
        /// Usage: [-h host] [-p port] [-t timeout] -c clientClass dllName [roleRequest] [-n name]
        /// </remarks>
        public static void Main(string[] args)
        {
            string host = "localhost";
            int port = 10000;
            string clsName = null;
            string dllName = null;
            Role? roleRequest = null;
            string playerName = null;
            int timeout = -1; // Do not limit by default.

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith("-"))
                {
                    if (args[i].Equals("-p"))
                    {
                        i++;
                        if (i < args.Length)
                        {
                            if (!int.TryParse(args[i], out port))
                            {
                                Console.Error.WriteLine("ClientStarter: Invalid port {0}.", args[i]);
                                return;
                            }
                        }
                        else
                        {
                            Usage();
                        }
                    }
                    else if (args[i].Equals("-h"))
                    {
                        i++;
                        if (i < args.Length)
                        {
                            host = args[i];
                        }
                        else
                        {
                            Usage();
                        }
                    }
                    else if (args[i].Equals("-c"))
                    {
                        i++;
                        if (i < args.Length)
                        {
                            clsName = args[i];
                        }
                        else
                        {
                            Usage();
                        }
                        i++;
                        if (i < args.Length)
                        {
                            dllName = args[i];
                        }
                        else
                        {
                            Usage();
                        }
                        i++;
                        if (i > args.Length - 1 || args[i].StartsWith("-")) // Role is not requested.
                        {
                            i--;
                            roleRequest = null;
                            continue;
                        }
                        Role role;
                        if (!Enum.TryParse(args[i], out role))
                        {
                            Console.Error.WriteLine("ClientStarter: Invalid role {0}.", args[i]);
                            return;
                        }
                    }
                    else if (args[i].Equals("-n"))
                    {
                        i++;
                        if (i < args.Length)
                        {
                            playerName = args[i];
                        }
                        else
                        {
                            Usage();
                        }
                    }
                    else if (args[i].Equals("-t"))
                    {
                        i++;
                        if (i < args.Length)
                        {
                            if (!int.TryParse(args[i], out timeout))
                            {
                                Console.Error.WriteLine("ClientStarter: Invalid timeout {0}.", args[i]);
                                return;
                            }
                        }
                        else
                        {
                            Usage();
                        }
                    }
                }
            }
            if (port < 0)
            {
                Usage();
            }

            IPlayer player;
            if (clsName == null)
            {
                player = new DefaultPlayer();
            }
            else
            {
                Assembly assembly;
                try
                {
                    assembly = new AssemblyLoader(Path.GetDirectoryName(dllName)).LoadFromAssemblyPath(Path.GetFullPath(dllName));
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine("ClientStarter: Error in loading {0}.", dllName);
                    Console.Error.WriteLine(e);
                    return;
                }

                try
                {
                    player = (IPlayer)Activator.CreateInstance(assembly.GetType(clsName));
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine("ClientStarter: Error in creating instance of {0}.", clsName);
                    Console.Error.WriteLine(e);
                    return;
                }
            }

            TcpipClient client = new TcpipClient(host, port, roleRequest);
            client.Timeout = timeout;
            if (playerName != null)
            {
                client.PlayerName = playerName;
            }
            try
            {
                client.Connect(player);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("ClientStarter: Error in runnning player.");
                Console.Error.WriteLine(ex);
                if (ex is AggregateException)
                {
                    foreach (var e in (ex as AggregateException).InnerExceptions)
                    {
                        Console.Error.WriteLine(e);
                    }
                }
                return;
            }
        }

        static void Usage()
        {
            Console.Error.WriteLine("Usage: ClientStarter [-h host] [-p port] -c clientClass dllName [roleRequest] [-n name] [-t timeout]");
            Environment.Exit(0);
        }
    }

    class AssemblyLoader : AssemblyLoadContext
    {
        string folderPath;

        public AssemblyLoader(string folderPath)
        {
            this.folderPath = folderPath;
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            var cl = DependencyContext.Default.CompileLibraries.Where(d => d.Name.Contains(assemblyName.Name));
            if (cl.Count() > 0)
            {
                return Assembly.Load(new AssemblyName(cl.First().Name));
            }
            else
            {
                var fileInfo = new FileInfo($"{folderPath}{Path.DirectorySeparatorChar}{assemblyName.Name}.dll");
                if (File.Exists(fileInfo.FullName))
                {
                    var asl = new AssemblyLoader(fileInfo.DirectoryName);
                    return asl.LoadFromAssemblyPath(fileInfo.FullName);
                }
            }
            return Assembly.Load(assemblyName);
        }
    }

    class DefaultPlayer : IPlayer
    {
        public string Name
        {
            get
            {
                return GetType().ToString();
            }
        }

        public Agent Attack()
        {
            return null;
        }

        public void DayStart()
        {
            return;
        }

        public Agent Divine()
        {
            return null;
        }

        public void Finish()
        {
            return;
        }

        public Agent Guard()
        {
            return null;
        }

        public void Initialize(GameInfo gameInfo, GameSetting gameSetting)
        {
            return;
        }

        public string Talk()
        {
            return Lib.Talk.Over;
        }

        public void Update(GameInfo gameInfo)
        {
            return;
        }

        public Agent Vote()
        {
            return null;
        }

        public string Whisper()
        {
            return Lib.Talk.Over;
        }
    }
}

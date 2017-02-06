[View in Japanese](https://github.com/AIWolfSharp/AIWolfCore/blob/core/README-J.md)
# AIWolf.NET Core
## .NET Core version of AIWolf Library

AIWolf.NET Core is the AIWolf (artificial intelligence based werewolf) library for .NET Core,
which is compatible with AIWolf platform version 0.4.4.
The current version number is 1.0.0.

1. QUICK START

    1. Install .NET Core SDK 1.1.0.
    2. Though AIWolf.NET Core has two libraries (AIWolfLib and AIWolfPlayer),
you do not have to install them manually since NuGet package manager automatically does.
    3. AIWolf.NET Core does not have own game server, so you have to download
[AIWolf platform version 0.4.4](http://aiwolf.org/server/)
to run the server.
    4. We do not distribute the executable for starting client agent,
so you first have to build it by yourself.
       1. Download [ClientStarter-1.0.0.zip](https://github.com/AIWolfSharp/AIWolfCore/releases/download/v1.0.0/ClientStarter-1.0.0.zip),
and put two files (ClientStarter.cs and project.json) in the zip file
into the folder you like.
       2. Execute the following commands in the folder to build ClientStarter.  
`dotnet restore`  
`dotnet build`
    5. After the successful build, execute the following command
to try connecting sample agent with the local server waiting the connection at port 10000.  
`dotnet run -d`

    6. Making your own agent

      * You can download [Reference Manual](https://github.com/AIWolfSharp/AIWolfCore/releases/download/v1.0.0/AIWolf_NET_ReferenceManual.zip) to be aid in making your own agent.
      * You can view the the tutorial [here](http://www.slideshare.net/takots/net-corevs-code-71808207) (sorry, in Japanese).

1. HISTORY and CHANGES

    * 1.0.0-prerelease2 : The first public prerelease.
    * 1.0.0-prerelease3 : Extension method Shuffle() for IEnumerable is moved from namespace AIWolf.Player.Sample
to namespace AIWolf.Lib because this method is useful in various situations.
    * 1.0.0-prerelease4 : Make projects generate portable PDBs to debug in VS Code.
    * 1.0.0 : Official release equivalent to prerelease4.

---
This software is released under the MIT License, see [LICENSE](https://github.com/AIWolfSharp/AIWolf_NET/blob/master/LICENSE).

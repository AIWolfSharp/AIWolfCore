# AIWolfCore
## .NET Core version of AIWolf Library

AIWolfCore is a AIWolf (artificial intelligence based werewolf) library for .NET Core.
This library is written in C#, and current version number is 1.0.0-prerelease2.

1. QUICK START

    1. Install .NET Core SDK 1.1.0.
    2. AIWolfCore's two libraries, AIWolfLib and AIWolfPlayer,
       are NuGet packages, so you do not have to install them to your solution
        because NuGet package manager automatically do it.
    1. Try sample agent.
       1. Download the [AIWolf platform version 0.4.x](http://aiwolf.org/server/),
        and launch the game server.
       2. Download [ClientStarter-1.0.0-pre2.zip](https://github.com/AIWolfSharp/AIWolfCore/releases/download/v1.0.0-pre2/ClientStarter-1.0.0-pre2.zip),
		and put two files(ClientStarter.cs and project.json) in this zip file
        into the folder you like.
       3. Execute the following commands in the folder to build ClientStarter.

          `dotnet restore`

          `dotnet build`

       4. After the build, you can test it by connecting the sample agent
       with the server on local machine waiting the connection at port 10000.

          `dotnet run -d`

    1. Making your own agent

        You will soon be able to view the tutorial for making an AIWolf agent using AIWolfCore.

1. HISTORY

    * 1.0.0-prerelease2

        This release.

---
This software is released under the MIT License, see [LICENSE](https://github.com/AIWolfSharp/AIWolf_NET/blob/master/LICENSE).

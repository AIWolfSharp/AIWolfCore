[View in Japanese](http://aiwolfsharp.github.io/AIWolf_NET)
# AIWolf.NET
## .NET Core version of AIWolf Library

AIWolf.NET is a AIWolf (artificial intelligence based werewolf) library for .NET Core.
This library is written in C#, and current version number is 1.0.0.

1. QUICK START

    1. AIWolf.NET is now NuGet package, so you do not have to install it to your solution
        because NuGet package manager automatically do it.

    1. Try sample agent.

        Download the [AIWolf platform](http://aiwolf.org/server/),
        and launch the game server.
        In order to connect a sample agent to the server listening port 10000 on localhost,
        limiting the agent's processing one request to 100ms,
        execute the following command in the folder of AIWolf.NET.

        `ClientStarter.exe -h localhost -p 10000 -t 100 -c AIWolf.Client.Base.Smpl.SampleRoleAssignPlayer AIWolfLibClient.dll`

    1. Making your own agent

        You can view the tutorial for making an AIWolf agent in C#
        [here](http://www.slideshare.net/takots/how-to-make-an-artificial-intelligence-based-werewolf-agent-in-c-using-visual-studio). 

1. HISTORY

    * 0.1.0

        Initial release.

    * 0.2.0

        - When the agent terminates abnormally throwing exception,
        ClientStarter.exe writes the stacktrace of the exception to the standard error stream.

        - In order to limit the time for agent's processing one request,
        timeout option `-t` is introduced into ClientStarter.exe.
        If the agent runs out of time (100ms by default), it is terminated immediately.

    * 0.2.1

        - In case of no timeout option, ClientStarter.exe does not limit the time for processing a request.

        - Redistribution of Json.NET 9.0 Release 1.

    * 0.2.2

        - Make exception handling smarter.

        - Make ClientStarter.exe show more information in case of exception.
        
    * 1.0.0
        
        - Built on .NET Core 1.0.
        
        - The sample agent has gone away. You can obtain it as another library.

---
This software is released under the MIT License, see [LICENSE](https://github.com/AIWolfSharp/AIWolf_NET/blob/master/LICENSE).

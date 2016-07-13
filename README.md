[View in Japanese （日本語）](http://aiwolfsharp.github.io/AIWolf_NET)
# AIWolf.NET(aka AIWolf#)
## .NET version of AIWolf Library

AIWolf.NET is a AIWolf (artificial intelligence based werewolf) library for .NET Framework 4.5.
This library is written in C#, and current version number is 0.2.0.

1. Quick start
  
  1. Download AIWolf_NET-0.2.0.zip
  
    [AIWolf_NET-0.1.0.zip](https://github.com/AIWolfSharp/AIWolf_NET/releases/download/v0.2.0/AIWolf_NET-0.2.0.zip)
includes two dlls (AIWolfLibCommon.dll, AIWolfLibClient.dll),
starter executable (ClientStarter.exe)
and API reference manual in html folder.
This is enough to make an AIWolf agent
if you don't need source code of the library.
In case the downloaded files do not work properly becase Windows blocks them,
unblock them using "Properties" in context menu.
    
  1. Obtain Json.NET version 9.0.1
  
    Json.NET version 9.0.1 is necessary for ClientStarter.exe to work properly.
    Put Newtonsoft.Json.dll into the same folder of AIWolf.NET. 
  
  1. Try sample agent.
  
    Download [AIWolf platform](http://www.aiwolf.org/aiwp/wp-content/uploads/2014/03/aiwolf-ver0.3.2.zip),
and start ServerStarter.bat to launch the game server.
In order to connect sample agent to the server listening port 10000 on localhost,
execute the following command on command prompt in the folder of AIWolf.NET.

        `ClientStarter.exe -h localhost -p 10000 -c AIWolf.Client.Base.Smpl.SampleRoleAssignPlayer AIWolfLibClient.dll`
        
1. Making my own agent

     The tutorial for making an AIWolf agent in C# can be viewed
     [here](http://www.slideshare.net/takots/how-to-make-an-artificial-intelligence-based-werewolf-agent-in-c-using-visual-studio). 

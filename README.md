# AIWolf.NET(aka AIWolf#)
## .NET version of AIWolf Library

AIWolf.NETとは，人狼知能プラットフォームのライブラリ群を.NET Framework 4.5用にC#で書き直したもので，
最新バージョンは0.1.0です．

1. クイックスタート
  
  1. AIWolf_NET-0.1.0.zipをダウンロードする
  
    [AIWolf_NET-0.1.0.zip](https://github.com/AIWolfSharp/AIWolf_NET/releases/download/v0.1.0/AIWolf_NET-0.1.0.zip)は，
ライブラリ(AIWolfLibCommon.dll, AIWolfLibClient.dll)と
クライアントスターター(ClientStarter.exe)そしてAPIリファレンス(html)をまとめたものです．
ライブラリのソースファイルが必要でなければ，これをダウンロードするだけで
人狼知能エージェントを作成することができます．
なお，Windowsではダウンロードしたファイルがブロックされるため，そのままでは動作しない場合があります．
その場合はコンテキストメニューの「プロパティ」よりブロックを解除してください．
    
  1. 必要なライブラリを入手する
  
    ClientStarterを使用する場合にはJson.NETが必要です．
    入手したらNewtonsoft.Json.dllをAIWolf_NET-0.1.0.zipを展開したディレクトリに置いてください．
  
  1. サンプルプレイヤーを起動してみる
  
    人狼知能プラットフォームを入手し，ServerStarterで人狼知能サーバーを起動しておきます．
ここでは，サーバーがlocalhost上のポート10000番で接続を待っているとします．
AIWolf_NET-0.1.0.zipを展開したディレクトリで，
コマンドプロンプトから以下のコマンドを発行すると，サーバーにSampleRoleAssignPlayerが接続したことが表示されるはずです．
    
        `ClientStarter.exe –h localhost –p 10000 –c AIWolf.Client.Base.Smpl.SampleRoleAssignPlayer AIWolfLibClient.dll`
        
1. 独自のエージェントを作成

     エージェント作成のチュートリアルをご覧ください．
     * [C#版人狼知能エージェントの作り方（Visual Studio編）](http://www.slideshare.net/takots/c-59927842)
     * [C#版人狼知能エージェントの作り方(MonoDevelop/Xamarin Studio編)](http://www.slideshare.net/takots/cmonodevelopxamarin-studio)

# AIWolf.NET Core
## .NET Core版人狼知能ライブラリ
AIWolf.NET Coreは人狼知能プラットフォーム0.4.4互換の.NET Core版人狼知能ライブラリです．
最新版はバージョン1.0.0です．

1. クイックスタート
    1. .NET Core SDK 1.1.0をインストールしてください．
    2. AIWolf.NET CoreにはAIWolfLibとAIWolfPlayerの2つのライブラリがあるのですが，
NuGetパッケージマネージャが管理しますので，インストール作業は不要です．
    3. AIWolf.NET Coreには自前のゲームサーバがありませんので，サーバを動かすために
[人狼知能プラットフォームversion 0.4.4](http://aiwolf.org/server/)をダウンロードしてください．
    4. クライアントエージェントを起動するClientStarterの実行ファイルは配布していませんので，
まずはClientStarterをビルドするところから始めましょう．
       1. [ClientStarter-1.0.0.zip](https://github.com/AIWolfSharp/AIWolfCore/releases/download/v1.0.0/ClientStarter-1.0.0.zip)をダウンロードして，
その中にあるClientStarter.csとproject.jsonをお好きなフォルダに入れてください．
       2. ClientStarterをビルドするために以下のコマンドを実行してください．  
`dotnet restore`  
`dotnet build`
    5. ビルドに成功したら，以下のコマンドでサンプルエージェントが10000番ポートで接続待ちのゲームサーバに接続できるか確認しましょう．  
`dotnet run -d`

    6. 自分でエージェントを作るには
      * [リファレンスマニュアル](https://github.com/AIWolfSharp/AIWolfCore/releases/download/v1.0.0/AIWolf_NET_ReferenceManual.zip) が役に立つでしょう．
      * [.NET CoreとVS Codeで作る人狼知能](http://www.slideshare.net/takots/net-corevs-code-71808207)をご覧ください．
1. 履歴と変更点

    * 1.0.0-prerelease2 : 最初の公開版
    * 1.0.0-prerelease3 : IEnumerableの拡張メソッドShuffle()は結構使われそうなので，
名前空間AIWolf.Player.SampleからAIWolf.Libに移動しました．
    * 1.0.0-prerelease4 : Portable PDBを生成するようにproject.jsonを修正しました．
VS Codeでデバッグが可能になります．
    * 1.0.0 : 正式リリース．prerelease4と同等です．

---
このソフトウェアは，MITライセンスのもとで公開されています．[LICENSE](https://github.com/AIWolfSharp/AIWolf_NET/blob/master/LICENSE)を参照のこと.

using AIWolf.Lib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AIWolf.Player.Sample
{
    public sealed class Bodyguard : BasePlayer
    {
        // 人狼候補リスト
        List<Agent> werewolves = new List<Agent>();
        // 前日護衛したエージェント
        Agent guardedAgent;

        protected override void ChooseVoteCandidate()
        {
            // 自分や殺されたエージェントを人狼と判定していて，生存している占い師を投票先候補とする
            var candidates = DivinationList
                .Where(j => j.Result == Species.WEREWOLF && (j.Target == Me || Killed(j.Target)) && Alive(j.Agent))
                .Select(j => j.Agent).Distinct();
            if (candidates.Count() > 0)
            {
                if (!candidates.Contains(voteCandidate))
                {
                    voteCandidate = candidates.Shuffle().First();
                    // 以前の投票先から変わる場合，新たに推測発言と占い要請をする
                    if (CanTalk)
                    {
                        TalkQueue.Enqueue(new Content(new EstimateContentBuilder(voteCandidate, Role.WEREWOLF)));
                        TalkQueue.Enqueue(new Content(new RequestContentBuilder(null, new Content(new DivinationContentBuilder(voteCandidate)))));
                    }
                }
            }
            // 人狼候補がいない場合はランダム
            else
            {
                if (!AliveOthers.Contains(voteCandidate))
                {
                    voteCandidate = AliveOthers.Shuffle().First();
                }
            }
        }

        public override void Initialize(GameInfo gameInfo, GameSetting gameSetting)
        {
            base.Initialize(gameInfo, gameSetting);
            werewolves.Clear();
            guardedAgent = null;
        }

        public override Agent Guard()
        {
            Agent candidate = null;
            // 前日の護衛が成功しているようなら同じエージェントを護衛
            if (guardedAgent != null && Alive(guardedAgent) && CurrentGameInfo.LastDeadAgentList.Count == 0)
            {
                candidate = guardedAgent;
            }
            // 新しい護衛先の選定
            else
            {
                // 占い師をカミングアウトしていて，かつ人狼候補になっていないエージェントを探す
                var candidates = AliveOthers.Where(a => GetCoRole(a) == Role.SEER && !werewolves.Contains(a));
                // 見つからなければ霊媒師をカミングアウトしていて，かつ人狼候補になっていないエージェントを探す
                if (candidates.Count() == 0)
                {
                    candidates = AliveOthers.Where(a => GetCoRole(a) == Role.MEDIUM && !werewolves.Contains(a));
                }
                // それでも見つからなければ自分と人狼候補以外から護衛
                if (candidates.Count() == 0)
                {
                    candidates = AliveOthers.Where(a => a != Me && !werewolves.Contains(a));
                }
                // それでもいなければ自分以外から護衛
                if (candidates.Count() == 0)
                {
                    candidates = AliveOthers;
                }
                // 護衛候補からランダムに護衛
                candidate = candidates.Shuffle().First();
            }
            guardedAgent = candidate;
            return candidate;
        }

        public override string Whisper()
        {
            throw new NotImplementedException();
        }

        public override Agent Attack()
        {
            throw new NotImplementedException();
        }

        public override Agent Divine()
        {
            throw new NotImplementedException();
        }
    }
}

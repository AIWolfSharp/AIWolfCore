using AIWolf.Lib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AIWolf.Player.Sample
{
    public sealed class Possessed : BasePlayer
    {
        // 規定人狼数
        int numWolves;
        // カミングアウト済みか否か
        bool isCameout;
        // 偽占い結果リスト
        List<Judge> fakeDivinationList = new List<Judge>();
        // 偽占い結果を入れる待ち行列
        Queue<Judge> fakeDivinationQueue = new Queue<Judge>();
        // 偽占い済みエージェントのリスト
        List<Agent> divinedAgents = new List<Agent>();

        protected override void ChooseVoteCandidate()
        {
            // 自分や死亡したエージェントを人狼と判定していて，生存している占い師は人狼候補
            var werewolves = DivinationList
                .Where(j => j.Result == Species.WEREWOLF && (j.Target == Me || !Alive(j.Target))).Select(j => j.Agent);
            // 対抗カミングアウトのエージェントは投票先候補
            var rivals = AliveOthers.Where(a => !werewolves.Contains(a) && GetCoRole(a) == Role.SEER);
            // 人狼と判定したエージェントは投票先候補
            var fakeHumans = fakeDivinationQueue.Where(j => j.Result == Species.HUMAN).Select(j => j.Target).Distinct();
            var fakeWerewolves = fakeDivinationQueue.Where(j => j.Result == Species.WEREWOLF).Select(j => j.Target).Distinct();
            var candidates = rivals.Concat(fakeWerewolves).Distinct();
            // 候補がいなければ人間と判定していない村人陣営から
            if (candidates.Count() == 0)
            {
                candidates = AliveOthers.Where(a => !werewolves.Contains(a) && !fakeHumans.Contains(a));
                // それでも候補がいなければ村人陣営から
                if (candidates.Count() == 0)
                {
                    candidates = AliveOthers.Where(a => !werewolves.Contains(a));
                }
            }
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

        public override void Initialize(GameInfo gameInfo, GameSetting gameSetting)
        {
            base.Initialize(gameInfo, gameSetting);
            numWolves = gameSetting.RoleNumMap[Role.WEREWOLF];
            isCameout = false;
            fakeDivinationList.Clear();
            fakeDivinationQueue.Clear();
            divinedAgents.Clear();
        }

        public override void DayStart()
        {
            base.DayStart();
            // 偽の判定
            if (Day > 0)
            {
                Judge judge = GetFakeDivination();
                if (judge != null)
                {
                    fakeDivinationList.Add(judge);
                    fakeDivinationQueue.Enqueue(judge);
                    divinedAgents.Add(judge.Target);
                }
            }
        }

        public override string Talk()
        {
            // 初日カミングアウト
            if (!isCameout && Day == 0)
            {
                TalkQueue.Enqueue(new Content(new ComingoutContentBuilder(Me, Role.SEER)));
                isCameout = true;
            }
            // カミングアウトしたらこれまでの偽判定結果をすべて公開
            if (isCameout)
            {
                while (fakeDivinationQueue.Count > 0)
                {
                    Judge judge = fakeDivinationQueue.Dequeue();
                    TalkQueue.Enqueue(new Content(new DivinedResultContentBuilder(judge.Target, judge.Result)));
                }
            }
            return base.Talk();
        }

        /// <summary>
        /// 偽占い結果を返す
        /// </summary>
        /// <returns>偽占い結果</returns>
        Judge GetFakeDivination()
        {
            Agent target = null;
            var candidates = AliveOthers.Where(a => !divinedAgents.Contains(a) && GetCoRole(a) != Role.SEER);
            if (candidates.Count() > 0)
            {
                target = candidates.Shuffle().First();
            }
            else
            {
                target = AliveOthers.Shuffle().First();
            }
            // 偽人狼に余裕があれば，人狼と人間の割合を勘案して，30%の確率で人狼と判定
            Species result = Species.HUMAN;
            if (fakeDivinationList.Where(j => j.Result == Species.WEREWOLF).Count() < numWolves && new Random().NextDouble() < 0.3)
            {
                result = Species.WEREWOLF;
            }
            return new Judge(Day, Me, target, result);
        }
    }
}

using AIWolf.Lib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AIWolf.Player.Sample
{
    public sealed class Medium : BasePlayer
    {
        // カミングアウトする日
        int comingoutDay;
        // カミングアウト済みか否か
        bool isCameout;
        // 霊媒結果を入れる待ち行列
        Queue<Judge> identQueue = new Queue<Judge>();
        // 霊媒結果マップ
        Dictionary<Agent, Species> myIdentMap = new Dictionary<Agent, Species>();

        protected override void ChooseVoteCandidate()
        {
            // 霊媒師をカミングアウトしている他のエージェントは人狼候補
            var fakeMediums = AliveOthers.Where(a => GetCoRole(a) == Role.MEDIUM);
            // 自分や殺されたエージェントを人狼と判定，あるいは自分と異なる判定の占い師は人狼候補
            var fakeSeers = DivinationList
                .Where(j => (j.Result == Species.WEREWOLF && (j.Target == Me || Killed(j.Target)))
               || (myIdentMap.ContainsKey(j.Target) && j.Result != myIdentMap[j.Target])).Select(j => j.Agent);
            var candidates = fakeMediums.Concat(fakeSeers).Where(a => Alive(a)).Distinct();
            if(candidates.Count() > 0)
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
            comingoutDay = new int[] { 1, 2, 3 }.Shuffle().First();
            isCameout = false;
            identQueue.Clear();
            myIdentMap.Clear();
        }

        public override void DayStart()
        {
            base.DayStart();
            // 霊媒結果を待ち行列に入れる
            if (CurrentGameInfo.MediumResult != null)
            {
                identQueue.Enqueue(CurrentGameInfo.MediumResult);
                myIdentMap[CurrentGameInfo.MediumResult.Target] = CurrentGameInfo.MediumResult.Result;
            }
        }

        public override string Talk()
        {
            // カミングアウトする日になったら，あるいは霊媒結果が人狼だったら
            // あるいは霊媒師カミングアウトが出たらカミングアウト
            if (!isCameout && (Day >= comingoutDay
                    || (identQueue.Count > 0 && identQueue.Peek().Result == Species.WEREWOLF)
                    || IsCo(Role.MEDIUM)))
            {
                TalkQueue.Enqueue(new Content(new ComingoutContentBuilder(Me, Role.MEDIUM)));
                isCameout = true;
            }
            // カミングアウトしたらこれまでの霊媒結果をすべて公開
            if (isCameout)
            {
                while (identQueue.Count > 0)
                {
                    Judge ident = identQueue.Dequeue();
                    TalkQueue.Enqueue(new Content(new IdentContentBuilder(ident.Target, ident.Result)));
                }
            }
            return base.Talk();
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

        public override Agent Guard()
        {
            throw new NotImplementedException();
        }
    }
}

using AIWolf.Lib;
using System;
using System.Linq;

namespace AIWolf.Player.Sample
{
#if JHELP
    /// <summary>
    /// 村人プレイヤーの見本
    /// </summary>
#else
    /// <summary>
    /// Sample villager player.
    /// </summary>
#endif
    public sealed class Villager : BasePlayer
    {
        protected override void ChooseVoteCandidate()
        {
            // 自分や死亡したエージェントを人狼と判定していて，生存している占い師を投票先候補とする
            var candidates = DivinationList
                .Where(j => j.Result == Species.WEREWOLF && (j.Target == Me || !Alive(j.Target)) && Alive(j.Agent))
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
                voteCandidate = AliveOthers.Shuffle().First();
            }
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

using System;
using AI.Base;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AI.Gather
{
    public class Gather : BaseFSM
    {
        private float waitTime;
        private float startWaitTime = 3f;
        [ReadOnly]
        private readonly GatherBot gatherBot;
        public Gather(GatherBot _GatherBot) : base(_GatherBot.gameObject)
        {
            waitTime = startWaitTime;
            gatherBot = _GatherBot;
        }
        

        public override Type tick()
        {
            if (gatherBot.agent.remainingDistance < .25f)
            {
                if (waitTime < 0)
                {
                    ChangeStateToDropOff();
                    return typeof(DropOff);
                }
                else
                {
                    turnOnGatherAnimation();
                    waitTime -= Time.deltaTime;
                }
            }
            
            return typeof(Gather);
        }
        
        private void ChangeStateToDropOff()
        {
            turnOnWalkAnimation();
            gatherBot._Twig.SetActive(true);
            gatherBot.agent.destination = gatherBot._DropOffPoint.position;
            waitTime = startWaitTime;
        }

        private void turnOnWalkAnimation()
        {
            gatherBot._Animator.SetBool("isGather", false);
            gatherBot._Animator.SetBool("isWalk", true);
        }
        
        private void turnOnGatherAnimation()
        {
            gatherBot._Animator.SetBool("isGather", true);
            gatherBot._Animator.SetBool("isWalk", false);
        }
    }
}
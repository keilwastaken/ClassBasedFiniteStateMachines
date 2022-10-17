using System;
using AI.Base;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AI.Gather
{
    public class DropOff : BaseFSM
    {

        private float waitTime;
        private float startWaitTime = 3f;
        [ReadOnly]
        private readonly GatherBot gatherBot;
        public DropOff(GatherBot _GatherBot) : base(_GatherBot.gameObject)
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
                    ChangeStateToGather();
                    return typeof(Gather);
                }
                else
                {
                    turnOnDropOffAnimation();
                    waitTime -= Time.deltaTime;
                }
            }

            return typeof(DropOff);
        }
        
        private void ChangeStateToGather()
        {
            turnOnWalkAnimation();
            gatherBot._Twig.SetActive(false);
            gatherBot.agent.destination = gatherBot._GatherPoint.position;
            waitTime = startWaitTime;
        }

        private void turnOnWalkAnimation()
        {
            gatherBot._Animator.SetBool("isWalk", true);
            gatherBot._Animator.SetBool("isDropOff", false);
        }
        
        private void turnOnDropOffAnimation()
        {
            gatherBot._Animator.SetBool("isWalk", false);
            gatherBot._Animator.SetBool("isDropOff", true);
        }
    }
} 
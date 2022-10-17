using System;
using System.Collections;
using System.Collections.Generic;
using AI.Base;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Gather
{
    [RequireComponent(typeof(StateMachine))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class GatherBot : MonoBehaviour
    {
        
        public Transform _DropOffPoint;
        public Transform _GatherPoint;

        public GameObject _Twig;
        public Animator _Animator;
        [HideInInspector]
        public NavMeshAgent agent => GetComponent<NavMeshAgent>();
        private StateMachine stateMachine => GetComponent<StateMachine>();
        void Awake()
        {
            initalizeStateMachine();
        }

        private void initalizeStateMachine()
        {
            var state = new Dictionary<Type, BaseFSM>()
            {
                //{typeof(Move), new Move(this)},
                {typeof(Gather), new Gather(this)},
                {typeof(DropOff), new DropOff(this)}
            };
            
            stateMachine.SetStates(state);

            agent.destination = _GatherPoint.position;
        }
            
    }
}


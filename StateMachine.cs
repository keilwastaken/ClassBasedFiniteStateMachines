using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AI.Base
{
    public class StateMachine: MonoBehaviour
    {
        private Dictionary<Type, BaseFSM> availableStates;
        public BaseFSM CurrentState { get; private set; }
        public event Action<BaseFSM> OnStateChanged;

        public void SetStates(Dictionary<Type, BaseFSM> states)
        {
            availableStates = states;
        }

        private void Update()
        {
            if (CurrentState == null)
            {
                CurrentState = availableStates.Values.First();
            }

            var nextState = CurrentState?.tick();

            if (nextState != null && nextState != CurrentState?.GetType())
            {
                switchToNewState(nextState);
            }
        }

        private void switchToNewState(Type nextState)
        {
            CurrentState = availableStates[nextState];
            OnStateChanged?.Invoke(CurrentState);
        }
    }
}
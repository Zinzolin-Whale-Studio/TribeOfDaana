using System;
using System.Collections.Generic;
using _TribeOfDaana.Scripts.Runtime.Core.System;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Core.StateMachine
{
    public class StateMachine<TStateEnum> : MonoBehaviour where TStateEnum : Enum
    {
        #region Fields

        [SerializeField] private List<SubTypeReference<State<TStateEnum>>> _stateTypesToInstantiate;
        [SerializeField] private SubTypeReference<State<TStateEnum>> r;
        private List<State<TStateEnum>> _states;
        private State<TStateEnum> _currentState;
        #endregion

        #region MonoBehaviour

        private void Start()
        {
            InitStateMachine();
        }

        private void FixedUpdate()
        {
            StateMachineUpdate(Time.fixedDeltaTime);
        }

        #endregion
        
        public virtual void InitStateMachine()
        {
            InitStates();
        }

        private void InitStates()
        {
            foreach (State<TStateEnum> state in _states)
            {
                state.InitState();
            }
        }

        private void ChangeState(TStateEnum nextStateID)
        {
            if(_currentState != null && nextStateID.Equals(_currentState.GetStateID())) return;

            State<TStateEnum> nextState = GetState(nextStateID);
            if(nextState == null) return;

            if (_currentState != null)
            {
                _currentState.StateExit();
            }

            _currentState = nextState;
            
            _currentState.StateEnter();
        }

        private State<TStateEnum> GetState(TStateEnum stateID)
        {
            foreach (State<TStateEnum> state in _states)
            {
                if(state.GetStateID().Equals(stateID)) return state;
            }
            
            return null;
        }
        
        private void StateMachineUpdate(float deltaTime)
        {
            if(_currentState == null) return;
            _currentState.StateUpdate(deltaTime);
        }
        
    }
}

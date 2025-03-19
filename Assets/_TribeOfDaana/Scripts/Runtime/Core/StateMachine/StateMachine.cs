using System;
using System.Collections.Generic;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Core.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<State> _states;
        private State _currentState;
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
        
        public void InitStateMachine()
        {
            InitStates();
            
            ChangeState(StateID.Idle);
        }

        private void InitStates()
        {
            foreach (State state in _states)
            {
                state.InitState();
            }
        }

        private void ChangeState(StateID nextStateID)
        {
            if(_currentState != null && nextStateID == _currentState.GetStateID()) return;

            State nextState = GetState(nextStateID);
            if(nextState == null) return;

            if (_currentState != null)
            {
                _currentState.StateExit();
            }

            _currentState = nextState;
            
            _currentState.StateEnter();
        }

        private State GetState(StateID stateID)
        {
            foreach (State state in _states)
            {
                if(state.GetStateID() == stateID) return state;
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

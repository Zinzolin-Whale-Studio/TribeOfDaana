using System;
using System.Collections.Generic;
using _TribeOfDaana.Scripts.Runtime.Core.System;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Core.StateMachine
{
    public abstract class StateMachine<TState, TStateEnum, TController> : MonoBehaviour where TState : State<TStateEnum, TController> where TStateEnum : Enum  where TController : StateMachineController 
    {
        #region Fields

        [SerializeField] private List<SubTypeReference<State<TStateEnum, TController>>> _stateTypesToInstantiate;
        private List<TState> _states;
        private State<TStateEnum, TController> _currentState;

        [SerializeField] private TController _stateMachineController;
        #endregion

        #region MonoBehaviour

        private void Start()
        {
            StartStateMachine();
        }

        private void FixedUpdate()
        {
            StateMachineUpdate(Time.fixedDeltaTime);
        }

        #endregion
        
        public virtual void InitStateMachine()
        {
            _stateMachineController.InitializeController();
            
            CreateStates();
            InitStates(_stateMachineController);
            RegisterToStateEvents();
        }

        private void CreateStates()
        {
            _states = new List<TState>();

            List<Type> createdTypes = new List<Type>();
            
            foreach (SubTypeReference<State<TStateEnum, TController>> stateTypeReference in _stateTypesToInstantiate)
            {
                Type stateType = stateTypeReference.Type;
                
                if(createdTypes.Contains(stateType)) continue;
                
                createdTypes.Add(stateType);
               
                TState state = (TState)Activator.CreateInstance(stateType);
                _states.Add(state);
            }
        }

        protected virtual void InitStates(TController stateMachineController)
        {
            foreach (State<TStateEnum, TController> state in _states)
            {
                state.InitState(stateMachineController);
            }
        }

        private void RegisterToStateEvents()
        {
            foreach (State<TStateEnum, TController> state in _states)
            {
                state.StateChangeAsked += OnStateChangeAsked;
            }
        }

        public void StartStateMachine()
        {
            if(_states.Count <= 0) return;
            
            ChangeState(_states[0].GetStateID());
        }
        
        private void ChangeState(TStateEnum nextStateID)
        {
            if(_currentState != null && nextStateID.Equals(_currentState.GetStateID())) return;

            State<TStateEnum, TController> nextState = GetState(nextStateID);
            if(nextState == null) return;

            if (_currentState != null)
            {
                _currentState.StateExit();
            }

            _currentState = nextState;
            
            _currentState.StateEnter();
        }

        private void OnStateChangeAsked(TStateEnum nextStateID)
        {
            ChangeState(nextStateID);
        }
        
        private State<TStateEnum, TController> GetState(TStateEnum stateID)
        {
            foreach (State<TStateEnum, TController> state in _states)
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
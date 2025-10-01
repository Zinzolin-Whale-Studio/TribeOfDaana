using System;
using UnityEngine;
using UnityEngine.Events;

namespace _TribeOfDaana.Scripts.Runtime.Core.StateMachine
{
    [Serializable]
    public abstract class State<TStateEnum, TController> where TStateEnum : Enum where TController : StateMachineController
    {
        public UnityAction<TStateEnum> StateChangeAsked;
        
        public abstract void InitState(TController stateMachineController);

        public abstract TStateEnum GetStateID();

        public abstract void StateEnter();

        public abstract void StateUpdate(float deltaTime);

        public abstract void StateExit();
    }
}

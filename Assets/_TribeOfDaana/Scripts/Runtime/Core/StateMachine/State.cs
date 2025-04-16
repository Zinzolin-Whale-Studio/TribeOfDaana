using System;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Core.StateMachine
{
    [Serializable]
    public abstract class State<TStateEnum> where TStateEnum : Enum
    {
        public abstract void InitState();

        public abstract TStateEnum GetStateID();

        public abstract void StateEnter();

        public abstract void StateUpdate(float deltaTime);

        public abstract void StateExit();
    }
}

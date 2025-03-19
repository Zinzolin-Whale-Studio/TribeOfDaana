using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Core.StateMachine
{
    public abstract class State : MonoBehaviour
    {
        public abstract void InitState();

        public abstract StateID GetStateID();

        public abstract void StateEnter();

        public abstract void StateUpdate(float deltaTime);

        public abstract void StateExit();
    }
}

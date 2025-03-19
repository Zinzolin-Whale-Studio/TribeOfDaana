using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Core.StateMachine
{
    public class StateIdle : State
    {
        public override void InitState()
        {
        }

        public override StateID GetStateID()
        {
            return StateID.Idle;
        }

        public override void StateEnter()
        {
            Debug.LogWarning("Idle Enter");
        }

        public override void StateUpdate(float deltaTime)
        {
            Debug.LogWarning("Idle Update");
        }

        public override void StateExit()
        {
            Debug.LogWarning("Idle Exit");
        }
    }
}

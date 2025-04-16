using _TribeOfDaana.Scripts.Runtime.Core.StateMachine;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.ExplorationSystem.StateMachines.ExplorationPlayerStates
{
    public class ExplorationStateIdle : State<ExplorationStateID>
    {
        public override void InitState()
        {
        }
    
        public override ExplorationStateID GetStateID()
        {
            return ExplorationStateID.Idle;
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

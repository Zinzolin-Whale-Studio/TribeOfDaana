using _TribeOfDaana.Scripts.Runtime.Core.StateMachine;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.ExplorationSystem.StateMachines.ExplorationPlayerStates
{
    public class ExplorationStateWalk : State<ExplorationStateID, ExplorationController>
    {
        public override void InitState(ExplorationController stateMachineController)
        {
            stateMachineController.WalkEnded += OnWalkEnded;
        }
        
        #region State Implementation
        public override ExplorationStateID GetStateID()
        {
            return ExplorationStateID.Walk;
        }
    
        public override void StateEnter()
        {
            Debug.LogWarning("Walk Enter");
        }
    
        public override void StateUpdate(float deltaTime)
        {
            Debug.LogWarning("Walk Update");
        }
    
        public override void StateExit()
        {
            Debug.LogWarning("Walk Exit");
        }
        #endregion
        
        #region React to controller events
        private void OnWalkEnded()
        {
            StateChangeAsked?.Invoke(ExplorationStateID.Idle);
        }
        #endregion
    }
}
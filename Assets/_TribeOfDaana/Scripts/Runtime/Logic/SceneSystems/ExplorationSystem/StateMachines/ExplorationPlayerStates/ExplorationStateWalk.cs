using _TribeOfDaana.Scripts.Runtime.Core.StateMachine;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.ExplorationSystem.StateMachines.ExplorationPlayerStates
{
    public class ExplorationStateWalk : ExplorationStateBase
    {
        private Vector2 _direction;
        private float _walkSpeed;
        
        public override void InitState(ExplorationController stateMachineController)
        {
            stateMachineController.WalkPerformed += OnWalkPerformed;
            stateMachineController.WalkEnded += OnWalkEnded;
        }

        public override void InitExplorationState(Rigidbody2D rb, ExplorationPlayerDataSO explorationPlayerDataSo)
        {
            base.InitExplorationState(rb, explorationPlayerDataSo);

            _walkSpeed = explorationPlayerDataSo.WalkSpeed;
        }

        #region State Implementation
        public override ExplorationStateID GetStateID()
        {
            return ExplorationStateID.Walk;
        }
    
        public override void StateEnter()
        {
            // Debug.LogWarning("Walk Enter");
        }
    
        public override void StateUpdate(float deltaTime)
        {
            // Debug.LogWarning("Walk Update");

            _movedRb.linearVelocity = _direction.normalized * (_walkSpeed * deltaTime);
        }
    
        public override void StateExit()
        {
            // Debug.LogWarning("Walk Exit");
        }
        #endregion
        
        #region React to controller events

        private void OnWalkPerformed(Vector2 direction)
        {
            _direction = direction;
        }
        
        private void OnWalkEnded()
        {
            StateChangeAsked?.Invoke(ExplorationStateID.Idle);
        }
        #endregion
    }
}
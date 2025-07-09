using System;
using _TribeOfDaana.Scripts.Runtime.Core.StateMachine;
using _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.ExplorationSystem.StateMachines.ExplorationPlayerStates;
using UnityEngine;
using UnityEngine.Serialization;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.ExplorationSystem.StateMachines
{
    public class ExplorationPlayerStateMachine : StateMachine<ExplorationStateBase,ExplorationStateID, ExplorationController>
    {
        [SerializeField] private Rigidbody2D _playerRigidBody;
        [SerializeField] private ExplorationPlayerDataSO _explorationPlayerDataSO;

        protected override void InitStates(ExplorationController stateMachineController)
        {
            base.InitStates(stateMachineController);

            InitExplorationStates();
        }

        private void InitExplorationStates()
        {
            foreach (ExplorationStateBase state in _states)
            {
                state.InitExplorationState(_playerRigidBody,_explorationPlayerDataSO);
            }
        }
    }
}

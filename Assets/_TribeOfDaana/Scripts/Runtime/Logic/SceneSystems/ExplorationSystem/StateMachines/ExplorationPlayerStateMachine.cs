using System;
using _TribeOfDaana.Scripts.Runtime.Core.StateMachine;
using _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.ExplorationSystem.StateMachines.ExplorationPlayerStates;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.ExplorationSystem.StateMachines
{
    public class ExplorationPlayerStateMachine : StateMachine<ExplorationStateBase,ExplorationStateID, ExplorationController>
    {
        [SerializeField] private Rigidbody2D _playerRigidBody;

        protected override void InitStates(ExplorationController stateMachineController)
        {
            base.InitStates(stateMachineController);

            InitExplorationStates();
        }

        private void InitExplorationStates()
        {
        }
    }
}

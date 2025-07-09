using _TribeOfDaana.Scripts.Runtime.Core.StateMachine;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.ExplorationSystem.StateMachines.ExplorationPlayerStates
{
    public abstract class ExplorationStateBase : State<ExplorationStateID, ExplorationController>
    {
        protected Rigidbody2D _movedRb;
        
        public virtual void InitExplorationState(Rigidbody2D rb, ExplorationPlayerDataSO explorationPlayerDataSo)
        {
            _movedRb = rb;
        }
    }
}
  
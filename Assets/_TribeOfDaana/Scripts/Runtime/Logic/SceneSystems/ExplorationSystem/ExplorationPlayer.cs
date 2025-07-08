using _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.ExplorationSystem.StateMachines;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.ExplorationSystem
{
    public class ExplorationPlayer : MonoBehaviour
    {
        [SerializeField] private ExplorationPlayerStateMachine _explorationPlayerStateMachine;

        public void InitPlayer()
        {
            _explorationPlayerStateMachine.InitStateMachine();
        }
    }
}

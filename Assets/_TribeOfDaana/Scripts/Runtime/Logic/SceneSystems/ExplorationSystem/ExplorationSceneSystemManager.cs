using _TribeOfDaana.Scripts.Runtime.Core.Manager;
using _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.ExplorationSystem.StateMachines;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.ExplorationSystem
{
    public class ExplorationSceneSystemManager : SceneSystemManager<ExplorationSceneSystemManager>
    {
        [SerializeField] private ExplorationPlayerStateMachine _explorationPlayerStateMachine;
        
        public override bool InitializeManager()
        {
            Debug.Log("Initialized Exploration System Manager");
            
            _explorationPlayerStateMachine.InitStateMachine();
            
            return true;
        }
    }
}

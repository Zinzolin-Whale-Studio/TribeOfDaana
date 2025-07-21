using _TribeOfDaana.Scripts.Runtime.Core.Manager;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.ExplorationSystem
{
    public class ExplorationSceneSystemManager : SceneSystemManager<ExplorationSceneSystemManager>
    {
        [SerializeField] private ExplorationPlayer _explorationPlayer;
        
        public override bool InitializeManager()
        {
            Debug.Log("Initialized Exploration System Manager");
            
            _explorationPlayer.InitPlayer();
            
            return true;
        }
    }
}

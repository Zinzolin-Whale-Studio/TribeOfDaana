using _TribeOfDaana.Scripts.Runtime.Core.Manager;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.ExplorationSystem
{
    public class ExplorationSceneSystemManager : SceneSystemManager<ExplorationSceneSystemManager>
    {
        public override bool InitializeManager()
        {
            Debug.Log("Initialized Exploration System Manager");
            return true;
        }
    }
}

using _TribeOfDaana.Scripts.Runtime.Core.Manager;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.ExplorationSystem
{
    public class ExplorationSystemManager : SceneSystemManager<ExplorationSystemManager>
    {
        public override bool InitializeManager()
        {
            Debug.Log("Initialized Exploration System Manager");
            return true;
        }
    }
}

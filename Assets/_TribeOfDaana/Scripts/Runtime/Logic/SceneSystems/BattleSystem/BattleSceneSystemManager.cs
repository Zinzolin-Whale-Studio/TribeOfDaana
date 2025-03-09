using _TribeOfDaana.Scripts.Runtime.Core.Manager;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.BattleSystem
{
    public class BattleSceneSystemManager : SceneSystemManager<BattleSceneSystemManager>
    {
        public override bool InitializeManager()
        {
            Debug.LogWarning("Initialized Battle System Manager");
            return true;
        }
    }
}

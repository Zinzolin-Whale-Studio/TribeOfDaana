using _TribeOfDaana.Scripts.Runtime.Core.Diagnostic;
using _TribeOfDaana.Scripts.Runtime.Core.Manager;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.MapScene
{
    public class MapSceneManager : SceneSystemManager<MapSceneManager>, ISceneSystemManager
    {
        public override bool InitializeManager()
        {
            MapSceneDebug.Log("Initialize MapScene Manager");
            MapSceneDebug.LogWarning("Initialize MapScene Manager");
            MapSceneDebug.LogError("Initialize MapScene Manager");
            return true;
        }
    }
}

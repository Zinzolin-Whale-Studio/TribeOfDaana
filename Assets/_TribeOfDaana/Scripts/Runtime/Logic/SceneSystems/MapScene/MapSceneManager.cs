using _TribeOfDaana.Scripts.Runtime.Core.Manager;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.MapScene
{
    public class MapSceneManager : SceneSystemManager<MapSceneManager>, ISceneSystemManager
    {
        [SerializeField] private MapScenePlayerController m_playerController;
        [SerializeField] private Map.Map m_map;
        
        public override bool InitializeManager()
        {
            if (!m_playerController.InitializeController())
            {
                MapSceneDebug.LogError("Failed to initialize : MapScenePlayerController");
                return false;
            }
            
            if (!m_map.InitializeMap())
            {
                MapSceneDebug.LogError("Failed to initialize : Map");
                return false;
            }
            
            return true;
        }

        public override bool StartScene()
        {
            if (!m_playerController.StartController())
            {
                MapSceneDebug.LogError("Failed to start: MapScenePlayerController");
                return false;
            }            
            
            if (!m_map.StartMap())
            {
                MapSceneDebug.LogError("Failed to start : Map");
                return false;
            }
            
            return true;
        }
    }
}

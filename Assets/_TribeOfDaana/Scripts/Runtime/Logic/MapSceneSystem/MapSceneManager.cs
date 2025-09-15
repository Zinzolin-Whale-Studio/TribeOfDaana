using _TribeOfDaana.Scripts.Runtime.Core.Manager;
using _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem.MapSubsystem;
using _TribeOfDaana.Scripts.Runtime.UI.MapSceneSystem;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem
{
    public class MapSceneManager : SceneSystemManager<MapSceneManager>, ISceneSystemManager
    {
        [SerializeField] private MapScenePlayerController m_playerController;
        [SerializeField] private Map m_map;

        [SerializeField] private MapSceneCanvas m_mapSceneCanvas;
        
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
            
            if (!m_mapSceneCanvas.InitMapSceneCanvas(m_map))
            {
                MapSceneDebug.LogError("Failed to initialize : MapSceneCanvas");
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
            
            if (!m_mapSceneCanvas.StartMapSceneCanvas())
            {
                MapSceneDebug.LogError("Failed to start : MapSceneCanvas");
                return false;
            }
            
            return true;
        }
    }
}

using _TribeOfDaana.Scripts.Runtime.Core.Manager;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.MapScene
{
    public class MapSceneManager : SceneSystemManager<MapSceneManager>, ISceneSystemManager
    {
        [SerializeField] private MapScenePlayerController m_playerController;
        
        public override bool InitializeManager()
        {
            if (!m_playerController.InitializeController())
            {
                Debug.LogError("Failed to initialize : MapScenePlayerController");
                return false;
            }
            
            return true;
        }

        public override bool StartScene()
        {
            return true;
        }
    }
}

using System;
using _TribeOfDaana.Scripts.Runtime.Core.Manager;
using _TribeOfDaana.Scripts.Runtime.Logic.Levels;
using _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem.MapSubsystem;
using _TribeOfDaana.Scripts.Runtime.Logic.SceneLoadingSubsystem;
using _TribeOfDaana.Scripts.Runtime.UI.MapSceneSystem;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem
{
    public class MapSceneManager : SceneSystemManager<MapSceneManager>, ISceneSystemManager
    {
        //Logic
        [SerializeField] private MapScenePlayerController m_playerController;
        [SerializeField] private Map m_map;

        //UI
        [SerializeField] private MapSceneCanvas m_mapSceneCanvas;

        //Level loading
        [SerializeField] private LevelInfoDatabase m_levelInfoDatabase;
        [SerializeField] private LoadSceneComponent m_loadSceneComponent;
        [SerializeField] private string m_campSceneName;
        
        
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

            m_mapSceneCanvas.EnterLevelButtonClicked += EnterLevel;
            
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

        private void EnterLevel(string levelKey)
        {
            if (m_levelInfoDatabase == null)
            {
                MapSceneDebug.LogError("Can't enter level : Missing reference to a LevelInfoDatabase");
                throw new NullReferenceException();
            }
            m_levelInfoDatabase.SetLevelToLoadKey(levelKey);
            
            if (m_loadSceneComponent == null)
            {
                MapSceneDebug.LogError("Can't enter level : Missing reference to a LoadSceneComponent");
                throw new NullReferenceException();
            }
            
            m_loadSceneComponent.AskToLoadScene(m_campSceneName);
        }
    }
}

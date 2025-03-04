using System;
using _TribeOfDaana.Scripts.Runtime.Core.Manager;
using _TribeOfDaana.Scripts.Runtime.Logic.SceneLoadingSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _TribeOfDaana.Scripts.Runtime.Logic.GameManager
{
    public class GameManager : MonoBehaviour, IGameWideManager
    {
        [SerializeField] private LoadingManager m_loadingManager;

        private GameManager()
        {
        }

        void OnEnable()
        {
            if(!InitializeManager()) Debug.LogError("Game Manager failed to initialize");
        }

        public bool InitializeManager()
        {
            if (!m_loadingManager.InitializeManager())
            {
                Debug.LogError("Loading Manager failed to initialize");
                return false;
            }

            if(!SynchronizeWithScene())
            {
                Debug.LogError("Game Manager failed to synchronize with scene");
                return false;
            }
            
            SubscribeToSceneManagerEvents();
            
            return true;
        }

        #region IGameWideManager

        public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if(!SynchronizeWithScene()) Debug.LogError("Game Manager failed to synchronize with scene");
        }

        public bool SynchronizeWithScene()
        {
            if (!m_loadingManager.SynchronizeWithScene())
            {
                Debug.LogError("Loading Manager failed to synchronize with scene");
                return false;
            }

            return true;
        }

        #endregion
        
        
        #region SceneManager

        private void SubscribeToSceneManagerEvents()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void UnsubscribeToSceneManagerEvents()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        #endregion

        private void OnDisable()
        {
            UnsubscribeToSceneManagerEvents();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using _TribeOfDaana.Scripts.Runtime.Core.Manager;
using _TribeOfDaana.Scripts.Runtime.Logic.GameSubsystems.SceneLoading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _TribeOfDaana.Scripts.Runtime.Logic.GameManager
{
    public class GameManager : Manager<GameManager>
    {
        [SerializeField] private LoadingSubsystemManager m_loadingSubsystemManager;

        private GameManager()
        {
        }

        void OnEnable()
        {
            if(!InitializeManager()) Debug.LogError("Game Manager failed to initialize");
        }

        public override bool InitializeManager()
        {
            if (!m_loadingSubsystemManager.InitializeManager())
            {
                Debug.LogError("Loading Manager failed to initialize");
                return false;
            }

            if(!SynchronizeSubsystemsWithScene())
            {
                Debug.LogError("Game Manager failed to synchronize with scene");
                return false;
            }
            
            SubscribeToSceneManagerEvents();
            
            return true;
        }

        private bool SynchronizeSubsystemsWithScene()
        {
            if (!m_loadingSubsystemManager.SynchronizeWithScene())
            {
                Debug.LogError("Loading Manager failed to synchronize with scene");
                return false;
            }

            return true;
        }

        private bool FindSceneSystemManager(out ISceneSystemManager foundSceneSystemManager)
        {
            foundSceneSystemManager = null;
            
            IEnumerable<ISceneSystemManager> sceneSystemManagers = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID).OfType<ISceneSystemManager>();

            if (!sceneSystemManagers.Any()) return false;

            foundSceneSystemManager = sceneSystemManagers.First();
            
            return foundSceneSystemManager != null;
        }
        
        #region React to SceneManager events
        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if(!SynchronizeSubsystemsWithScene()) Debug.LogError("Game Manager failed to synchronize with scene");

            if (!FindSceneSystemManager(out ISceneSystemManager foundSceneSystemManager))
            {
                Debug.LogError("Game Manager failed to find Scene System Manager");
                return;
            }

            if(!foundSceneSystemManager.InitializeManager()) Debug.LogError("Game Manager failed to initialize Scene System Manager");;
            
            if(!foundSceneSystemManager.StartScene()) Debug.LogError("Error when starting the scene");;
        }

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

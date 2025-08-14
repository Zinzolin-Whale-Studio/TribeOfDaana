using System.Collections.Generic;
using System.Linq;
using _TribeOfDaana.Scripts.Runtime.Core.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _TribeOfDaana.Scripts.Runtime.Logic.GameSubsystems.SceneLoading
{
    public class LoadingSubsystemManager : Manager<LoadingSubsystemManager>, IGameSubsystemManager
    {
        private List<LoadSceneComponent> m_loadSceneComponents;

        private LoadingSubsystemManager()
        {
            m_loadSceneComponents = new List<LoadSceneComponent>();
        }
        
        public override bool InitializeManager()
        {
            return true;
        }

        public bool SynchronizeWithScene()
        {
            m_loadSceneComponents = FindObjectsByType<LoadSceneComponent>(FindObjectsInactive.Include,
                FindObjectsSortMode.InstanceID).ToList();

            foreach (LoadSceneComponent loadSceneComponent in m_loadSceneComponents)
            {
                SubscribeToLoadSceneComponentActions(loadSceneComponent);
            }

            return true;
        }

        #region LoadSceneComponent
        
        #region Subscribe/Unsubscribe to events
        private void SubscribeToLoadSceneComponentActions(LoadSceneComponent loadSceneComponent)
        {
            loadSceneComponent.LoadOfSceneAsked += OnLoadOfSceneAsked;
        }

        private void UnsubscribeToLoadSceneComponentActions(LoadSceneComponent loadSceneComponent)
        {
            loadSceneComponent.LoadOfSceneAsked -= OnLoadOfSceneAsked;
        }
        #endregion

        #region React to LoadSceneComponent events

        private void OnLoadOfSceneAsked(string sceneName)
        {
            if(SceneUtility.GetBuildIndexByScenePath(sceneName) == -1) return;
            
            SceneManager.LoadScene(sceneName);
        }
        
        #endregion
        
        #endregion

        private void OnDisable()
        {
            foreach (LoadSceneComponent loadSceneComponent in m_loadSceneComponents)
            {
                UnsubscribeToLoadSceneComponentActions(loadSceneComponent);
            }
        }
    }
}

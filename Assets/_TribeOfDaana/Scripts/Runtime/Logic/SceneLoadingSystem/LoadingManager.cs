using System;
using System.Collections.Generic;
using System.Linq;
using _TribeOfDaana.Scripts.Runtime.Core.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneLoadingSystem
{
    public class LoadingManager : MonoBehaviour, IManager
    {
        private List<LoadSceneComponent> m_loadSceneComponents;

        private LoadingManager()
        {
            m_loadSceneComponents = new List<LoadSceneComponent>();
        }
        
        public bool InitializeManager(params IManager[] requiredManagers)
        {
            m_loadSceneComponents = FindObjectsByType<LoadSceneComponent>(FindObjectsInactive.Include,
                FindObjectsSortMode.InstanceID).ToList();

            foreach (LoadSceneComponent loadSceneComponent in m_loadSceneComponents)
            {
                SubscribeToLoadSceneComponentActions(loadSceneComponent);
            }

            return true;
        }

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
        
        private void OnDestroy()
        {
            foreach (LoadSceneComponent loadSceneComponent in m_loadSceneComponents)
            {
                UnsubscribeToLoadSceneComponentActions(loadSceneComponent);
            }
        }
    }
}

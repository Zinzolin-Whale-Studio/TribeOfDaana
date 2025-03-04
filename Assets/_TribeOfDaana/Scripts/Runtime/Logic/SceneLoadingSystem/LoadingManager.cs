using System;
using System.Collections.Generic;
using System.Linq;
using _TribeOfDaana.Scripts.Runtime.Core.Manager;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneLoadingSystem
{
    public class LoadingManager : MonoBehaviour, IManager
    {
        private List<LoadSceneComponent> m_loadSceneComponents;

        private LoadingManager()
        {
            m_loadSceneComponents = new List<LoadSceneComponent>();
        }
        
        public void InitializeManager()
        {
            m_loadSceneComponents = FindObjectsByType<LoadSceneComponent>(FindObjectsInactive.Include,
                FindObjectsSortMode.InstanceID).ToList();

            foreach (LoadSceneComponent loadSceneComponent in m_loadSceneComponents)
            {
                
            }
        }

        #region Subscribe/Unsubscribe to events
        private void SubscribeToLoadSceneComponentActions(LoadSceneComponent loadSceneComponent)
        {
            
        }
        
        private void UnsubscribeToLoadSceneComponentActions(LoadSceneComponent loadSceneComponent)
        {
            
        }
        #endregion

        private void OnDestroy()
        {
            foreach (LoadSceneComponent loadSceneComponent in m_loadSceneComponents)
            {
                
            }
        }
    }
}

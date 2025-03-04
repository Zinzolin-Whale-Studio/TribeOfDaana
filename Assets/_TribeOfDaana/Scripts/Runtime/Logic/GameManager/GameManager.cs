using System;
using _TribeOfDaana.Scripts.Runtime.Core.Manager;
using _TribeOfDaana.Scripts.Runtime.Logic.SceneLoadingSystem;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.GameManager
{
    public class GameManager : MonoBehaviour, IManager
    {
        [SerializeField] private LoadingManager m_loadingManager;

        private GameManager()
        {
        }

        void Start()
        {
            InitializeManager();
        }

        public void InitializeManager()
        {
            m_loadingManager.InitializeManager();
        }
    }
}

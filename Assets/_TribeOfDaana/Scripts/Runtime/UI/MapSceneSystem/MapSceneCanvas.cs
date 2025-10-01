using _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem.MapSubsystem;
using _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem.MapSubsystem.Point;
using _TribeOfDaana.Scripts.Runtime.UI.MapSceneSystem.MapSubsystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _TribeOfDaana.Scripts.Runtime.UI.MapSceneSystem
{
    public class MapSceneCanvas : MonoBehaviour
    {
        private Map m_map;
        
        [SerializeField] private MapPointInfoUI mapPointInfoUI;
        [SerializeField] private Button m_stopObservingButton;
        [SerializeField] private Button m_enterLevelButton;

        public event UnityAction<string> EnterLevelButtonClicked; //string is level key
        
        #region GameLoop
        public bool InitMapSceneCanvas(Map map)
        {
            if (map == null) return false;
            
            m_map = map;
            m_map.MapPointObserved += OnMapPointObserved;

            m_stopObservingButton.onClick.AddListener(HideMapPointInfoUI);
            m_enterLevelButton.onClick.AddListener(UserEnterLevel);
            return true;
        }

        public bool StartMapSceneCanvas()
        {
            return true;
        }
        #endregion

        private void OnMapPointObserved(MapPointInfo mapPointInfo)
        {
            ShowMapPointInfoUI(mapPointInfo);
        }

        private void ShowMapPointInfoUI(MapPointInfo mapPointInfo)
        {
            mapPointInfoUI.gameObject.SetActive(true);
            mapPointInfoUI.UpdateInfo(mapPointInfo);
            
            m_stopObservingButton.gameObject.SetActive(true);
            m_enterLevelButton.gameObject.SetActive(true);
        }
        
        private void HideMapPointInfoUI()
        {
            mapPointInfoUI.gameObject.SetActive(false);
            m_stopObservingButton.gameObject.SetActive(false);
            m_enterLevelButton.gameObject.SetActive(false);
        }
        
        private void UserEnterLevel()
        {
            EnterLevelButtonClicked?.Invoke(m_map.GetObservedMapPointInfo());
        }
    }
}

using _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem.MapSubsystem;
using _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem.MapSubsystem.Point;
using _TribeOfDaana.Scripts.Runtime.UI.MapSceneSystem.MapSubsystem;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _TribeOfDaana.Scripts.Runtime.UI.MapSceneSystem
{
    public class MapSceneCanvas : MonoBehaviour
    {
        [SerializeField] private MapPointInfoUI mapPointInfoUI;
        [SerializeField] private Button _stopObservingButton;

        #region GameLoop
        public bool InitMapSceneCanvas(Map map)
        {
            map.MapPointObserved += OnMapPointObserved;

            _stopObservingButton.onClick.AddListener(HideMapPointInfoUI);
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
            
            _stopObservingButton.gameObject.SetActive(true);
        }
        
        private void HideMapPointInfoUI()
        {
            mapPointInfoUI.gameObject.SetActive(false);
            _stopObservingButton.gameObject.SetActive(false);
        }
    }
}

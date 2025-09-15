using _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem.MapSubsystem;
using _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem.MapSubsystem.Point;
using _TribeOfDaana.Scripts.Runtime.UI.MapSceneSystem.MapSubsystem;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.UI.MapSceneSystem
{
    public class MapSceneCanvas : MonoBehaviour
    {
        [SerializeField] private MapPointInfoWorldCanvas _mapPointInfoWorldCanvas;

        #region GameLoop
        public bool InitMapSceneCanvas(Map map)
        {
            map.MapPointObserved += OnMapPointObserved;
            return true;
        }

        public bool StartMapSceneCanvas()
        {
            return true;
        }
        

        #endregion

        private void OnMapPointObserved(MapPointInfo mapPointInfo)
        {
            _mapPointInfoWorldCanvas.gameObject.SetActive(true);
            _mapPointInfoWorldCanvas.Show(mapPointInfo);
        }
    }
}

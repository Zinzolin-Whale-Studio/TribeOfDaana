using _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem.MapSubsystem;
using _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem.MapSubsystem.Point;
using _TribeOfDaana.Scripts.Runtime.UI.MapSceneSystem.MapSubsystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace _TribeOfDaana.Scripts.Runtime.UI.MapSceneSystem
{
    public class MapSceneCanvas : MonoBehaviour
    {
        [FormerlySerializedAs("_mapPointInfoWorldCanvas")] [SerializeField] private MapPointInfoUI mapPointInfoUI;

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
            mapPointInfoUI.gameObject.SetActive(true);
            mapPointInfoUI.UpdateInfo(mapPointInfo);
        }
    }
}

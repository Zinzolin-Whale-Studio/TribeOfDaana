using System.Collections.Generic;
using _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem.MapSubsystem.Point;
using UnityEngine;
using UnityEngine.Events;

namespace _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem.MapSubsystem
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private List<MapPoint> m_mapPoints = new List<MapPoint>();
        private MapPoint m_startPoint = null;

        
        private MapPoint m_observedPoint = null; // Point that's currently selected by the player to see it's information 
        private MapPoint m_currentPoint = null; // Point on which the character is located

        #region Actions

        public event UnityAction<MapPointInfo> MapPointObserved;

        #endregion
        
        public bool InitializeMap()
        {
            
            
            SubscribeToAllMapPointsEvents();

            return true;
        }
        
        public bool StartMap()
        {
            //Empty for now
            return true;
        }

        #region Points
        public string GetObservedMapPointInfo()
        {
            return m_observedPoint.MapPointInfo.LevelKey;
        }
        
        private void SubscribeToAllMapPointsEvents()
        {
            foreach (MapPoint mapPoint in m_mapPoints)
            {
                mapPoint.Clicked += OnMapPointClicked;
            }
        }

        private void OnMapPointClicked(MapPoint pointClicked)
        {
            if(pointClicked == null) return;

            m_observedPoint = pointClicked;
            
            MapPointObserved?.Invoke(m_observedPoint.MapPointInfo);
        }
        #endregion

#if UNITY_EDITOR
        #region Editor
        public void Editor_AddPoint(MapPoint mapPoint)
        {
            if (m_mapPoints.Contains(mapPoint)) return;
            
            m_mapPoints.Add(mapPoint);
        }

        public void Editor_RemovePoint(MapPoint mapPoint)
        {
            m_mapPoints.Remove(mapPoint);
        }
        #endregion
#endif
    }
}

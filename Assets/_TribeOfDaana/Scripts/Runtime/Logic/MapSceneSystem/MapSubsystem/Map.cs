using System.Collections.Generic;
using _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem.MapSubsystem.Point;
using UnityEngine;
using UnityEngine.Events;

namespace _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem.MapSubsystem
{
    
    public class Map : MonoBehaviour
    {
        [SerializeField] private List<MapPoint> m_mapPoints;
        [SerializeField] private MapPoint m_startPoint;

        private MapPoint m_observedPoint; // Point that's currently selected by the player to see it's information 
        private MapPoint m_currentPoint; // Point on which the character is located

        #region Actions

        /// <summary>
        /// Vector 3 is for the position of the point in world space
        /// </summary>
        public event UnityAction<MapPointInfo> MapPointObserved;

        #endregion
        
        public bool InitializeMap()
        {
            m_currentPoint = m_startPoint;
            
            SubscribeToMapPointEvents();

            return true;
        }
        
        public bool StartMap()
        {
            //Empty for now
            return true;
        }

        private void SubscribeToMapPointEvents()
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
    }
}

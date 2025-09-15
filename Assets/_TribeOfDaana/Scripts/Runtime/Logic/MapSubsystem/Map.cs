using System.Collections.Generic;
using _TribeOfDaana.Scripts.Runtime.Logic.MapSubsystem.Point;
using UnityEngine;
using UnityEngine.Events;

namespace _TribeOfDaana.Scripts.Runtime.Logic.MapSubsystem
{
    
    public class Map : MonoBehaviour
    {
        [SerializeField] private List<MapPoint> _mapPoints;
        [SerializeField] private MapPoint _startPoint;

        private MapPoint _observedPoint; // Point that's currently selected by the player to see it's information 
        private MapPoint _currentPoint; // Point on which the character is located

        #region Actions

        /// <summary>
        /// Vector 3 is for the position of the point in world space
        /// </summary>
        public event UnityAction<Vector3,MapPointInfo> MapPointObserved;

        #endregion
        
        public bool InitializeMap()
        {
            _currentPoint = _startPoint;
            
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
            foreach (MapPoint mapPoint in _mapPoints)
            {
                mapPoint.Clicked += OnMapPointClicked;
            }
        }

        private void OnMapPointClicked(MapPoint pointClicked)
        {
            if(pointClicked == null) return;

            _observedPoint = pointClicked;
            
            MapPointObserved?.Invoke(_observedPoint.transform.position, _observedPoint.PointInfo);
        }
        
        private void UpdateCurrentPoint(MapPoint mapPoint)
        {
            //Current point shouldn't be null when trying to update it because the map should always have a current point selected.
            //In the case of the Start of the Scene, we don't call update we just SetCurrentPoint directly, bypassing the logic of this function.
            if(mapPoint == null || _currentPoint == null) return;

            _currentPoint = mapPoint;
        }
    }
}

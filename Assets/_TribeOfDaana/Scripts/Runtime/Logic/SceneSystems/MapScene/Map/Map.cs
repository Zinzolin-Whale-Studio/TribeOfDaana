using System.Collections.Generic;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.MapScene.Map
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private List<MapPoint> _mapPoints;
        [SerializeField] private MapPoint _startPoint;
        private MapPoint _currentPoint;

        public bool InitializeMap()
        {
            SetCurrentPoint(_startPoint);
            
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
                mapPoint.MapPointClicked += OnMapPointClicked;
            }
        }

        private void OnMapPointClicked(MapPoint pointClicked)
        {
            UpdateCurrentPoint(pointClicked);
        }

        private void SetCurrentPoint(MapPoint mapPoint)
        {
            _currentPoint = mapPoint;
            _currentPoint.Select();
        }
        
        private void UpdateCurrentPoint(MapPoint mapPoint)
        {
            //Current point shouldn't be null when trying to update it because the map should always have a current point selected.
            //In the case of the Start of the Scene, we don't call update we just SetCurrentPoint directly, bypassing the logic of this function.
            if(mapPoint == null || _currentPoint == null) return;

            if(!_currentPoint.NeighborPoints.Contains(mapPoint)) return;
            
            _currentPoint.Unselect();

            SetCurrentPoint(mapPoint);
        }
    }
}

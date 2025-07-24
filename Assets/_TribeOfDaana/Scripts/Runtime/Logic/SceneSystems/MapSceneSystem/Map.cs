using System;
using System.Collections.Generic;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.MapSceneSystem
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private List<MapPoint> _mapPoints;
        [SerializeField] private MapPoint _startPoint;
        private MapPoint _currentPoint;

        private void Start()
        {
            SubscribeToMapPointEvents();
            
            UpdateCurrentPoint(_startPoint);
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

        private void UpdateCurrentPoint(MapPoint mapPoint)
        {
            if(mapPoint == null) return;

            if (_currentPoint != null)
            {
                _currentPoint.Unselect();
            }
            
            _currentPoint = mapPoint;
            _currentPoint.Select();
        }
    }
}

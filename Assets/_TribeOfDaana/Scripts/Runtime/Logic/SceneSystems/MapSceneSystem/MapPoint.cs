using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.MapSceneSystem
{
    public class MapPoint : MonoBehaviour
    {
        [SerializeField] private Image _mapPointImage;
        [SerializeField] private Button _mapPointButton;
        [SerializeField] private List<MapPoint> _neighborPoints = new List<MapPoint>();

        public List<MapPoint> NeighborPoints => _neighborPoints;
        
        public UnityAction<MapPoint> MapPointClicked;

        public void RaiseClicked()
        {
            MapPointClicked?.Invoke(this);
        }

        public void Select()
        {
            _mapPointImage.color = Color.green;
            _mapPointButton.interactable = false;
            
            foreach (MapPoint mapPoint in _neighborPoints)
            {
                mapPoint.MarkAsNeighbor();
            }
        }

        public void Unselect()
        {
            _mapPointImage.color = Color.white;
            _mapPointButton.interactable = false;
            
            foreach (MapPoint mapPoint in _neighborPoints)
            {
                mapPoint.UnmarkAsNeighbor();
            }
        }

        private void MarkAsNeighbor()
        {
            _mapPointImage.color = Color.red;
            _mapPointButton.interactable = true;
        }

        private void UnmarkAsNeighbor()
        {
            _mapPointImage.color = Color.white;
            _mapPointButton.interactable = false;
        }
        
        private void OnDrawGizmos()
        {
            if(_neighborPoints.Count == 0) return;
            
            Color oldColor = Gizmos.color;
            Gizmos.color = Color.green;
            
            foreach (MapPoint neighborPoint in _neighborPoints)
            {
                if(neighborPoint == null) continue;
                Gizmos.DrawLine(transform.position, neighborPoint.transform.position);
            }
            
            Gizmos.color = oldColor;
        }
    }
}

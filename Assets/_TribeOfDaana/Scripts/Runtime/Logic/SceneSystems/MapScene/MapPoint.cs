using System.Collections.Generic;
using _TribeOfDaana.Scripts.Runtime.Logic.SceneSubsystems.Clickable;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.MapScene
{
    public class MapPoint : MonoBehaviour, IClickable
    {
        [SerializeField] private SpriteRenderer _mapPointSprite;
        [SerializeField] private List<MapPoint> _neighborPoints = new List<MapPoint>();

        public UnityAction<MapPoint> MapPointClicked;

        #region  IClickable Implementation
        public void Click()
        {
            Debug.Log("Clicked a MapPoint");
            MapPointClicked?.Invoke(this);
        }
        #endregion
        
        public void Select()
        {
            _mapPointSprite.color = Color.green;
            
            foreach (MapPoint mapPoint in _neighborPoints)
            {
                mapPoint.MarkAsNeighbor();
            }
        }

        public void Unselect()
        {
            _mapPointSprite.color = Color.white;
            
            foreach (MapPoint mapPoint in _neighborPoints)
            {
                mapPoint.UnmarkAsNeighbor();
            }
        }

        private void MarkAsNeighbor()
        {
            _mapPointSprite.color = Color.red;
        }

        private void UnmarkAsNeighbor()
        {
            _mapPointSprite.color = Color.white;
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

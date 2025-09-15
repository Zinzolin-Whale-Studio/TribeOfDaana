using System.Collections.Generic;
using _TribeOfDaana.Scripts.Runtime.Logic.SceneSubsystems.Clickable;
using UnityEngine;
using UnityEngine.Events;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.MapScene.Map.Point
{
    public class MapPoint : MonoBehaviour, IClickable
    {
        #region  Fields
        [SerializeField] private List<MapPoint> _neighborPoints = new List<MapPoint>();
        
        #endregion

        #region Properties
        public List<MapPoint> NeighborPoints => _neighborPoints;
        [field:SerializeField] public MapPointInfo PointInfo { get; private set; }
        #endregion

        #region Actions
        public event UnityAction<MapPoint> Clicked;
        #endregion

        #region  IClickable Implementation
        public void Click()
        {
            Clicked?.Invoke(this);
        }
        #endregion
        
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

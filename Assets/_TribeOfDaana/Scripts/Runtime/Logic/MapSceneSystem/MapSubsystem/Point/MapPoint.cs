using System.Collections.Generic;
using _TribeOfDaana.Scripts.Runtime.Logic.ClickableSubsystem;
using UnityEngine;
using UnityEngine.Events;

namespace _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem.MapSubsystem.Point
{
    public class MapPoint : MonoBehaviour, IClickable
    {
        #region  Fields
        [SerializeField] private List<MapPoint> m_neighborPoints = new List<MapPoint>();
        #endregion

        #region Properties
        public List<MapPoint> NeighborPoints => m_neighborPoints;
        [field:SerializeField] public MapPointInfo MapPointInfo { get; private set; }
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
            if(m_neighborPoints.Count == 0) return;
            
            Color oldColor = Gizmos.color;
            Gizmos.color = Color.green;
            
            foreach (MapPoint neighborPoint in m_neighborPoints)
            {
                if(neighborPoint == null) continue;
                Gizmos.DrawLine(transform.position, neighborPoint.transform.position);
            }
            
            Gizmos.color = oldColor;
        }
    }
}

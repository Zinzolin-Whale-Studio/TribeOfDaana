using _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem.MapSubsystem.Point;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.UI.MapSceneSystem.MapSubsystem
{
    public class MapPointInfoWorldCanvas : MonoBehaviour
    {
        public void Show(MapPointInfo mapPointInfo)
        {
            transform.position = mapPointInfo.Position;
        }
    }
}
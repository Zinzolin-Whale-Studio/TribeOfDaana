using _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem.MapSubsystem.Point;
using TMPro;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.UI.MapSceneSystem.MapSubsystem
{
    public class MapPointInfoUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _pointNameText;
        
        
        public void UpdateInfo(MapPointInfo mapPointInfo)
        {
            _pointNameText.text = mapPointInfo.Name;
        }
    }
}
using UnityEngine;
using UnityEngine.EventSystems;

namespace _TribeOfDaana.Scripts.Runtime.Test
{
    public class TestPointerHandler : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
    {
        

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Pointer Down");

        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("Pointer Up");
        }
    }
}

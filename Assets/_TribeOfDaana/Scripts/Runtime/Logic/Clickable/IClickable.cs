using UnityEngine;
using UnityEngine.EventSystems;

namespace _TribeOfDaana.Scripts.Runtime.Logic.Clickable
{
    public interface IClickable : IPointerDownHandler, IPointerUpHandler

    {
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            //Will do some click down anim
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            Click();
        }

        public abstract void Click();
    }
}
